using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfflineFileSync
{
    public partial class Form1 : Form
    {
        private const string SnapshotFileName = "sync_snapshot.txt";

        public Form1()
        {
            InitializeComponent();
        }

        #region UI Event Handlers

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select the source folder";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtSource.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select the destination folder";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtDestination.Text = fbd.SelectedPath;
                }
            }
        }

        private async void btnCopy_Click(object sender, EventArgs e)
        {
            string sourcePath = txtSource.Text.Trim();
            string destinationPath = txtDestination.Text.Trim();
            DateTime thresholdDate = dtpModifiedDate.Value;

            if (!ValidatePaths(sourcePath, destinationPath)) return;

            SetUIEnabled(false);
            pbCopy.Visible = true;
            lblStatus.Visible = true;
            pbCopy.Value = 0;
            lblStatus.Text = "Scanning files...";

            try
            {
                var progress = new Progress<(int copied, int total, string currentFile)>(p =>
                {
                    if (p.total > 0)
                    {
                        pbCopy.Maximum = p.total;
                        pbCopy.Value = Math.Min(p.copied, p.total);
                        lblStatus.Text = $"Copying: {p.copied} / {p.total} files. Current: {Path.GetFileName(p.currentFile)}";
                    }
                });

                int totalCopied = 0;

                await Task.Run(() =>
                {
                    int totalToCopy = CountMatchingFiles(sourcePath, thresholdDate);
                    if (totalToCopy > 0)
                    {
                        totalCopied = ProcessDirectoryWithProgress(sourcePath, destinationPath, thresholdDate, progress, totalToCopy, 0);
                    }
                });

                MessageBox.Show($"Copy process completed successfully!\nTotal files copied: {totalCopied}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetUIEnabled(true);
                pbCopy.Visible = false;
                lblStatus.Visible = false;
            }
        }

        private async void btnGenerateSnapshot_Click(object sender, EventArgs e)
        {
            string sourcePath = txtSource.Text.Trim();
            if (string.IsNullOrWhiteSpace(sourcePath) || !Directory.Exists(sourcePath))
            {
                MessageBox.Show("Please provide a valid source directory path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGenerateSnapshot.Enabled = false;
            lblStatus.Visible = true;
            lblStatus.Text = "Generating snapshot...";

            try
            {
                await Task.Run(() =>
                {
                    var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories)
                        .Select(f => Path.GetRelativePath(sourcePath, f))
                        .Where(f => !f.Equals(SnapshotFileName, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    File.WriteAllLines(Path.Combine(sourcePath, SnapshotFileName), files);
                });

                MessageBox.Show("Snapshot generated successfully in the source directory.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating snapshot: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerateSnapshot.Enabled = true;
                lblStatus.Visible = false;
            }
        }

        private async void btnAnalyzeAndClean_Click(object sender, EventArgs e)
        {
            string destinationPath = txtDestination.Text.Trim();
            string snapshotPath = Path.Combine(destinationPath, SnapshotFileName);

            if (string.IsNullOrWhiteSpace(destinationPath) || !Directory.Exists(destinationPath))
            {
                MessageBox.Show("Please provide a valid destination directory path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(snapshotPath))
            {
                MessageBox.Show($"Snapshot file '{SnapshotFileName}' not found in the destination folder.", "Snapshot Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnAnalyzeAndClean.Enabled = false;
            lstOrphans.Items.Clear();
            lblStatus.Visible = true;
            lblStatus.Text = "Analyzing destination...";

            try
            {
                List<string> orphans = new List<string>();

                await Task.Run(() =>
                {
                    var snapshotFiles = new HashSet<string>(File.ReadAllLines(snapshotPath), StringComparer.OrdinalIgnoreCase);
                    var currentFiles = Directory.EnumerateFiles(destinationPath, "*", SearchOption.AllDirectories)
                        .Select(f => Path.GetRelativePath(destinationPath, f))
                        .Where(f => !f.Equals(SnapshotFileName, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    orphans = currentFiles.Except(snapshotFiles, StringComparer.OrdinalIgnoreCase).ToList();
                });

                if (orphans.Count == 0)
                {
                    MessageBox.Show("No orphaned files found. Destination is in sync with the snapshot.", "Clean", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var orphan in orphans)
                {
                    lstOrphans.Items.Add(orphan);
                }

                var result = MessageBox.Show($"Found {orphans.Count} orphaned files. Do you want to delete them?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int deletedCount = 0;
                    await Task.Run(() =>
                    {
                        foreach (var orphan in orphans)
                        {
                            try
                            {
                                string fullPath = Path.Combine(destinationPath, orphan);
                                if (File.Exists(fullPath))
                                {
                                    File.Delete(fullPath);
                                    deletedCount++;
                                }
                            }
                            catch { /* Ignore individual file errors */ }
                        }

                        // Cleanup empty folders
                        CleanupEmptyFolders(destinationPath);
                    });

                    lstOrphans.Items.Clear();
                    MessageBox.Show($"Deleted {deletedCount} orphaned files and cleaned up empty folders.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during analyze and clean: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAnalyzeAndClean.Enabled = true;
                lblStatus.Visible = false;
            }
        }

        #endregion

        #region Helper Methods

        private bool ValidatePaths(string source, string destination)
        {
            if (string.IsNullOrWhiteSpace(source) || !Directory.Exists(source))
            {
                MessageBox.Show("Please provide a valid source directory path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                MessageBox.Show("Destination path cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void SetUIEnabled(bool enabled)
        {
            btnCopy.Enabled = enabled;
            btnBrowseSource.Enabled = enabled;
            btnBrowseDestination.Enabled = enabled;
            txtSource.Enabled = enabled;
            txtDestination.Enabled = enabled;
            dtpModifiedDate.Enabled = enabled;
            btnCopy.Text = enabled ? "Copy Files" : "Copying...";
        }

        private int CountMatchingFiles(string sourceDir, DateTime thresholdDate)
        {
            int count = 0;
            try
            {
                foreach (string filePath in Directory.EnumerateFiles(sourceDir))
                {
                    try
                    {
                        FileInfo fi = new FileInfo(filePath);
                        if (fi.LastWriteTime.Date >= thresholdDate.Date || fi.CreationTime.Date >= thresholdDate.Date)
                        {
                            count++;
                        }
                    }
                    catch (UnauthorizedAccessException) { }
                }

                foreach (string subDir in Directory.EnumerateDirectories(sourceDir))
                {
                    count += CountMatchingFiles(subDir, thresholdDate);
                }
            }
            catch (UnauthorizedAccessException) { }
            return count;
        }

        private int ProcessDirectoryWithProgress(string sourceDir, string destDir, DateTime thresholdDate, IProgress<(int copied, int total, string currentFile)> progress, int totalFiles, int alreadyCopied)
        {
            int currentCopied = alreadyCopied;

            try
            {
                foreach (string filePath in Directory.EnumerateFiles(sourceDir))
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(filePath);

                        if (fileInfo.LastWriteTime.Date >= thresholdDate.Date || fileInfo.CreationTime.Date >= thresholdDate.Date)
                        {
                            if (!Directory.Exists(destDir))
                            {
                                Directory.CreateDirectory(destDir);
                            }

                            string fileName = Path.GetFileName(filePath);
                            string destFile = Path.Combine(destDir, fileName);

                            File.Copy(filePath, destFile, true);
                            currentCopied++;
                            progress.Report((currentCopied, totalFiles, filePath));
                        }
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (IOException) { }
                }

                foreach (string subDir in Directory.EnumerateDirectories(sourceDir))
                {
                    string dirName = Path.GetFileName(subDir);
                    string destSubDir = Path.Combine(destDir, dirName);
                    currentCopied = ProcessDirectoryWithProgress(subDir, destSubDir, thresholdDate, progress, totalFiles, currentCopied);
                }
            }
            catch (UnauthorizedAccessException) { }

            return currentCopied;
        }

        private void CleanupEmptyFolders(string path)
        {
            try
            {
                foreach (var directory in Directory.GetDirectories(path))
                {
                    CleanupEmptyFolders(directory);
                    if (!Directory.EnumerateFileSystemEntries(directory).Any())
                    {
                        Directory.Delete(directory, false);
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
        }

        #endregion
    }
}

namespace OfflineFileSync
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtSource = new TextBox();
            txtDestination = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnBrowseSource = new Button();
            btnBrowseDestination = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label3 = new Label();
            dtpModifiedDate = new DateTimePicker();
            btnCopy = new Button();
            tabPage2 = new TabPage();
            label4 = new Label();
            lstOrphans = new ListBox();
            btnAnalyzeAndClean = new Button();
            btnGenerateSnapshot = new Button();
            pbCopy = new ProgressBar();
            lblStatus = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // txtSource
            // 
            txtSource.Location = new Point(150, 25);
            txtSource.Name = "txtSource";
            txtSource.Size = new Size(385, 23);
            txtSource.TabIndex = 0;
            // 
            // txtDestination
            // 
            txtDestination.Location = new Point(150, 65);
            txtDestination.Name = "txtDestination";
            txtDestination.Size = new Size(385, 23);
            txtDestination.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 28);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 4;
            label1.Text = "Source Folder:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 68);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 5;
            label2.Text = "Destination Folder:";
            // 
            // btnBrowseSource
            // 
            btnBrowseSource.Location = new Point(545, 24);
            btnBrowseSource.Name = "btnBrowseSource";
            btnBrowseSource.Size = new Size(75, 25);
            btnBrowseSource.TabIndex = 1;
            btnBrowseSource.Text = "Browse...";
            btnBrowseSource.UseVisualStyleBackColor = true;
            btnBrowseSource.Click += btnBrowseSource_Click;
            // 
            // btnBrowseDestination
            // 
            btnBrowseDestination.Location = new Point(545, 64);
            btnBrowseDestination.Name = "btnBrowseDestination";
            btnBrowseDestination.Size = new Size(75, 25);
            btnBrowseDestination.TabIndex = 3;
            btnBrowseDestination.Text = "Browse...";
            btnBrowseDestination.UseVisualStyleBackColor = true;
            btnBrowseDestination.Click += btnBrowseDestination_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(25, 110);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(595, 260);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(dtpModifiedDate);
            tabPage1.Controls.Add(btnCopy);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(587, 232);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Copy Files";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 25);
            label3.Name = "label3";
            label3.Size = new Size(88, 15);
            label3.TabIndex = 6;
            label3.Text = "Threshold Date:";
            // 
            // dtpModifiedDate
            // 
            dtpModifiedDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpModifiedDate.Format = DateTimePickerFormat.Custom;
            dtpModifiedDate.Location = new Point(125, 19);
            dtpModifiedDate.Name = "dtpModifiedDate";
            dtpModifiedDate.Size = new Size(300, 23);
            dtpModifiedDate.TabIndex = 0;
            // 
            // btnCopy
            // 
            btnCopy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCopy.Location = new Point(125, 60);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(120, 35);
            btnCopy.TabIndex = 1;
            btnCopy.Text = "Copy Files";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(lstOrphans);
            tabPage2.Controls.Add(btnAnalyzeAndClean);
            tabPage2.Controls.Add(btnGenerateSnapshot);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(587, 232);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Snapshot & Clean";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 65);
            label4.Name = "label4";
            label4.Size = new Size(160, 15);
            label4.TabIndex = 3;
            label4.Text = "Orphaned Files (to be deleted):";
            // 
            // lstOrphans
            // 
            lstOrphans.FormattingEnabled = true;
            lstOrphans.ItemHeight = 15;
            lstOrphans.Location = new Point(15, 85);
            lstOrphans.Name = "lstOrphans";
            lstOrphans.Size = new Size(555, 139);
            lstOrphans.TabIndex = 2;
            // 
            // btnAnalyzeAndClean
            // 
            btnAnalyzeAndClean.Location = new Point(180, 20);
            btnAnalyzeAndClean.Name = "btnAnalyzeAndClean";
            btnAnalyzeAndClean.Size = new Size(150, 30);
            btnAnalyzeAndClean.TabIndex = 1;
            btnAnalyzeAndClean.Text = "Analyze & Clean";
            btnAnalyzeAndClean.UseVisualStyleBackColor = true;
            btnAnalyzeAndClean.Click += btnAnalyzeAndClean_Click;
            // 
            // btnGenerateSnapshot
            // 
            btnGenerateSnapshot.Location = new Point(15, 20);
            btnGenerateSnapshot.Name = "btnGenerateSnapshot";
            btnGenerateSnapshot.Size = new Size(150, 30);
            btnGenerateSnapshot.TabIndex = 0;
            btnGenerateSnapshot.Text = "Generate Snapshot";
            btnGenerateSnapshot.UseVisualStyleBackColor = true;
            btnGenerateSnapshot.Click += btnGenerateSnapshot_Click;
            // 
            // pbCopy
            // 
            pbCopy.Location = new Point(25, 385);
            pbCopy.Name = "pbCopy";
            pbCopy.Size = new Size(595, 23);
            pbCopy.TabIndex = 7;
            pbCopy.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(25, 415);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(39, 15);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Ready";
            lblStatus.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 445);
            Controls.Add(lblStatus);
            Controls.Add(pbCopy);
            Controls.Add(tabControl1);
            Controls.Add(btnBrowseDestination);
            Controls.Add(btnBrowseSource);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtDestination);
            Controls.Add(txtSource);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OfflineFileSync";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSource;
        private TextBox txtDestination;
        private Label label1;
        private Label label2;
        private Button btnBrowseSource;
        private Button btnBrowseDestination;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label3;
        private DateTimePicker dtpModifiedDate;
        private Button btnCopy;
        private Button btnGenerateSnapshot;
        private Button btnAnalyzeAndClean;
        private ListBox lstOrphans;
        private Label label4;
        private ProgressBar pbCopy;
        private Label lblStatus;
    }
}

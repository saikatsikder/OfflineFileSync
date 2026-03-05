# Offline File Sync Utility

A C# Windows Forms application designed to make manual, offline file synchronization between two computers safe and reliable. 

Whether you are copying files to a backup drive or synchronizing directories across an air-gapped network via USB, this utility ensures you only copy what you need and safely clean up orphaned files on the destination without requiring a live network connection.

## 🚀 Features

The application is split into two primary modules:

### 1. Date-Based File Copier (Copy Files Tab)
Extract files from a source directory based on when they were created or modified.
* **Smart Filtering:** Selects files modified or created *after* a specific date.
* **Folder Structure Retention:** Recreates the exact source folder structure in the destination, but *only* if matching files are found (prevents empty folder spam).
* **Safe Execution:** Automatically skips protected system folders without crashing.

### 2. Air-Gapped Folder Synchronization (Snapshot & Clean Tab)
Safely delete files on a destination PC that have been removed from the source PC, without the two computers ever being on the same network.
* **State-Map Generation:** Creates a `sync_snapshot.txt` file on the source machine containing relative paths of all current files.
* **Orphan Detection:** Compares the destination folder against the snapshot file to find "orphaned" files (files that exist in the destination but were deleted from the source).
* **Safe Deletion:** Lists orphaned files for user review before requiring a final confirmation to delete.
* **Deep Clean:** Automatically sweeps the destination directory after file deletion to remove any leftover empty folders.

## 🛠️ Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (or your target .NET version)
* Windows OS (for Windows Forms support)

## 📖 How to Use the Offline Sync (Mirroring)

If you want to make PC B an exact mirror of PC A, follow these steps:

**On the Source PC (PC A):**
1. Open the application and go to the **Snapshot & Clean** tab.
2. Select your Source Folder.
3. Click **Generate Snapshot**. This creates a `sync_snapshot.txt` file in your source folder.
4. Manually copy your source folder (including the snapshot text file) to your USB drive, and transfer it to the Destination PC.

**On the Destination PC (PC B):**
1. Copy the updated files from your USB drive into your Destination folder.
2. Open the application and go to the **Snapshot & Clean** tab.
3. Select your Destination Folder (which now contains the `sync_snapshot.txt`).
4. Click **Analyze and Clean**.
5. Review the list of orphaned files in the UI. Click **Yes** when prompted to delete them and mirror the source state.

## 💻 Building from Source

1. Clone the repository:
   ```bash
   git clone [https://github.com/yourusername/OfflineFileSync.git](https://github.com/yourusername/OfflineFileSync.git)

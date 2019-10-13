﻿using System;
using System.Collections.Generic;
using System.IO;


namespace R5T.Gepidia
{
    public interface IFileSystemOperationsListing
    {
        bool ExistsFile(string filePath);
        bool ExistsDirectory(string directoryPath);
        bool Exists(string path);

        bool IsExistingFile(string path); // Same as ExistsFile().
        bool IsExistingDirectory(string path); // Same as ExistsDirectory().

        bool IsFile(string path); // Same as IsExistingFile(). Path must exist, throw not found exception if not.
        bool IsDirectory(string path); // Same as IsExistingDirectory(). Path must exist, throw not found exception if not.

        // Create a codenamed library project that is just for a FileSystemEntryType enumeration (file/directory).

        void DeleteFile(string filePath); // Checks that entry exists, if it exists, that it's a file, and then deletes it. Idempotent, can be called multiple times with no ill effect.
        void DeleteDirectory(string directoryPath, bool recursive = true); // Checks that the entry exists, if it exists, that it's a diretory, then deletes it (recursive and all!). Idempotent, can be called multiple times with no ill effect.
        void Delete(string path); // Checks that path exists, and if it does, what type of entry the path is, then calls the correct delete method.

        Stream CreateFile(string filePath, bool overwrite = true); // If overwrite is false and file path exists, throws an access exception? Found exception? Invalid operation exception? Exception?
        Stream OpenFile(string filePath); // File must exist.
        Stream ReadFile(string filePath);

        void CreateDirectory(string directoryPath);
        IEnumerable<string> EnumerateFileSystemEntries(string directoryPath, bool recursive = false);
        IEnumerable<string> EnumerateDirectories(string directoryPath); // Non-recursive.
        IEnumerable<string> EnumerateFiles(string directoryPath); // Non-recursive.

        // Extensions for filtering enumerated files/directories.

        DateTime GetDirectoryLastModifiedTime(string directoryPath);
        DateTime GetFileLastModifiedTime(string filePath);
        DateTime GetLastModifiedTime(string path);
        DateTime GetDirectoryLastModifiedTimeUTC(string directoryPath);
        DateTime GetFileLastModifiedTimeUTC(string filePath);
        DateTime GetLastModifiedTimeUTC(string path);
        //void SetLastModifiedTime(string path); // Modify the file to set the last modified time to now!
        //void SetLastModifiedTimeUTC(string path); // Modify the file to set the last modified time to now!

        void ChangePermissions(string path, short mode);

        // Copy internally to the file system.
        void CopyFile(string sourceFilePath, string destinationFilePath, bool overwrite = true);
        void CopyDirectory(string sourceDirectoryPath, string destinationDirectoryPath); // Overwrite files.
        void Copy(string sourcePath, string destinationPath, bool overwrite = true);

        // Copy files externally to/from the file system.
        void Copy(Stream source, string destinationFilePath, bool overwrite = true); // Copy from source to destination.
        //Task CopyAsync(Stream source, string destinationFilePath, bool overwrite = true); // Maybe later for async.
        void Copy(string sourceFilePath, Stream destination);
        //Task CopyAsync(string sourceFilePath, Stream destination); // Maybe later for async.

        void CopyTo(Stream source, string destinationFilePath, bool overwrite = true); // Same as Copy(Stream, string).
        void CopyFrom(string sourceFilePath, Stream destination); // Sames as Copy(string, Stream).

        // Move files and directories internally within the file system.
        void MoveFile(string sourceFilePath, string destinationFilePath, bool overwrite = true);
        void MoveDirectory(string sourceDirectoryPath, string destinationDirectoryPath);
        void Move(string sourcePath, string destinationPath, bool overwrite = true);
        // Rename is different than move? Maybe internally to the file system?

        // Extensions for reading/writing text lines.

        #region Exceptions

        string GetCannotOverwriteFileExceptionMessage(string filePath);
        IOException GetCannotOverwriteFileIOException(string filePath);

        #endregion
    }
}

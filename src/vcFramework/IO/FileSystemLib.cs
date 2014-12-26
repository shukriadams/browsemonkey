//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Linq;
using vcFramework.Parsers;
using vcFramework.RandomItems;
using System.Collections.Generic;

namespace vcFramework.IO
{
    /// <summary> 
    /// Static library of file io-related methods 
    /// </summary>
    public class FileSystemLib
    {
        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static double BytesToMegs(long bytes) 
        {
        	return (bytes / 1024f) / 1024f;
        }

        /// <summary>
        /// Traverses a directory structure, looking for a directory of the given name (folder name, only full path)
        /// </summary>
        /// <param name="startDirectory"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string FindDirectory(string startDirectory, string folder)
        {

            while (true)
            {
                DirectoryInfo dir = new DirectoryInfo(startDirectory);
                foreach (DirectoryInfo child in dir.GetDirectories())
                {
                    if (child.FullName.ToLower().EndsWith(Path.DirectorySeparatorChar + folder))
                        return child.FullName;
                }

                if (dir.Parent == null)
                    return null;

                startDirectory = dir.Parent.FullName;
            }

        }


        /// <summary>
        /// Deletes everything in a folder
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>True on no errors, false if there were errors.</returns>
        public static bool ClearDirectory(string directory)
        {
            DirectoryInfo dir = new DirectoryInfo(directory);
            if (!dir.Exists)
                return false;

            bool noErrors = true;
            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch
                {
                    noErrors = false;
                }
            }
            foreach (DirectoryInfo subDirectory in dir.GetDirectories())
            {
                try
                {
                    subDirectory.Delete(true);
                }
                catch
                {
                    noErrors = false;
                }
            }

            return noErrors;
        }


        /// <summary>
        /// Moves a folder from one location to another, if the file and target directory are valid 
        /// </summary>
        static public bool MoveFile_AfterChecking(
            string sourcePath,
            string targetPath,
            bool overwrite
            )
        {
            //MOVES A FILE FROM ONE PLACE TO ANOTHER. IF THE TARGET DIRECTORY DOESNT EXIST THE FILE IS NOT MOVED
            //RETURNS TRUE IF MOVED, RETURNS FALSE IF NOT

            bool movedSuccesfully = false;

            DirectoryInfo folderScan = new DirectoryInfo(targetPath);

            // MOVES FILE
            if (folderScan.Exists)
            {
                //MOVES THE FILE IN QUESTION TO THE UPLOADED FOLDER
                FileInfo objMoveFile = new FileInfo(sourcePath);
                if (objMoveFile.Exists)
                {
                    string strFileName = sourcePath.Substring(sourcePath.LastIndexOf("/") + 1, sourcePath.Length - sourcePath.LastIndexOf("/") - 1);



                    //CHECKS IF A FILE OF THE SAME NAME ALREADY EXISTS IN TARGET PATH. DELETES IF blnOverWrite SET TO TRUE
                    if (overwrite)
                    {
                        FileInfo objExistsTest = new FileInfo(targetPath + "/" + strFileName);
                        if (objExistsTest.Exists)
                        {
                            objExistsTest.Delete();
                        }
                    }


                    //DESTROYS AND RECREATES OBJECT TO MAKE SURE IT HAS THE _LATST_ POSSIBLE FILE SYSTEM INFO
                    objMoveFile = new FileInfo(sourcePath);

                    if (objMoveFile.Exists)
                    {
                        //DOES THE ACTUAL MOVE
                        objMoveFile.MoveTo(targetPath + "/" + strFileName);
                        movedSuccesfully = true;

                    }





                }
            }

            return movedSuccesfully;
        }


        /// <summary>
        /// Deletes a file if it exists - else, ignores it
        /// </summary>
        static public void DeleteFile_AfterChecking(
            string file
            )
        {
            FileInfo checkFile = new FileInfo(file);

            if (checkFile.Exists)
                checkFile.Delete();

        }


        /// <summary> 
        /// Returns true if a given string contains any characters which are invalid in a file name 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public bool FilenameContainsInvalidCharacters(
            string filename
            )
        {
            char[] invalidChars = new char[9];
            invalidChars[0] = '/';
            invalidChars[1] = '\\';
            invalidChars[2] = ':';
            invalidChars[3] = '*';
            invalidChars[4] = '?';
            invalidChars[5] = '\"';
            invalidChars[6] = '<';
            invalidChars[7] = '>';
            invalidChars[8] = '|';

            return filename.IndexOfAny(invalidChars) != -1;
        }


        /// <summary> 
        /// Removes all invalid characters from filename 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public string RemoveInvalidCharactersFromFilename(
            string filename
            )
        {
            filename = filename.Replace("/", "");
            filename = filename.Replace("\\", "");
            filename = filename.Replace(":", "");
            filename = filename.Replace("*", "");
            filename = filename.Replace("?", "");
            filename = filename.Replace("\"", "");
            filename = filename.Replace("<", "");
            filename = filename.Replace(">", "");
            filename = filename.Replace("|", "");
            return filename;
        }


        /// <summary> 
        /// Gets a count of all directories under the given one, not matter how far down they may be nested 
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="intFolderCount"></param>
        public static void CountAllDirectoriesUnder(
            string strPath,
            ref int intFolderCount
            )
        {
            string[] childDirectories = null;

            intFolderCount++;

            //gets list of all directories inside current directories
            try
            {
                childDirectories = Directory.GetDirectories(strPath);
            }
            catch
            {
                //do nothing
            }

            //calls this method again for all subdirectories in this directory. this is where recursion occurs
            for (int i = 0; i < childDirectories.Length; i++)
            {
                try
                {
                    CountAllDirectoriesUnder(childDirectories[i], ref intFolderCount);
                }
                catch
                {
                    // do nothing
                }
            }

        }


        /// <summary> 
        /// Gets a count of all files under a given folder, no matter how far down tehy may be nested 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileCount"></param>
        public static void CountAllFilesUnder(
            string path,
            ref int fileCount
            )
        {
            string[] fileList = null;
            string[] childDirectories = null;

            // gets number of files and adds to file count
            try
            {
                fileList = Directory.GetFiles(path);
                fileCount += fileList.Length;
            }
            catch
            {
                // suppress
            }


            //gets list of all directories inside current directories
            try
            {
                childDirectories = Directory.GetDirectories(path);
            }
            catch
            {
                // suppress
            }

            //calls this method again for all subdirectories in this directory. this is where recursion occurs
            for (int i = 0; i < childDirectories.Length; i++)
            {
                try
                {
                    CountAllFilesUnder(childDirectories[i], ref fileCount);
                }
                catch
                {
                    // suppress
                }
            }
        }


        /// <summary> 
        /// Generates a unique file name at the given
        /// path location 
        /// </summary>
        /// <returns></returns>
        public static string GenerateUniqueFileName(
            string path,
            int filenameLength
            )
        {
            // Ensures that directory exists
            if (!Directory.Exists(path))
                throw new Exception("The folder " + path + " does not exist.");

            // ensures that file name length is greater than 0
            if (filenameLength < 1)
                throw new Exception(filenameLength + " is an invalid file name length. Length must be at least 1.");


            string filename = string.Empty;
            while (true)
            {
                // gets a new file name
                filename = RandomLib.RandomString(filenameLength);

                // checks if file name already exists. if not, can exit loop
                // and return file name
                if (!File.Exists(path + "\\" + filename))
                    break;
            }

            return filename;

        }


        /// <summary> </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static long GetDirectoryContentsSize(
            string path
            )
        {

            FileInfo[] files = null;
            DirectoryInfo dirInfo = null;
            long lngLogDirSize = 0;

            dirInfo = new DirectoryInfo(path);

            files = dirInfo.GetFiles();

            foreach (FileInfo objFile in files)
                lngLogDirSize += objFile.Length;

            return lngLogDirSize;
        }


        /// <summary> 
        /// Gets the total size of files directly in folder. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern">For example, "*.zip"</param>
        /// <returns></returns>
        public static long GetDirectoryContentsSize(
            string path,
            string searchPattern
            )
        {
            FileInfo[] files;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            long lngLogDirSize = 0;

            files = dirInfo.GetFiles(searchPattern);

            foreach (FileInfo objFile in files)
                lngLogDirSize += objFile.Length;

            return lngLogDirSize;
        }


        /// <summary> 
        /// Returns true if the suspected child dir is nested under the suspected parent .
        /// </summary>
        /// <param name="suspectedParentDirectory"></param>
        /// <param name="suspectChildDirectory"></param>
        /// <returns></returns>
        public static bool IsAChildOf(
            string suspectedParentDirectory,
            string suspectChildDirectory
            )
        {
            bool isChild = false;

            IsAChildOfInternal(
                suspectedParentDirectory,
                suspectChildDirectory,
                ref isChild);

            return isChild;
        }


        /// <summary>
        /// Recursive method behind IsAChildOf();
        /// </summary>
        /// <param name="suspectedParentDirectoryCurrentLevel"></param>
        /// <param name="suspectChildDirectory"></param>
        /// <param name="isChild"></param>
        private static void IsAChildOfInternal(
            string suspectedParentDirectoryCurrentLevel,
            string suspectChildDirectory,
            ref bool isChild
            )
        {
            try
            {
                // kills concurrent recursing processes
                // if a child confirmation is reported elsewhere
                if (isChild)
                    return;

                DirectoryInfo dirInfo = new DirectoryInfo(suspectedParentDirectoryCurrentLevel);
                DirectoryInfo[] folders = dirInfo.GetDirectories();

                // processes current level folders
                foreach (DirectoryInfo dir in folders)
                {
                    if (Path.GetDirectoryName(dir.FullName) == Path.GetDirectoryName(suspectChildDirectory))
                    {
                        isChild = true;
                        return;
                    }
                }

                // recursion happens here
                foreach (DirectoryInfo dir in folders)
                    IsAChildOfInternal(
                        dir.FullName,
                        suspectChildDirectory,
                        ref isChild);

            }
            catch (UnauthorizedAccessException)
            {
                // suppress these, they will be hit a lot
            }
            catch (PathTooLongException)
            {
                // suppress these, they will be hit a lot
            }
        }


        /// <summary>
        /// Returns true if the given path is writeable
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsWriteable(string path)
        {
            try
            {
                //Create the file.
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    if (fs.CanWrite)
                        return true;
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Gets a list of folders nested in a path. 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetFoldersUnder(string path)
        {
            IList<string> folders = new List<string>();
            GetFoldersUnderInternal(path, folders);
            return folders;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="folders"></param>
        private static void GetFoldersUnderInternal(
            string path,
            ICollection<string> folders
            )
        {
            try
            {
                if (!Directory.Exists(path))
                    return;

                folders.Add(path);

                // not empty, find folders and handle them
                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = dir.GetDirectories();

                foreach (DirectoryInfo child in dirs)
                    GetFoldersUnderInternal(
                        child.FullName,
                        folders);
            }
            catch (UnauthorizedAccessException)
            {
                // suppress these exceptions
            }
            catch (PathTooLongException)
            {
                // suppress these, they will be hit a lot
            }
        }


        /// <summary>
        /// Returns true of directory or any child regardless of depth contains children.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ContainsFiles(string path)
        {
            int fileCount = (from file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                             select file).Count();
            if (fileCount > 0)
                return true;

            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dir.GetDirectories();

            bool childrenContains = false;
            foreach (DirectoryInfo child in dirs)
            {
                if (ContainsFiles(child.FullName))
                {
                    childrenContains = true;
                    break;
                }
            }

            return childrenContains;
        }


        /// <summary>
        /// Gets a list of file names for files nested under a given path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetFilesUnder(string path)
        {

            List<string> files = new List<string>();

            GetFilesUnderInternal(path, null, null, files);

            return files.ToArray();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Path to search in</param>
        /// <param name="fileTypes">File types to filter for. Nullable.</param>
        /// <param name="ignoreFolders">List of folders to ignore. Case-insensitive. Nullable.</param>
        /// <param name="files">Holder of files to return</param>
        private static void GetFilesUnderInternal(
            string path,
            IEnumerable<string> fileTypes,
            ICollection<string> ignoreFolders,
            ICollection<string> files
            )
        {
            try
            {

                // handles files
                if (!Directory.Exists(path))
                    return;

                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] filesInDir = null;

                if (fileTypes == null)
                    filesInDir = dir.GetFiles();
                else
                {
                    string search = "";

                    foreach (string fileType in fileTypes)
                        search += "*." + fileType;

                    filesInDir = dir.GetFiles(search);
                }

                foreach (FileInfo file in filesInDir)
                {
                    try
                    {
                        files.Add(file.FullName);
                    }
                    catch (PathTooLongException)
                    {
                        // suppress these
                    }
                }


                // handles folders
                DirectoryInfo[] dirs = dir.GetDirectories();

                foreach (DirectoryInfo child in dirs)
                    if (ignoreFolders == null || (!ignoreFolders.Contains(child.FullName.ToLower()) && !ignoreFolders.Contains(child.Name)))
                        GetFilesUnderInternal(
                            child.FullName,
                            fileTypes,
                            ignoreFolders,
                            files);
            }
            catch (UnauthorizedAccessException)
            {
                // suppress these exceptions
            }
            catch (PathTooLongException)
            {
                // suppress these, they will be hit a lot
            }
        }


        /// <summary>
        /// Gets a list of file names for files nested under a given path. Only files with
        /// the given filetype (extension) are returned
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ignorefolders"></param>
        /// <returns></returns>
        public static string[] GetFilesUnder(
            string path,
            List<string> ignorefolders
            )
        {
            if (ignorefolders != null)
                for (int i = 0; i < ignorefolders.Count; i++)
                    ignorefolders[i] = ignorefolders[i].ToLower();

            List<string> files = new List<string>();

            GetFilesUnderInternal(path, null, ignorefolders, files);

            return files.ToArray();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileTypes"></param>
        /// <param name="ignorefolders"></param>
        /// <returns></returns>
        public static string[] GetFilesUnder(
            string path,
            string[] fileTypes,
            List<string> ignorefolders
            )
        {
            if (ignorefolders != null)
                for (int i = 0; i < ignorefolders.Count; i++)
                    ignorefolders[i] = ignorefolders[i].ToLower();

            List<string> files = new List<string>();
            GetFilesUnderInternal(path, fileTypes, ignorefolders, files);
            return files.ToArray();
        }


        /// <summary>
        /// Maps an array of paths from one location to another.
        /// </summary>
        /// <returns></returns>
        public static string[] MapPaths(
            string[] sourceFiles,
            string srcRoot,
            string targetRoot
            )
        {
            srcRoot = srcRoot.ToLower();
            targetRoot = targetRoot.ToLower();

            string[] targetPaths = new string[sourceFiles.Length];

            for (int i = 0; i < sourceFiles.Length; i++)
            {
                sourceFiles[i] = sourceFiles[i].ToLower();

                // ensure that the source root on the current path is valid
                if (!sourceFiles[i].StartsWith(srcRoot))
                    throw new MappingException(sourceFiles[i]);

                // needs to remove any trailing "\" from paths
                if (srcRoot.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    srcRoot = srcRoot.Substring(0,
                        srcRoot.Length - Path.DirectorySeparatorChar.ToString().Length);
                if (targetRoot.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    targetRoot = targetRoot.Substring(0,
                        targetRoot.Length - Path.DirectorySeparatorChar.ToString().Length);

                targetPaths[i] = sourceFiles[i].Replace(
                    srcRoot,
                    targetRoot);
            }

            return targetPaths;

        }


        /// <summary>
        /// Gets the "lowest" folder in path, eg, returns "world" from 
        /// "d:\hello\there\world"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetRootPath(
            string path
            )
        {
            // remove trailing "\"
            if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                path = path.Substring(0, path.Length - 1);

            // c:
            if (ParserLib.StringCount(path, Path.DirectorySeparatorChar.ToString()) == 0)
                return string.Empty;

            // c:\1
            return ParserLib.ReturnAfterLast(
                path,
                Path.DirectorySeparatorChar.ToString());
        }

        #endregion
    }
}

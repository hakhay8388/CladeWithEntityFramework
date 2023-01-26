using Bootstrapper.Core.nApplication;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nCore;
using Bootstrapper.Core.nExceptions;
using Bootstrapper.Core.nHandlers.nStringHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Bootstrapper.Core.nHandlers.nFileHandler
{
    public class cFileHandler : cCoreObject
    {
        public cFileHandler(nApplication.cApp _App)
            : base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cFileHandler>(this);
        }

        public string GenerateFileName(string extension = "")
        {
            return string.Concat(Path.GetRandomFileName().Replace(".", ""),
                (!string.IsNullOrEmpty(extension)) ? (extension.StartsWith(".") ? extension : string.Concat(".", extension)) : "");
        }

        private bool WriteString(string _Value, string _FilePath, FileMode _FileMode, Encoding _Encoding)
        {
            int __NumberOfRetries = 100;
            int __DelayOnRetry = 10;

            bool __ReturnValue = false;
            for (int i = 1; i <= __NumberOfRetries; ++i)
            {
                try
                {
                    Stream __SafeStream = Stream.Synchronized(File.Open(_FilePath, _FileMode));

                    //FileStream fileStream = new FileStream(filePath, fileMode);
                    BinaryWriter _BinaryWriter = new BinaryWriter(__SafeStream, _Encoding);

                    _BinaryWriter.Write(_Value.ToCharArray());
                    _BinaryWriter.Close();
                    __SafeStream.Close();
                    __ReturnValue = true;
                    break; // When done we can break loop
                }
                catch (IOException _Ex) when (i <= __NumberOfRetries)
                {
                    //App.Loggers.CoreLogger.LogError(e);
                    // You may check error code to filter some exceptions, not every error
                    // can be recovered.
                    Thread.Sleep(__DelayOnRetry);
                }
            }
            return __ReturnValue;
        }


		private void WriteStringWithNewThread(string _Value, string _FilePath, FileMode _FileMode, Encoding _Encoding)
		{
			Thread __Thread1 = new Thread(() =>
			{
				int __NumberOfRetries = 100;
				int __DelayOnRetry = 10;

				for (int i = 1; i <= __NumberOfRetries; ++i)
				{
					try
					{
						Stream __SafeStream = Stream.Synchronized(File.Open(_FilePath, _FileMode));

						//FileStream fileStream = new FileStream(filePath, fileMode);
						BinaryWriter _BinaryWriter = new BinaryWriter(__SafeStream, _Encoding);
						_BinaryWriter.Write(_Value.ToCharArray());
						_BinaryWriter.Close();
						__SafeStream.Close();
						break; // When done we can break loop
					}

					catch (IOException _Ex) when (i <= __NumberOfRetries)
					{
						//App.Loggers.CoreLogger.LogError(e);
						// You may check error code to filter some exceptions, not every error
						// can be recovered.
						Thread.Sleep(__DelayOnRetry);						
					}
				}
			});
			__Thread1.Start();
		}

		public bool WriteString(string _Value, string _FilePath)
        {
            return WriteString(_Value, _FilePath, FileMode.Create, Encoding.UTF8);
        }
        public void AppendString(string _Value, string _FilePath)
        {
			WriteString(_Value, _FilePath, FileMode.Append, Encoding.UTF8);
        }

		public void AppendStringWithNewThread(string _Value, string _FilePath)
		{
			WriteStringWithNewThread(_Value, _FilePath, FileMode.Append, Encoding.UTF8);
		}
		public void WriteByte(byte[] _Value, string _FilePath)
        {
            int __NumberOfRetries = 100;
            int __DelayOnRetry = 10;

            for (int i = 1; i <= __NumberOfRetries; ++i)
            {
                try
                {
                    Stream __SafeStream = Stream.Synchronized(File.Open(_FilePath, FileMode.Create));
                    BinaryWriter __BinaryWriter = new BinaryWriter(__SafeStream);

                    __BinaryWriter.Write(_Value);
                    __BinaryWriter.Close();
                    __SafeStream.Close();

                    break; // When done we can break loop
                }
                catch (IOException e) when (i <= __NumberOfRetries)
                {
                    // You may check error code to filter some exceptions, not every error
                    // can be recovered.
                    Thread.Sleep(__DelayOnRetry);

					Thread.Sleep(__DelayOnRetry);
				}
            }

        }
        public byte[] ReadByte(string _FilePath)
        {
            return File.ReadAllBytes(_FilePath);
        }
        public string ReadString(string _FilePath, Encoding _Encoding, bool _RemoveUTF8Mark)
        {
            string _Value = null;

            byte[] data = File.ReadAllBytes(_FilePath);

            if (_RemoveUTF8Mark)
                _Value = _Encoding.GetString(App.Handlers.StringHandler.Remove_UTF8BomMark(data)); // generally for files saved by visualstudio
            else
                _Value = _Encoding.GetString(data);

            return _Value;
        }
        public string ReadString(string _FilePath)
        {
            return ReadString(_FilePath, Encoding.UTF8, false);
        }
        public string ReadString(string _FilePath, bool _RemoveUTF8Mark)
        {
            return ReadString(_FilePath, Encoding.UTF8, _RemoveUTF8Mark);
        }
        public string MakeDirectory(string _Path, bool _Recursive)
        {
            if (_Path == null)
                return _Path;

            if (!Directory.Exists(_Path))
            {
                string parentPath = Path.GetDirectoryName(_Path);

                if (!parentPath.IsNullOrEmpty())
                {
                    if (!Directory.Exists(parentPath) && !_Recursive)
                        //throw new CoreException(RS.Message.E1038_ParentPathNotFound, parentPath);
                        throw new cCoreException(App, "MakeDirectory Error");
                }

                Directory.CreateDirectory(_Path);
            }
            return _Path;
        }
        public string MakeDirectoryWithCredential(string _Path, bool _Recursive, string _DomainPath, string _User, string _Password)
        {

            App.Utils.ImpersonatedUserUtils.ConnectPath(_DomainPath, _User, _Password);
            {
                if (_Path == null)
                {
                    App.Utils.ImpersonatedUserUtils.DisConnectPath(_DomainPath);
                    return _Path;

                }


                if (!Directory.Exists(_Path))
                {
                    string parentPath = Path.GetDirectoryName(_Path);

                    if (!parentPath.IsNullOrEmpty())
                    {
                        if (!Directory.Exists(parentPath) && !_Recursive)
                            //throw new CoreException(RS.Message.E1038_ParentPathNotFound, parentPath);
                            throw new cCoreException(App, "MakeDirectory Error");
                    }

                    Directory.CreateDirectory(_Path);
                }
            }
            App.Utils.ImpersonatedUserUtils.DisConnectPath(_DomainPath);
            return _Path;
        }
        public void DeleteFile(string _File)
        {
            File.Delete(_File);
        }
        public string File_Zip(string _FilePath, bool _AfterDelete)
        {
            string __ZipFilePath = _FilePath + ".zip";
            try
            {
                using (ZipFile zip = new ZipFile())
                {

                    zip.UseZip64WhenSaving = Zip64Option.AsNecessary;



                    zip.AddFile(_FilePath, "");
                    zip.ZipErrorAction = ZipErrorAction.Skip;
                    zip.Save(__ZipFilePath);
                    if (_AfterDelete)
                    {
                        try
                        {
                            File.Delete(_FilePath);
                        }
                        catch (Exception _Ex)
                        {
                            App.Loggers.CoreLogger.LogError(_Ex);
                        }
                    }

                    return __ZipFilePath;
                }
            }
            catch (Exception ex)
            {
                App.Loggers.CoreLogger.LogError(ex);
                return "";
            }

        }
        public string File_ZipWithCredential(string _FilePath, bool _AfterDelete, string _DomainPath, string _User, string _Password)
        {
            App.Utils.ImpersonatedUserUtils.ConnectPath(_DomainPath, _User, _Password);
            string __ZipFilePath = _FilePath + ".zip";
            try
            {
                using (ZipFile __Zip = new ZipFile())
                {

                    __Zip.UseZip64WhenSaving = Zip64Option.AsNecessary;



                    __Zip.AddFile(_FilePath, "");
                    __Zip.ZipErrorAction = ZipErrorAction.Skip;
                    __Zip.Save(__ZipFilePath);
                    if (_AfterDelete)
                    {
                        try
                        {
                            File.Delete(_FilePath);
                        }
                        catch (Exception _Ex)
                        {
                            App.Loggers.CoreLogger.LogError(_Ex);
                        }
                    }
                    App.Utils.ImpersonatedUserUtils.DisConnectPath(_DomainPath);
                    return __ZipFilePath;
                }
            }
            catch (Exception ex)
            {
                App.Loggers.CoreLogger.LogError(ex);
                App.Utils.ImpersonatedUserUtils.DisConnectPath(_DomainPath);
                return "";
            }

        }
        public void EmptyDirectoryClear(string __StartLocation)
        {
            foreach (var __Directory in Directory.GetDirectories(__StartLocation))
            {
                EmptyDirectoryClear(__Directory);
                if (Directory.GetFiles(__Directory).Length == 0 &&
                    Directory.GetDirectories(__Directory).Length == 0)
                {
                    try
                    {
                        Directory.Delete(__Directory, false);
                    }
                    catch { }
                }
            }
        }
        public string ZipFiles(string __ZipFileName, List<string> _FilePaths, bool _AfterDelete)
        {
            string __ZipFilePath = __ZipFileName + ".zip";
            try
            {
                using (ZipFile zip = new ZipFile())
                {

                    zip.UseZip64WhenSaving = Zip64Option.AsNecessary;


                    for (int i = 0; i < _FilePaths.Count; i++)
                    {
                        zip.AddFile(_FilePaths[i], "");
                    }

                    zip.ZipErrorAction = ZipErrorAction.Skip;
                    zip.Save(__ZipFilePath);

                }
                if (_AfterDelete)
                {
                    try
                    {
                        for (int i = 0; i < _FilePaths.Count; i++)
                        {
                            File.Delete(_FilePaths[i]);
                        }
                    }
                    catch (Exception _Ex)
                    {
                        App.Loggers.CoreLogger.LogError(_Ex);
                    }
                }

                return __ZipFilePath;
            }
            catch (Exception __Ex)
            {
                App.Loggers.CoreLogger.LogError(__Ex);
                return "";
            }

        }
        public string ZipFilesExtract(string _ZipFileName)
        {

            try
            {
                string __ExistingZipFile = _ZipFileName;
                string __TargetDirectory = _ZipFileName.Replace(".zip", "");

                using (ZipFile zip = ZipFile.Read(__ExistingZipFile))
                {
                    foreach (ZipEntry e in zip)
                    {
                        e.Extract(__TargetDirectory);
                    }
                }
                return __TargetDirectory;
            }
            catch (Exception __Ex)
            {
                App.Loggers.CoreLogger.LogError(__Ex);
                return "";
            }

        }
        public void DeleteFileIfExists(string _File)
        {
            if (File.Exists(_File))
            {
                File.Delete(_File);
            }
        }

        public bool Exists(string _File)
        {
            return File.Exists(_File);
        }

        public void DeleteFiles(string _Path, string _Pattern)
        {
            if (Directory.Exists(_Path))
                foreach (string file in Directory.GetFiles(_Path, _Pattern))
                    DeleteFile(file);
        }
        public int DirectoryDeepSize(string _Path)
        {
            cStringList list = new cStringList(_Path.Replace(@"\\", @"\"), @"\");

            while (list.Items.Find(x => x.Equals(string.Empty)) != null)
                list.Items.Remove(string.Empty);

            return list.Items.Count;
        }
        public void RemoveDirectory(string _Path)
        {
            if (DirectoryDeepSize(_Path) < 3)
                //throw new CoreException(RS.Message.E1015_TooFewDeepForRemoveDirectory);
                throw new cCoreException(App, "RemoveDirectory Error");

            if (Directory.Exists(_Path))
                Directory.Delete(_Path, true);
        }
        public byte[] ComputeHash(string _File)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(File.ReadAllBytes(_File));
            }
        }
        public string GetFileChecksum(string _File)
        {
            byte[] hash = ComputeHash(_File);
            string checksum = App.Handlers.StringHandler.ByteToHex(hash);
            return checksum;
        }
        public string GetPathChecksum(string _Path, string _Pattern, string _StripPrefix)
        {
            string value = string.Empty;

            if (!Directory.Exists(_Path))
                return value;

            foreach (string file in Directory.GetFiles(_Path, _Pattern))
            {
                string checksum = GetFileChecksum(file);
                string fileItem = file.StartsWith(_StripPrefix) ? file.Substring(_StripPrefix.Length) : file;
                value += fileItem + "=" + checksum + "\r\n";
            }

            foreach (string subDir in Directory.GetDirectories(_Path, "*.*"))
            {
                value += GetPathChecksum(Path.Combine(_Path, Path.GetFileName(subDir)), _Pattern, _StripPrefix);
            }

            return value;
        }
        public bool FilesAreEqual(string _File1, string _File2)
        {
            byte[] hash1 = ComputeHash(_File1);
            byte[] hash2 = ComputeHash(_File2);

            if (hash1.Length != hash2.Length)
                return false;

            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                    return false;
            }

            return true;
        }
        public int CopyDirectory(string _SourcePath, string _DestPath, string _SearchPattern, bool _Overwrite, Action<string> _Callback)
        {
            int i = 0;
            MakeDirectory(_DestPath, true);

            foreach (string file in Directory.GetFiles(_SourcePath, _SearchPattern))
            {
                i++;

                if (_Callback != null)
                    _Callback.Invoke("Copying... " + file);

                File.Copy(file, Path.Combine(_DestPath, Path.GetFileName(file)), _Overwrite);
            }

            foreach (string subDir in Directory.GetDirectories(_SourcePath))
            {
                string subSourcePath = Path.Combine(_SourcePath, Path.GetFileName(subDir));
                string subDestPath = Path.Combine(_DestPath, Path.GetFileName(subDir));

                i += CopyDirectory(subSourcePath, subDestPath, _SearchPattern, _Overwrite, _Callback);
            }

            return i;
        }
        public string FindFileFromOuterDirectory(string _StartPath, string _FileName)
        {
            if (Directory.Exists(_StartPath))
            {
                string __CheckFile = Path.Combine(_StartPath, _FileName);

                if (File.Exists(__CheckFile))
                    return __CheckFile;

                
                string __RootName = Directory.GetDirectoryRoot(_StartPath);
                DirectoryInfo __DirectoryInfo = Directory.GetParent(_StartPath);
                if (__RootName != _StartPath && __DirectoryInfo != null)
                {
                    string __ParentName = __DirectoryInfo.FullName;
                    return FindFileFromOuterDirectory(__ParentName, _FileName);
                }
            }

            return null;
        }

        public string FindFile(string _StartRootPath, string _FileName)
        {
            if (Directory.Exists(_StartRootPath))
            {
                string checkFile = Path.Combine(_StartRootPath, _FileName);

                if (File.Exists(checkFile))
                    return checkFile;

                foreach (string subDir in Directory.GetDirectories(_StartRootPath, "*.*"))
                {
                    string file = FindFile(subDir, _FileName);

                    if (file != null)
                        return file;
                }
            }

            return null;
        }

        public string FindDirectoryDeepRoot(string _SearchStartPath, string _DirectoryName)
        {
            string __Find = Path.Combine(_SearchStartPath, _DirectoryName);

            if (Directory.Exists(__Find))
            {
                return _SearchStartPath;
            }
            else
            {
                //Directory.GetDirectoryRoot(_SearchStartPath);
                string __Search = Directory.GetParent(_SearchStartPath).FullName;
                return FindDirectoryDeepRoot(__Search, _DirectoryName);
            }
        }

        public string[] FindFileStartWith(string _StartRootPath, string _Start, bool _SearchInSubFolder)
        {
            if (Directory.Exists(_StartRootPath))
            {
                string[] __Files = Directory.GetFiles(_StartRootPath, _Start + "*.dll");

                if (_SearchInSubFolder)
                {
                    foreach (string subDir in Directory.GetDirectories(_StartRootPath, "*.*"))
                    {
                        string[] __InnerFile = FindFileStartWith(subDir, _Start, _SearchInSubFolder);
                        __Files = Enumerable.Union(__Files, __InnerFile).ToArray(); ;

                    }
                }

                return __Files;
            }

            return null;
        }
        public FileStream LockFile(string _File, long _Timeout)
        {
            long total = 0;
            int interval = 500;

            while (total == 0 || total < _Timeout)
            {
                try
                {
                    if (!File.Exists(_File))
                        WriteByte(new byte[] { }, _File);

                    FileStream stream = new FileInfo(_File).Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    return stream;
                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                }

                Thread.Sleep(interval);
                total += interval;
            }

            return null;
        }
        public void UnlockFile(FileStream _LockedFileStream)
        {
            if (_LockedFileStream != null)
                _LockedFileStream.Close();
        }
        public bool IsFileLockable(string _File, long _Timeout)
        {
            FileStream stream = LockFile(_File, _Timeout);
            bool lockable = stream != null;
            UnlockFile(stream);

            return lockable;
        }
        public void Move(string _SourceFileName, string _DestFileName)
        {
            File.Move(_SourceFileName, _DestFileName);
        }

        public void SaveHashTableToFile(Hashtable _Table, String _FileName)
        {
            FileStream __FileStream = new FileStream(_FileName, FileMode.Create);
            StreamWriter __StreamWriter = new StreamWriter(__FileStream);
            foreach (DictionaryEntry __Entry in _Table)
            {
                __StreamWriter.WriteLine("[" + __Entry.Key + "]#=#[" + __Entry.Value + "]");
            }
            __StreamWriter.Close();
            __FileStream.Close();
        }

        public Hashtable LoadHashTableFromFile(String _FileName)
        {
            if (!File.Exists(_FileName))
            {
                Hashtable __Hashtable = new Hashtable();
                SaveHashTableToFile(__Hashtable, _FileName);
            }

            StreamReader __StreamReader = new StreamReader(_FileName);
            Hashtable __Result = new Hashtable();
            String __Line = "";
            Regex __Splitter = new Regex("#=#");
            while ((__Line = __StreamReader.ReadLine()) != null)
            {
                String[] __Columns = __Splitter.Split(__Line);
                __Columns[0] = RemoveWrapper(__Columns[0]);
                __Columns[1] = RemoveWrapper(__Columns[1]);
                __Result.Add(__Columns[0], __Columns[1]);
            }
            __StreamReader.Close();
            return __Result;
        }

        private String RemoveWrapper(String _Value)
        {
            _Value = _Value.Remove(0, 1);
            _Value = _Value.Remove(_Value.Length - 1, 1);
            return _Value;
        }

        public static int GetLineCount(string _FileName)
        {
            int __Count = 0;
            string __Line;
            TextReader __Reader = new StreamReader(_FileName);
            while ((__Line = __Reader.ReadLine()) != null)
            {
                __Count++;
            }
            __Reader.Close();
            return __Count;
        }
    }
}

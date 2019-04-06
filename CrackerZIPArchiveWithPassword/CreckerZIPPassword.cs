using SharpCompress.Archive.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace CrackerZIPArchiveWithPassword
{
    class CreckerZIPPassword
    {
        private object _lockObject = new object();
        private string _currentPassword;
        private ZipArchive _zipArchive;
        private Stream _stream;
        private FileStream _writer;
        private int _countAtemptsCompareCRC = 0;

        public List<string> ListOfFile { get; set; }          // for deleting files after close programm

        public string CurrentPassword
        {
            get
            {
                lock (_lockObject)
                {
                    return _currentPassword;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _currentPassword = value;
                }
            }
        }

        public CreckerZIPPassword() : this("0")
        {
        }

        public CreckerZIPPassword(string currentPassword)
        {
            _currentPassword = currentPassword;
            ListOfFile = new List<string>();
        }

        public string GetPassword(string pathOfZipArchive)
        {
            bool passwordIsFind = false;
            uint CRCOfEntry = 0;
            string nameOfEntireInArchive = "";

            while (!passwordIsFind)
            {
                try
                {
                    _zipArchive = ZipArchive.Open(pathOfZipArchive, _currentPassword);
                }
                catch (Exception e)
                {
                    MessageBox.Show("File not found or you input wrong name of file");
                    break;
                }
                
                try
                {
                    foreach (var entire in _zipArchive.Entries)
                    {
                        _stream = entire.OpenEntryStream();
                        nameOfEntireInArchive = entire.FilePath;

                        if (!ListOfFile.Contains(entire.FilePath))
                        {
                            ListOfFile.Add(entire.FilePath);
                        }

                        _writer = new FileStream(nameOfEntireInArchive, FileMode.Create, FileAccess.Write);
                        entire.WriteTo(_writer);
                        CRCOfEntry = entire.Crc;
                    }
                }
                catch (Exception ex)
                {
                    if (_zipArchive != null)
                    {
                        _zipArchive.Dispose();
                    }
                    if (_stream != null)
                    {
                        _stream.Close();
                    }
                    if (_writer != null)
                    {
                        _writer.Close();
                    }

                    CurrentPassword = PasswordIncrementor.IncrementPassword(CurrentPassword, CurrentPassword.Length - 1);
                    continue;
                }

                _writer.Close();
                uint destCRC = 0;

                using (var newArchive = ZipArchive.Create())
                {
                    newArchive.AddEntry(nameOfEntireInArchive, new FileInfo(nameOfEntireInArchive));

                    using (Stream newStream = File.Create("Destination.zip"))
                    {
                        newArchive.SaveTo(newStream, SharpCompress.Common.CompressionType.LZMA);
                    }

                    ZipArchive z = ZipArchive.Open("Destination.zip");

                    foreach (var item in z.Entries)
                    {
                        destCRC = item.Crc;
                    }
                    z.Dispose();
                }

                if (CRCOfEntry != destCRC)
                {
                    _countAtemptsCompareCRC++;
                    CurrentPassword = PasswordIncrementor.IncrementPassword(CurrentPassword, CurrentPassword.Length - 1);

                    continue;
                }
                //MessageBox.Show("Password: " + CurrentPassword);
                passwordIsFind = true;
            }
            return CurrentPassword;
        }
    }
}
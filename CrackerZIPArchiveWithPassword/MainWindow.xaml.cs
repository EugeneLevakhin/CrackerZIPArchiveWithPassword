using System;
using System.Windows;
using Microsoft.Win32;                      // OpenDialog
using System.Runtime.Remoting.Messaging;    // AsyncResult
using System.Windows.Threading;             // Timer
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using CrackerZIPArchiveWithPassword.Models;
using CrackerZIPArchiveWithPassword.Utilities;

namespace CrackerZIPArchiveWithPassword  //TODO: delete files, more entires, Enabled labels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CreckerZIPPassword _cracker;
        DispatcherTimer _elapsedTimeCountTimer;
        DispatcherTimer _displayCurrentPasswordTimer;

        TimeSpan _elapsedTime;
        string _archiveName = "";
        SaveFileDialog SaveDialogState;
        IAsyncResult asyncResult;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialogZIPFile = new OpenFileDialog();

            openDialogZIPFile.Filter = "ZIP-archive Files|*.zip";
            openDialogZIPFile.ShowDialog();
            labelFileName.Content = openDialogZIPFile.FileName;
            _archiveName = openDialogZIPFile.FileName;

            if (!_archiveName.Equals(""))
            {
                _elapsedTime = new TimeSpan(0, 0, 0);

                StartCracking("0");
            }
        }

        private void MenuItemLoadState_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog stateFileDialog = new OpenFileDialog();
            stateFileDialog.Filter = "Password files|*.psw";
            stateFileDialog.ShowDialog();

            if (!stateFileDialog.FileName.Equals(""))
            {
                SaveDialogState = new SaveFileDialog(); // for quick save
                SaveDialogState.FileName = stateFileDialog.FileName;

                CrackerState crackerState = SerializeHelper.DeSerialize(stateFileDialog.FileName);
                _archiveName = crackerState.FileName;

                FileInfo fi = new FileInfo(_archiveName);

                if (!fi.Exists)
                {
                    MessageBox.Show("Archive not found, please choose file");

                    OpenFileDialog openDialogZIPFile = new OpenFileDialog();

                    openDialogZIPFile.Filter = "ZIP-archive Files|*.zip";
                    openDialogZIPFile.ShowDialog();
                    labelFileName.Content = openDialogZIPFile.FileName;
                    _archiveName = openDialogZIPFile.FileName;
                }

                _elapsedTime = crackerState.ElapsedTime.TimeSpan;

                StartCracking(crackerState.CurrentPassword);
            }
        }

        private void StartCracking(string password)
        {
            _cracker = new CreckerZIPPassword(password);

            Func<string, string> thread = new Func<string, string>(_cracker.GetPassword);

            _elapsedTimeCountTimer = new DispatcherTimer();
            _elapsedTimeCountTimer.Tick += Timer_Tick;
            _elapsedTimeCountTimer.Interval = new TimeSpan(0, 0, 1);
            _elapsedTimeCountTimer.Start();

            _displayCurrentPasswordTimer = new DispatcherTimer();
            _displayCurrentPasswordTimer.Tick += Timer2_Tick;
            _displayCurrentPasswordTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _displayCurrentPasswordTimer.Start();

            asyncResult = thread.BeginInvoke(_archiveName, Callback, null);

            label1.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            label3.Visibility = Visibility.Visible;
            MenuOpen.IsEnabled = false;
            MenuOpenState.IsEnabled = false;
            MenuSave.IsEnabled = true;
            MenuSaveAs.IsEnabled = true;
        }

        private void Callback(IAsyncResult ar)
        {
            AsyncResult AR = ar as AsyncResult;
            Func<string, string> func = AR.AsyncDelegate as Func<string, string>;

            string s = func.EndInvoke(ar);
            Thread.Sleep(1100);
            _elapsedTimeCountTimer.Stop();
            MessageBox.Show("Password: " + s);
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveDialogState == null || SaveDialogState.FileName.Equals(""))
            {
                MenuItemSaveAs_Click(null, null);
            }
            else
            {
                CrackerState crackerState = new CrackerState(_archiveName, _cracker.CurrentPassword, new Time(_elapsedTime));

                XmlSerializer formatter = new XmlSerializer(typeof(CrackerState));

                using (FileStream fs = new FileStream(SaveDialogState.FileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, crackerState);
                }
            }
        }

        private void MenuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveDialogState = new SaveFileDialog();
            SaveDialogState.Filter = "Password files|*.psw";
            SaveDialogState.ShowDialog();

            if (!SaveDialogState.FileName.Equals(""))
            {
                MenuItemSave_Click(null, null);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _elapsedTime = _elapsedTime.Add(new TimeSpan(0, 0, 1));
            labelElapsedTime.Content = _elapsedTime.Hours.ToString() + "h: " + _elapsedTime.Minutes.ToString() + "m: " + _elapsedTime.Seconds.ToString() + "s";
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            labelCurrPassword.Content = _cracker.CurrentPassword;

            if (asyncResult.IsCompleted)
            {
                MenuOpen.IsEnabled = true;
                MenuOpenState.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_cracker != null)
            {
                MessageBoxResult res = MessageBox.Show("Do you want save state of search", "Exiting", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    MenuItemSave_Click(null, null);
                }

                try
                {
                    foreach (var item in _cracker.ListOfFile)
                    {
                        FileInfo Fi = new FileInfo(item);
                        Fi.Delete();
                    }
                    FileInfo F = new FileInfo("Destination.zip");
                    F.Delete();
                }
                catch (Exception)
                {
                }
            }
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;

namespace WPFPractice.Model
{
    class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog()==true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool? ShowDialog(INotifyPropertyChanged viewmodel)
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}

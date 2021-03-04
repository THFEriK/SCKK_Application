using SCKK_App.Models;
using SCKK_App.Requests;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for Upload.xaml
    /// </summary>
    public partial class UploadView : Window
    {
        private List<ResultModel> data;

        public UploadView(List<ResultModel> data)
        {
            this.data = data;
            InitializeComponent();
        }

        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            var rx = new Regex("^[A-z0-9-]+$");
            if (rx.IsMatch(FileNameTb.Text))
            {
                var request = new UploadRequest();
                request.Upload(data, Dashboard.sessionCode, FileNameTb.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("A fájl neve nem tartalmazhat speciális karaktereket!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FileNameTb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FileNameTb.Text == "Fájlnév")
            {
                FileNameTb.Text = string.Empty;
                FileNameTb.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void FileNameTb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FileNameTb.Text == string.Empty)
            {
                FileNameTb.Text = "Fájlnév";
                FileNameTb.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}

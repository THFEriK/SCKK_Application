using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using SCKK_App.Models;
using SCKK_App.Requests;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for Upload.xaml
    /// </summary>
    public partial class Upload : Window
    {
        private List<ResultModel> data;

        public Upload(List<ResultModel> data)
        {
            this.data = data;
            InitializeComponent();
        }

        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            var rx = new Regex("^[a-z0-9]+$");
            if (rx.IsMatch(FileNameTb.Text))
            {
                var request = new UploadRequest();
                request.Upload(data, Dashboard.sessionCode, FileNameTb.Text);
            }
            else
            {
                MessageBox.Show("A fájl neve nem tartalmazhat speciális karaktereket!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

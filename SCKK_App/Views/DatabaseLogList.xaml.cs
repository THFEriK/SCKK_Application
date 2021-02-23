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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SCKK_App.Models;
using SCKK_App.Requests;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for DatabaseLogList.xaml
    /// </summary>
    public partial class DatabaseLogList : UserControl
    {
        public DatabaseLogList(List<TableModel> tables)
        {
            InitializeComponent();
            DataGridList.ItemsSource = tables;
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as TableModel;
            var childCallList = new CallList(new DownloadRequest().DownloadLog(Dashboard.sessionCode, obj.title));
            this.Content = childCallList;

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as TableModel;
            var result = MessageBox.Show("Biztosan törölni szeretnéd?", obj.title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                new DownloadRequest().DeleteTable(Dashboard.sessionCode, obj.title);
                DataGridList.ItemsSource = new DownloadRequest().DownloadTableList(Dashboard.sessionCode);
            }
        }
    }
}

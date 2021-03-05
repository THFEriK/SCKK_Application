using SCKK_App.Models;
using SCKK_App.Requests;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for DatabaseLogList.xaml
    /// </summary>
    public partial class LogListView : UserControl
    {
        public LogListView(List<TableModel> tables)
        {
            InitializeComponent();
            DataGridList.ItemsSource = tables;
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as TableModel;
            var childCallList = new CallListView(new DownloadRequest().DownloadLog(Dashboard.sessionCode, obj.title));
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

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as TableModel;
            var childCallList = new CallListView(new DownloadRequest().DownloadLog(Dashboard.sessionCode, obj.title));
            this.Content = childCallList;
        }
    }
}

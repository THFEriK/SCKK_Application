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
    /// Interaction logic for CallMenu.xaml
    /// </summary>
    public partial class CallMenuView : UserControl
    {
        public CallMenuView()
        {
            InitializeComponent();
        }

        private void LocalFileBtn_Click(object sender, RoutedEventArgs e)
        {
            //var childCallList = new CallListView();
            //this.Content = childCallList;
        }

        private void ServerFileBtn_Click(object sender, RoutedEventArgs e)
        {
            List<TableModel> result = new DownloadRequest().DownloadTableList(Dashboard.sessionCode);

            if (!(result is null))
            {
                var childDatabaseLogList = new LogListView(new DownloadRequest().DownloadTableList(Dashboard.sessionCode));
                this.Content = childDatabaseLogList;
            }
        }
    }
}

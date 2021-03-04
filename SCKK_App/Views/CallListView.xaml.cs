using MaterialDesignThemes.Wpf;
using SCKK_App.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for CallList.xaml
    /// </summary>
    public partial class CallListView
    {
        private bool sidebarOpen = false;
        private bool isGrouped = false;
        readonly List<ResultModel> log;

        public CallListView(List<ResultModel> log)
        {
            InitializeComponent();
            DataGridStatistic.ItemsSource = log;
            this.log = log;
        }

        private void BarOpener_Click(object sender, RoutedEventArgs e)
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();

            if (sidebarOpen)
            {
                ButtonBar.Height = (GridLength)gridLengthConverter.ConvertFrom("0");
                BarOpenerIcon.Kind = PackIconKind.ArrowDropUp;

                sidebarOpen = false;
            }
            else
            {
                ButtonBar.Height = (GridLength)gridLengthConverter.ConvertFrom("60");

                BarOpenerIcon.Kind = PackIconKind.ArrowDropDown;
                sidebarOpen = true;
            }
        }

        private void Grouping_Click(object sender, RoutedEventArgs e)
        {
            if (isGrouped)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(DataGridStatistic.ItemsSource);
                view.GroupDescriptions.Clear();

                GrouperBtn.Content = "Csoportosítás";

                isGrouped = false;
            }
            else
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(DataGridStatistic.ItemsSource);
                view.GroupDescriptions.Add(new PropertyGroupDescription("name"));

                GrouperBtn.Content = "Alap rendezés";

                isGrouped = true;
            }
        }

        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            var UploadWindow = new UploadView(log);
            UploadWindow.Show();
        }
    }
}

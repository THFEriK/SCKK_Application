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
using MaterialDesignThemes.Wpf;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for KeysView.xaml
    /// </summary>
    public partial class KeysView : UserControl
    {
        private bool sidebarOpen = false;

        public KeysView(List<KeyModel> keys)
        {
            InitializeComponent();
            DataGridList.ItemsSource = keys;
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

        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            new KeyRequest().GenerateKey(Dashboard.sessionCode);
            DataGridList.ItemsSource = new KeyRequest().GetKeys(Dashboard.sessionCode);
        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as KeyModel;
            Clipboard.SetText(obj.registerKey);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as KeyModel;
            new KeyRequest().DeleteKey(Dashboard.sessionCode, obj.registerKey);
            DataGridList.ItemsSource = new KeyRequest().GetKeys(Dashboard.sessionCode);
        }
    }
}

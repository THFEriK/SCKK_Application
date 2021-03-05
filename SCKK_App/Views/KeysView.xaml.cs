using MaterialDesignThemes.Wpf;
using SCKK_App.Models;
using SCKK_App.Requests;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for KeysView.xaml
    /// </summary>
    public partial class KeysView : INotifyPropertyChanged
    {
        private bool sidebarOpen = false;

        public KeysView(List<KeyModel> keys)
        {
            DataContext = this;
            InitializeComponent();
            Keys = keys;
        }

        private List<KeyModel> _keys;
        public List<KeyModel> Keys
        {
            get { return _keys; }
            set
            {
                if (_keys != value)
                {
                    _keys = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            Keys = new KeyRequest().GetKeys(Dashboard.sessionCode);
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
            Keys = new KeyRequest().GetKeys(Dashboard.sessionCode);
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as KeyModel;
            Clipboard.SetText(obj.registerKey);
        }
    }
}

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

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for CallMenu.xaml
    /// </summary>
    public partial class CallMenu : UserControl
    {
        public CallMenu()
        {
            InitializeComponent();
        }

        private void LocalFileBtn_Click(object sender, RoutedEventArgs e)
        {
            CallList childCallList = new CallList();
            this.Content = childCallList;
        }

        private void ServerFileBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

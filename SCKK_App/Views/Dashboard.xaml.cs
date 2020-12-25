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
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private bool IsMenuMinimized = true;

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ListViewMenu.SelectedIndex) //Menüválasztó (Oldalsáv)\\
            {
                case 0:                         //Kezdőlap
                    GridPrincipal.Children.Clear();

                    break;
                case 1:                         //Hívások
                    GridPrincipal.Children.Clear();

                    CallList childCallList = new CallList();

                    GridPrincipal.Children.Add(childCallList);
                    break;
                case 2:                         //Jogok
                    GridPrincipal.Children.Clear();

                    //UserManager childUserManager = new UserManager();

                    //GridPrincipal.Children.Add(childUserManager);
                    break;
                default:
                    break;
            }
        }

        private void DrawerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsMenuMinimized)
            {
                sidebar.Width = new GridLength(165);
                SideBarOpenerIcon.Kind = PackIconKind.ArrowLeft;

                IsMenuMinimized = false;
            }
            else
            {
                sidebar.Width = new GridLength(48);
                SideBarOpenerIcon.Kind = PackIconKind.ArrowRight;
                IsMenuMinimized = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

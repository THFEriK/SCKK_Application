using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

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

            CallMenu childCallMenu = new CallMenu();
            GridPrincipal.Children.Add(childCallMenu);
        }

        public static string sessionCode = "";
        public static int rank = 1;


        private bool IsMenuMinimized = true;

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (DateTime.Now.ToUniversalTime() > new DateTime(2021,3,1))
            {
                MessageBox.Show("Frissítse a programot!");
            }
            */
            switch (ListViewMenu.SelectedIndex) //Menüválasztó (Oldalsáv)\\
            {
                case 0:                         //Felhasználó
                    GridPrincipal.Children.Clear();

                    UserManager childUserManager = new UserManager();

                    GridPrincipal.Children.Add(childUserManager);

                    break;
                case 1:                         //Hívások
                    GridPrincipal.Children.Clear();

                    CallMenu childCallMenu = new CallMenu();

                    GridPrincipal.Children.Add(childCallMenu);
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

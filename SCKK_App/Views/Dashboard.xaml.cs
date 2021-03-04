using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using SCKK_App.Models;
using SCKK_App.Requests;
using SCKK_App.Controllers;
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

            string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/SCKK-APP/data.xml";
            if (File.Exists(filePath))
            {
                var fs = new FileStream(filePath, FileMode.Open);
                var sr = new StreamReader(fs);
                sessionCode = sr.ReadLine();
                sr.Close();
                fs.Close();

                rank = int.Parse(new AuthenticationRequest().AutoLogin(sessionCode));
                if (rank is 0)
                {
                    sessionCode = String.Empty;
                    File.Delete(filePath);
                }
            }

            string _version = new AuthenticationRequest().GetVersion();
            if (version != _version && _version != null)
            {
                rank = 0;
                MessageBox.Show("A program ezen a verziója elavult, így az online funkciókat kikapcsoltuk. Kérlek frissítsd a programot!", "Régi verzió", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Reload();
            }
        }

        public void Reload()
        {
            if (rank >= 1) 
            {
                DatabaseLv.Visibility = Visibility.Visible; 
            }
            if (rank >= 12)
            {
                UsersLv.Visibility = Visibility.Visible;
                RegKeysLv.Visibility = Visibility.Visible;
            }
        }

        public static string version = "1.0";
        public static string sessionCode = string.Empty;
        public static int rank = 0;

        private bool IsMenuMinimized = true;

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ListViewMenu.SelectedIndex) //Menüválasztó (Oldalsáv)\\
            {
                case -1:
                    Reload();
                    break;
                case 0:                         //Főoldal
                    GridPrincipal.Children.Clear();

                    var childHome = new HomeView();
                    GridPrincipal.Children.Add(childHome);

                    ListViewMenu.SelectedIndex = -1;
                    break;

                case 1:                         //Beolvasás
                    GridPrincipal.Children.Clear();

                    var reading = new FileDataController();
                    reading.FileRead();
                    reading.FileCompletion();
                    var childCallMenu = new CallListView(reading.results);
                    GridPrincipal.Children.Add(childCallMenu);

                    ListViewMenu.SelectedIndex = -1;
                    break;

                case 2:                         //Adatbázis
                    GridPrincipal.Children.Clear();
                    var tables = new DownloadRequest().DownloadTableList(Dashboard.sessionCode);

                    if (!(tables is null))
                    {
                        var childLogList = new LogListView(tables);
                        GridPrincipal.Children.Add(childLogList);
                    }

                    ListViewMenu.SelectedIndex = -1;
                    break;

                case 3:                         //Felhasználók
                    GridPrincipal.Children.Clear();
                    var users = new UserManagerRequest().GetUsers(Dashboard.sessionCode);

                    if (!(users is null))
                    {
                        var childUsers = new UserList(users);
                        GridPrincipal.Children.Add(childUsers);
                    }

                    ListViewMenu.SelectedIndex = -1;
                    break;

                case 4:                         //RegKeys
                    GridPrincipal.Children.Clear();
                    var keys = new KeyRequest().GetKeys(Dashboard.sessionCode);

                    if (!(keys is null))
                    {
                        var childUsers = new KeysView(keys);
                        GridPrincipal.Children.Add(childUsers);
                    }

                    ListViewMenu.SelectedIndex = -1;
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

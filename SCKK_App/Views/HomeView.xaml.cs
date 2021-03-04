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
using SCKK_App.Requests;
using SCKK_App.Models;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            fillData();
        }

        private void fillData()
        {
            MessageTb.Text = new HomeRequest().GetMessage();

            if (Dashboard.rank != 0)
            {
                var userData = new HomeRequest().GetUserData(Dashboard.sessionCode);

                LoginSP.Visibility = Visibility.Collapsed;
                StatsGB.Header = $"  Üdv {userData[0].username}!";
                StatsLB.Items.Clear();
                StatsLB.Items.Add($"Rang:\n {userData[0].rank}");
                StatsLB.Items.Add($"Csapat:\n {userData[0].team}");
                StatsLB.Items.Add($"Felöltés:\n {userData[0].uploads} db");
                StatsSP.Visibility = Visibility.Visible;
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var loginV = new LoginView();
            loginV.ShowDialog();
            fillData();
        }
    }
}

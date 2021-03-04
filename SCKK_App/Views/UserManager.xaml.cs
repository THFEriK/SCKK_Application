using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SCKK_App.Models;
using SCKK_App.Requests;
using System.Linq;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for UserManager.xaml
    /// </summary>
    public partial class UserManager : Window
    {
        private int UserID;

        public UserManager(int id, string username, int rankID, int groupID)
        {
            InitializeComponent();
            usernameTB.Text = username;
            RankCb.ItemsSource = new UserManagerRequest().GetRanks(Dashboard.sessionCode);
            RankCb.SelectedValue = rankID;
            GroupCb.ItemsSource = new UserManagerRequest().GetGroups(Dashboard.sessionCode);
            GroupCb.SelectedValue = groupID;
            UserID = id;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            new UserManagerRequest().SetUser(Dashboard.sessionCode, UserID.ToString(), RankCb.SelectedValue.ToString(), GroupCb.SelectedValue.ToString());
            this.Close();
        }
    }
}

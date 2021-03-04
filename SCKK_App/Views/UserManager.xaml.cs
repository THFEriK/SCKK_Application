using SCKK_App.Requests;
using System.Windows;

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

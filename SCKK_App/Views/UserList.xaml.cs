using SCKK_App.Models;
using SCKK_App.Requests;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserList : UserControl
    {
        public UserList(List<UserModel> users)
        {
            InitializeComponent();
            DataGridList.ItemsSource = users;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as UserModel;
            var userV = new UserManager(obj.id ,obj.username, obj.rankID, obj.groupID);
            userV.ShowDialog();
            DataGridList.ItemsSource = new UserManagerRequest().GetUsers(Dashboard.sessionCode);
        }
    }
}

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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserList : INotifyPropertyChanged
    {
        public UserList(List<UserModel> users)
        {
            DataContext = this;
            InitializeComponent();
            Users = users;
        }

        private List<UserModel> _users;
        public List<UserModel> Users
        {
            get { return _users; }
            set
            { 
                if(_users != value)
                {
                    _users = value;
                    OnPropertyChanged();
                } 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as UserModel;
            var userV = new UserManager(obj.id ,obj.username, obj.rankID, obj.groupID);
            userV.ShowDialog();
            Users = new UserManagerRequest().GetUsers(Dashboard.sessionCode);
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as UserModel;
            var userV = new UserManager(obj.id, obj.username, obj.rankID, obj.groupID);
            userV.ShowDialog();
            Users = new UserManagerRequest().GetUsers(Dashboard.sessionCode);
        }
    }
}

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

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserManager : UserControl
    {
        private readonly AuthenticationRequest request = new AuthenticationRequest();

        public UserManager()
        {
            InitializeComponent();
        }

        private void LoginUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginUsername.Text == "Felhasználónév")
            {
                LoginUsername.Text = "";
                LoginUsername.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void LoginPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginPasswordText.Text = "";
        }

        private void RegisterUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RegisterUsername.Text == "Felhasználónév")
            {
                RegisterUsername.Text = "";
                RegisterUsername.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void RegisterPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RegisterPasswordText.Text == "Jelszó")
            {
                RegisterPasswordText.Text = "";
            }
        }

        private void RegisterPassword2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RegisterPassword2Text.Text == "Jelszó ismét")
            {
                RegisterPassword2Text.Text = "";
            }
        }

        private void RegisterKey_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RegisterKey.Text == "Regisztrációs kulcs")
            {
                RegisterKey.Text = "";
             RegisterKey.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void LoginUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginUsername.Text == "")
            {
                LoginUsername.Text = "Felhasználónév";
             LoginUsername.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void LoginPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginPassword.Password == "")
            {
                LoginPasswordText.Text = "Jelszó";
            }
        }

        private void RegisterUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RegisterUsername.Text == "")
            {
                RegisterUsername.Text = "Felhasználónév";
                RegisterUsername.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void RegisterPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RegisterPassword.Password == "")
            {
                RegisterPasswordText.Text = "Jelszó";
            }
        }

        private void RegisterPassword2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RegisterPassword2.Password == "")
            {
                RegisterPassword2Text.Text = "Jelszó ismét";
            }
        }

        private void RegisterKey_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RegisterKey.Text == "")
            {
                RegisterKey.Text = "Regisztrációs kulcs";
                RegisterKey.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginUser();
        }

        private void LoginUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginUser();
            }
        }

        private void LoginPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginUser();
            }
        }

        private void LoginUser()
        {
            string[] s = request.Login(LoginUsername.Text, LoginPassword.Password);
            Dashboard.sessionCode = s[1];
            Dashboard.rank = int.Parse(s[2]);

            MessageBox.Show(Dashboard.sessionCode + ", " + Dashboard.rank);
        }

        private void RegisterUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                request.Registration(RegisterKey.Text, RegisterUsername.Text, RegisterPassword.Password, RegisterPassword2.Password);
            }
        }

        private void RegisterPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                request.Registration(RegisterKey.Text, RegisterUsername.Text, RegisterPassword.Password, RegisterPassword2.Password);
            }
        }

        private void RegisterPassword2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                request.Registration(RegisterKey.Text, RegisterUsername.Text, RegisterPassword.Password, RegisterPassword2.Password);
            }
        }

        private void RegisterKey_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                request.Registration(RegisterKey.Text, RegisterUsername.Text, RegisterPassword.Password, RegisterPassword2.Password);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            request.Registration(RegisterKey.Text, RegisterUsername.Text, RegisterPassword.Password, RegisterPassword2.Password);
        }
    }
}

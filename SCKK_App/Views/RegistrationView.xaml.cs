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
using SCKK_App.Requests;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        private readonly AuthenticationRequest request = new AuthenticationRequest();

        public RegistrationView()
        {
            InitializeComponent();
        }

        private void UsernameTb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTb.Text == "Felhasználónév")
            {
                UsernameTb.Text = string.Empty;
                UsernameTb.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void UsernameTb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTb.Text == string.Empty)
            {
                UsernameTb.Text = "Felhasználónév";
                UsernameTb.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void PasswordTb_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordText.Visibility = Visibility.Collapsed;
        }

        private void PasswordTb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTb.Password == string.Empty)
            {
                PasswordText.Visibility = Visibility.Visible;
            }
        }

        private void Password2Tb_GotFocus(object sender, RoutedEventArgs e)
        {
            Password2Text.Visibility = Visibility.Collapsed;
        }

        private void Password2Tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTb.Password == string.Empty)
            {
                Password2Text.Visibility = Visibility.Visible;
            }
        }

        private void RegKeyTb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RegKeyTb.Text == "Kulcs")
            {
                RegKeyTb.Text = string.Empty;
                RegKeyTb.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void RegKeyTb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RegKeyTb.Text == string.Empty)
            {
                RegKeyTb.Text = "Kulcs";
                RegKeyTb.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void Registration()
        {
            if(request.Registration(RegKeyTb.Text, UsernameTb.Text, PasswordTb.Password, Password2Tb.Password))
                this.Close();
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            Registration();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Registration();
            }
        }
    }
}

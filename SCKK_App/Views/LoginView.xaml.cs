using SCKK_App.Requests;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly AuthenticationRequest request = new AuthenticationRequest();

        public LoginView()
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

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginUser();
        }

        private void LoginUser()
        {
            string[] s = request.Login(UsernameTb.Text, PasswordTb.Password);
            if (!(s is null))
            {
                Dashboard.sessionCode = s[1];
                Dashboard.rank = int.Parse(s[2]);
                if (LoginCb.IsChecked.Value)
                {
                    var file = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/SCKK-APP/data.xml";
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(file));

                    var fs = new FileStream(file, FileMode.Create);
                    var sw = new StreamWriter(fs);

                    sw.Write(Dashboard.sessionCode);

                    sw.Close();
                    fs.Close();
                }
                this.Close();
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginUser();
            }
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            var regWindow = new RegistrationView();
            regWindow.ShowDialog();
        }
    }
}

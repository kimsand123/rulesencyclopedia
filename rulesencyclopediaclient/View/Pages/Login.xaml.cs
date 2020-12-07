using System;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System.Net.Http.Headers;
using rulesencyclopediaclient.Singletons;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Input;
using rulesencyclopediaclient.Pouch;
using rulesencyclopediaclient.Tools;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Net;

namespace rulesencyclopediaclient.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        CommunicationElements comElements = new CommunicationElements();
        InterfaceAnimation interfaceAnim = new InterfaceAnimation();

        public Login()
        {
            InitializeComponent();
            this.userNameBox.Focus();
        }

          protected void Login_ClickAsync(object sender, RoutedEventArgs args)
        {
            //Getting a response from server with the Login credentials
            var response = comElements.get("Login", "UserName=" + this.userNameBox.Text + "&Password=" + this.passwordBox.Password.ToString(), "");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                //removing escape character from result.
                string result = response.Content.ReadAsStringAsync().Result.Replace("\"", "");

                //store token in SettingsAndData singleton for futher use.
                SettingsAndData.Instance.token = result;

                //Remove Login page from memory
                NavigationService.Navigate(null);

                //Change to MainInfoWindow window using MainWindowState singleton
                //Change menustate using MainWindowState singleton
                MainWindowState.Instance.changePageInFrame(new MainInfoWindow());
                MainWindowState.Instance.changeMenuState("loggedon");
            } else
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    //If user does not exist ask if user wants to create new user.
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult diagResult = MessageBox.Show("User not found. Do you want to create a new user", "User not found", buttons, MessageBoxIcon.Warning);
                    if (diagResult == DialogResult.Yes) {
                        MainWindowState.Instance.changePageInFrame(new CreateUser());
                        MainWindowState.Instance.changeMenuState("createuser");
                    }
                }
            
                //If username is already used.
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Password is wrong. Try again", "Password wrong", buttons, MessageBoxIcon.Warning);
                    this.passwordBox.Focus();
                }
            }
        }

        //Pressing the return key is the same as clicking the button
        private void Keydown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RoutedEventArgs z = new RoutedEventArgs();
            if (e.Key==Key.Return || e.Key==Key.Enter)
            {
                Login_ClickAsync(this, z);
            }
        }

        //Animations
        private void txtBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox)
            {
                interfaceAnim.animateTextBox(sender as System.Windows.Controls.TextBox, "UP");
            }

            if (sender is PasswordBox)
            {
                interfaceAnim.animateTextBox(sender as PasswordBox, "UP");
            }
        }

        private void txtBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox)
            {
                interfaceAnim.animateTextBox(sender as System.Windows.Controls.TextBox, "DOWN");
            }

            if (sender is PasswordBox)
            {
                interfaceAnim.animateTextBox(sender as PasswordBox, "DOWN");
            }
        }
    }
}

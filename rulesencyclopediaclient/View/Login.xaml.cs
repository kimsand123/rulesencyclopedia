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
            //get values from textfields.
            string userName = this.userNameBox.Text;
            string password = this.passwordBox.Password.ToString();

            //TODO: Check for business rules concerning username and password.
            //HttpClient client = comElements.getClient();
            //setting address and port for the service.
            //UriBuilder uriBuilder = new UriBuilder("https://" + SettingsAndData.Instance.apiAddress + ":" + SettingsAndData.Instance.portNr + "/api/Login");
            //send values to api/login as parameters;
            //uriBuilder.Query = "UserName=" + userName + "&Password=" + password;
            //recieve token

            var response = comElements.get("Login", "UserName=" + userName + "&Password=" + password,"");
            //var response = client.GetAsync(uriBuilder.Uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = response.Content.ReadAsStringAsync().Result.Replace("\"", "");
                //store token
                SettingsAndData.Instance.token = result;
                MainWindowState.Instance.changePageInFrame(new MainInfoWindow());
                MainWindowState.Instance.changeMenuState("logon");
            } else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    MainWindowState.Instance.changePageInFrame(new CreateUser());
                    MainWindowState.Instance.changeMenuState("createuser");
                }
            
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Password is wrong. Try again", "Password wrong", buttons, MessageBoxIcon.Warning);
                    this.passwordBox.Focus();
                }
            }
        }

        private void Keydown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Pressing the return key is the same as clicking the button
            RoutedEventArgs z = new RoutedEventArgs();
            if (e.Key==Key.Return || e.Key==Key.Enter)
            {
                Login_ClickAsync(this, z);
            }
        }

        private void txtBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox)
            {
                interfaceAnim.animateTextBox(sender as System.Windows.Controls.TextBox, "UP");
            }

            if (sender is PasswordBox)
            {
                interfaceAnim.animateTextBox(sender as System.Windows.Controls.PasswordBox, "UP");
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
                interfaceAnim.animateTextBox(sender as System.Windows.Controls.PasswordBox, "DOWN");
            }
        }
    }
}

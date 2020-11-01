using System;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System.Net.Http.Headers;
using rulesencyclopediaclient.Singletons;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace rulesencyclopediaclient.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        public Login()
        {
            InitializeComponent();
        }

        protected void Login_ClickAsync(object sender, RoutedEventArgs args)
        {
            //get values from textfields.
            string userName = this.userNameBox.Text;
            string password = this.passwordBox.Password.ToString();

            //TODO: Check for business rules concerning username and password.

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //setting address and port for the service.
            UriBuilder uriBuilder = new UriBuilder("https://" + Pouch.SettingsAndData.Instance.apiAddress + ":" + Pouch.SettingsAndData.Instance.portNr + "/api/Login");
            //send values to api/login as parameters;
            uriBuilder.Query = "UserName=" + userName + "&Password=" + password;
            //recieve token
            var response = client.GetAsync(uriBuilder.Uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = response.Content.ReadAsStringAsync().Result.Replace("\"", "");
                //store token
                Pouch.SettingsAndData.Instance.token = result;
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
    }
}

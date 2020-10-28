using Newtonsoft.Json;
using rulesencyclopediaclient.Models;
using rulesencyclopediaclient.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace rulesencyclopediaclient.View
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Page
    {
        public CreateUser()
        {
            InitializeComponent();
        }

        private void btnCreateUser_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (checkPasswordConfirmation()){
            string userName = this.txtBoxUserName.Text;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //setting address and port for the service.
            UriBuilder uriBuilder = new UriBuilder("https://" + Pouch.SettingsAndData.Instance.apiAddress + ":" + Pouch.SettingsAndData.Instance.portNr + "/api/User");
            //send values to api/login as parameters;
            uriBuilder.Query = "UserName=" + userName;
            var response = client.GetAsync(uriBuilder.Uri).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Found)
                {
                    //If username is already in use
                    this.txtBoxUserName.Text = "";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Username is already in use. Try another username", "Username already in use", buttons, MessageBoxIcon.Warning);
                    this.txtBoxUserName.Focus();
                }
                else
                {
                    //If username is not in use
                    UserDTO user = new UserDTO();
                    user.FirstName = this.txtBoxFirstName.Text;
                    user.MiddleName = this.txtBoxMiddleName.Text;
                    user.LastName = this.txtBoxLastName.Text;
                    user.UserName = this.txtBoxUserName.Text;
                    user.Password = this.pswBoxPassword.Password.ToString();
                    user.Date = DateTime.Now;

                    uriBuilder = new UriBuilder("https://" + Pouch.SettingsAndData.Instance.apiAddress + ":" + Pouch.SettingsAndData.Instance.portNr + "/api/User");
                    //send values to api/login as parameters;   
                    string userJson = JsonConvert.SerializeObject(user);
                    var content = new StringContent(userJson, UnicodeEncoding.UTF8, "application/json");
                    var result = client.PostAsJsonAsync(uriBuilder.Uri, user).Result;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("User Created", "User Created", buttons);
                    //Change to login window
                    MainWindowState.Instance.changeMenuState("login");
                    MainWindowState.Instance.changePageInFrame(new Login());
                }
            }           
        }

        private bool checkPasswordConfirmation()
        {
            if (this.pswBoxConfirmPassworm.Password.ToString() != this.pswBoxPassword.Password.ToString())
            {
                this.pswBoxPassword.Password = "";
                this.pswBoxConfirmPassworm.Password = "";

                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Password is not confirmed. Try again", "Password not confirmed", buttons, MessageBoxIcon.Warning);
                this.pswBoxPassword.Focus();
                return false;
            }
            return true;
        }
    }
}

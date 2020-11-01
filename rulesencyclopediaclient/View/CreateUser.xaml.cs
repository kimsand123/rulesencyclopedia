using rulesencyclopediaclient.Models;
using rulesencyclopediaclient.Singletons;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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
            //if the password confirmation is OK
            if (checkPasswordConfirmation()){
            string userName = this.txtBoxUserName.Text;

            //Start process by asking the backend if the username already exists.

            //create the HttpClient with header
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Build the URI for the proper endpoint.
            UriBuilder uriBuilder = new UriBuilder("https://" + Pouch.SettingsAndData.Instance.apiAddress + ":" + Pouch.SettingsAndData.Instance.portNr + "/api/User");
            //send username as queryparameter to api/user;
            uriBuilder.Query = "UserName=" + userName;
            //make the Http request and recieve the reponse.
            var response = client.GetAsync(uriBuilder.Uri).Result;
                // if username already exists
                if (response.StatusCode == System.Net.HttpStatusCode.Found)
                {
                    //give message to user and make him try again with another username.
                    this.txtBoxUserName.Text = "";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Username is already in use. Try another username", "Username already in use", buttons, MessageBoxIcon.Warning);
                    this.txtBoxUserName.Focus();
                }
                else
                {
                    //If username is not in use create a new UserDTO object
                    UserDTO user = new UserDTO();
                    user.FirstName = this.txtBoxFirstName.Text;
                    user.MiddleName = this.txtBoxMiddleName.Text;
                    user.LastName = this.txtBoxLastName.Text;
                    user.UserName = this.txtBoxUserName.Text;
                    user.Password = this.pswBoxPassword.Password.ToString();
                    user.Date = DateTime.Now;

                    uriBuilder = new UriBuilder("https://" + Pouch.SettingsAndData.Instance.apiAddress + ":" + Pouch.SettingsAndData.Instance.portNr + "/api/User");
                    //send user object to api/login as request body;   
                    // TEST WITHOUT string userJson = JsonConvert.SerializeObject(user);
                    // TEST WITHOUT var content = new StringContent(userJson, UnicodeEncoding.UTF8, "application/json");
                    
                    //Make post request
                    //TODO: Exception handling.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

using System.Net.Http;
using System.Security.Policy;
using System.Net.Http.Headers;
using MySqlX.XDevAPI.Common;

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
            string userName = this.userNameBox.Text;
            string password = this.passwordBox.Password.ToString();

            //You could check for business rules concerning username and password.

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //her sætter vi addressen og porten for API servicen
            UriBuilder uriBuilder = new UriBuilder("https://" + Pouch.Pouch.Instance.apiAddress + ":" + Pouch.Pouch.Instance.portNr + "/");
            uriBuilder.Query = "userName=" + userName + "&password=" + password;
        
            var response = client.GetAsync(uriBuilder.Uri).Result;
            var result = response.Content.ReadAsStringAsync();

            //get values from textfields.
            //send values to api/login as parameters;
            //recieve token
            //store token

        }
    }
}

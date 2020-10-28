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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            string userName = this.txtBoxUserName.Text;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //setting address and port for the service.
            UriBuilder uriBuilder = new UriBuilder("https://" + Pouch.SettingsAndData.Instance.apiAddress + ":" + Pouch.SettingsAndData.Instance.portNr + "/api/users/");
            //send values to api/login as parameters;
            uriBuilder.Query = "UserName=" + userName;
            //Check for username. If it is already in use, present a modal, 
            //erase the username and set focus to the username box
        }
    }
}

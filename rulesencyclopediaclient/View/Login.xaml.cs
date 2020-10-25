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
using rulesencyclopediaclient.Tools;

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

        protected void Login_Click(object sender, RoutedEventArgs args)
        {
            string userName = this.userNameBox.Text;
            string password = this.passwordBox.Text;

            APIConnect connection = new APIConnect();

            //get values from textfields.
            //send values to api/login as parameters;
            //recieve token
            //store token

        }
    }
}

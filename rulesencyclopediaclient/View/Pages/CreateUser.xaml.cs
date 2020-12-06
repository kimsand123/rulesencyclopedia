using rulesencyclopediaclient.Models;
using rulesencyclopediaclient.Pouch;
using rulesencyclopediaclient.Singletons;
using rulesencyclopediaclient.Tools;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace rulesencyclopediaclient.View
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Page
    {
        InterfaceAnimation interfaceAnim = new InterfaceAnimation();
        public CreateUser()
        {
            InitializeComponent();
        }

        private void btnCreateUser_ClickAsync(object sender, RoutedEventArgs e)
        {
            //if the password confirmation is OK
            if (checkIfPasswordAlike()){
            string userName = this.txtBoxUserName.Text;

            //Start process by asking the backend if the username already exists.

            CommunicationElements comElements = new CommunicationElements();
            // make the Http request and recieve the reponse.

            var response = comElements.get("User", "UserName=" + userName, "");

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

                   response = comElements.post("User", user);
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show("User Created", "User Created", buttons);

                        //Change to login window using MainWindowState singleton
                        //Change menustate using MainWindowState singleton
                        NavigationService.Navigate(null);
                        MainWindowState.Instance.changePageInFrame(new Login());
                        MainWindowState.Instance.changeMenuState("login");
                    }
                    else
                    {
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show("User not Created", "Database Incident", buttons);
                    }
                }
            }           
        }

        private bool checkIfPasswordAlike()
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

        //Pressing the return key is the same as clicking the button
        private void Keydown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RoutedEventArgs z = new RoutedEventArgs();
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                //CHECK THAT ALL OBLIGATORY DATA IS FILLED OUT.
                btnCreateUser_ClickAsync(this, z);
            }
        }

        //ANIMATIONS
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

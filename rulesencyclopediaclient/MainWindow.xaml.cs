using rulesencyclopediaclient.Singletons;
using rulesencyclopediaclient.Tools;
using rulesencyclopediaclient.View;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;


namespace rulesencyclopediaclient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CommunicationElements comElement = new CommunicationElements();
        public MainWindow()
        {
            InitializeComponent();
            var response = comElement.checkConnection();

            //When application starts it checks to see if the service is up.
            //If it aint, it shows a box and explains, and then shuts down.
            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Service is not up. Contact s@ndberg.dk or go to www.blablabla.com, for further information", "Server incident", buttons);
                ExitProgram_Click(this, new RoutedEventArgs());
            }
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
        public void CallLoginPage(object sender, RoutedEventArgs args)
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;

            //Use MainWindowState singleton to change menustate
            MainWindowState.Instance.changeMenuState("logon");
            //Use MainWindowState singleton to insert a new page Login at the MainFrame Frame.
            MainWindowState.Instance.changePageInFrame(new Login());
        }

        public void ExitProgram_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }


        public void CallLogoffPage(object sender, RoutedEventArgs args)
        {
            //Use MainWindowState singleton to change menustate
            MainWindowState.Instance.changeMenuState("logoff");
            //Use MainWindowState singleton to insert a new page Login at the MainFrame Frame.
            MainWindowState.Instance.changePageInFrame(new Login());
        }

        public void CallProfileHandling(object sender, RoutedEventArgs args)
        {
            //Not implemented
        }

        public void CallSettingsPage(object sender, RoutedEventArgs args)
        {
            //Not implemented
        }

        public void CallHelpPage(object sender, RoutedEventArgs args)
        {
            //Not implemented
        }

        public void CallCreateUser(object sender, RoutedEventArgs args)
        {
            //Use MainWindowState singleton to change menustate
            MainWindowState.Instance.changeMenuState("createuser");
            //Use MainWindowState singleton to insert a new page CreateUser at the MainFrame Frame.
            MainWindowState.Instance.changePageInFrame(new CreateUser());
        }
    }

}

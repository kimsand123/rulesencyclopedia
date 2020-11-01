using rulesencyclopediaclient.Singletons;
using rulesencyclopediaclient.View;
using System.Windows;


namespace rulesencyclopediaclient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
        public void CallLoginPage(object sender, RoutedEventArgs args)
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            MainWindowState.Instance.changePageInFrame(new Login());
        }

        public void ExitProgram_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }


        public void CallLogoffPage(object sender, RoutedEventArgs args)
        {
            //TODO: close connection
            MainWindowState.Instance.changeMenuState("logoff");
            MainWindowState.Instance.changePageInFrame(new Login());
        }

        public void CallProfileHandling(object sender, RoutedEventArgs args)
        {

        }

        public void CallSettingsPage(object sender, RoutedEventArgs args)
        {

        }

        public void CallHelpPage(object sender, RoutedEventArgs args)
        {

        }

        public void CallCreateUser(object sender, RoutedEventArgs args)
        {
            MainWindowState.Instance.changeMenuState("createuser");
            MainWindowState.Instance.changePageInFrame(new CreateUser());
        }
    }

}

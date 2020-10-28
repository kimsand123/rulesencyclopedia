using rulesencyclopediaclient.Singletons;
using rulesencyclopediaclient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

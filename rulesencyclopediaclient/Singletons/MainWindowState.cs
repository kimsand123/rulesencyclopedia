using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace rulesencyclopediaclient.Singletons
{
    class MainWindowState
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;
        private MainWindowState()
        {
        }
        public static MainWindowState Instance { get { return get.instance; } }

        private class get
        {
            static get()
            {
            }
            internal static readonly MainWindowState instance = new MainWindowState();
        }
        public void changeMenuState(string menuSelection)
        {
            switch (menuSelection)
            {
                case "logon":
            //enabled or not
                    //window.Login.IsEnabled = false;
                    //window.Exit.IsEnabled = false;
                    //window.Logoff.IsEnabled = true;
                    //window.Profile.IsEnabled = true;
                    //window.CreateUser.IsEnabled = true;
                    //window.OtherPoint.IsEnabled = true;
                    //window.Settings.IsEnabled = true;
                    //window.Help.IsEnabled = true;
            //Visible or not
                    window.Login.Visibility = Visibility.Hidden;
                    window.Exit.Visibility = Visibility.Hidden;
                    window.Logoff.Visibility = Visibility.Visible;
                    window.Profile.Visibility = Visibility.Visible;
                    window.CreateUser.Visibility = Visibility.Hidden;
                    window.OtherPoint.Visibility = Visibility.Visible;
                    window.Settings.Visibility = Visibility.Visible;
                    window.Help.Visibility = Visibility.Visible;
                    break;
                case "logoff":
                    /*window.Login.IsEnabled = true;
                    window.Exit.IsEnabled = true;
                    window.Logoff.IsEnabled = false;
                    window.Profile.IsEnabled = false;
                    window.CreateUser.IsEnabled = false;
                    window.OtherPoint.IsEnabled = false;
                    window.Settings.IsEnabled = false;
                    window.Help.IsEnabled = false;*/
                    window.Login.Visibility = Visibility.Visible;
                    window.Exit.Visibility = Visibility.Visible;
                    window.Logoff.Visibility = Visibility.Hidden;
                    window.Profile.Visibility = Visibility.Hidden;
                    window.CreateUser.Visibility = Visibility.Visible;
                    window.OtherPoint.Visibility = Visibility.Visible;
                    window.Settings.Visibility = Visibility.Hidden;
                    window.Help.Visibility = Visibility.Visible;
                    break;
                case "createuser":
                    window.Login.Visibility = Visibility.Hidden;
                    window.Exit.Visibility = Visibility.Visible;
                    window.Logoff.Visibility = Visibility.Hidden;
                    window.Profile.Visibility = Visibility.Hidden;
                    window.CreateUser.Visibility = Visibility.Hidden;
                    window.OtherPoint.Visibility = Visibility.Visible;
                    window.Settings.Visibility = Visibility.Hidden;
                    window.Help.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void changePageInFrame(Page page)
        {
            window.MainFrame.Content = page;
        }
    }
}




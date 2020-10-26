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
                    window.Login.IsEnabled = false;
                    window.Exit.IsEnabled = false;
                    window.Logoff.IsEnabled = true;
                    window.Profile.IsEnabled = true;
                    window.OtherPoint.IsEnabled = true;
                    window.Settings.IsEnabled = true;
                    window.Help.IsEnabled = true;
                    break;
                case "logoff":
                    window.Login.IsEnabled = true;
                    window.Exit.IsEnabled = true;
                    window.Logoff.IsEnabled = false;
                    window.Profile.IsEnabled = false;
                    window.OtherPoint.IsEnabled = false;
                    window.Settings.IsEnabled = false;
                    window.Help.IsEnabled = false;
                    break;
            }
        }

        public void changePageInFrame(Page page)
        {
            window.MainFrame.Content = page;
        }
    }
}




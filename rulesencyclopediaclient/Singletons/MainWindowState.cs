using System.Windows;
using System.Windows.Controls;

namespace rulesencyclopediaclient.Singletons
{
    class MainWindowState
    {
        //Sets window to be the MainWindow object of the application.
        //So that the different pages can be loaded to the Frame "MainFrame"
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
            //Control of the different menu elements in regards to which page is loaded into the Frame.
            switch (menuSelection)
            {
                case "logon":
            //enabled or not
                    window.Login.IsEnabled = false;
                    window.Exit.IsEnabled = true;
                    window.Logoff.IsEnabled = false;
                    window.Profile.IsEnabled = false;
                    window.CreateUser.IsEnabled = true;
                    window.OtherPoint.IsEnabled = true;
                    window.Settings.IsEnabled = true;
                    window.Help.IsEnabled = true;
                    break;
                case "logoff":
                    window.Login.IsEnabled = true;
                    window.Exit.IsEnabled = true;
                    window.Logoff.IsEnabled = false;
                    window.Profile.IsEnabled = false;
                    window.CreateUser.IsEnabled = true;
                    window.OtherPoint.IsEnabled = false;
                    window.Settings.IsEnabled = false;
                    window.Help.IsEnabled = true;
                    break;
                case "createuser":
                    window.Login.IsEnabled = true;
                    window.Exit.IsEnabled = true;
                    window.Logoff.IsEnabled = false;
                    window.Profile.IsEnabled = false;
                    window.CreateUser.IsEnabled = false;
                    window.OtherPoint.IsEnabled = true;
                    window.Settings.IsEnabled = false;
                    window.Help.IsEnabled = true;
                    break;
                case "loggedon":
                    window.Login.IsEnabled = false;
                    window.Exit.IsEnabled = false;
                    window.Logoff.IsEnabled = true;
                    window.Profile.IsEnabled = true;
                    window.CreateUser.IsEnabled = false;
                    window.OtherPoint.IsEnabled = true;
                    window.Settings.IsEnabled = false;
                    window.Help.IsEnabled = true;
                    break;
            }
        }

        public void changePageInFrame(Page page)
        {
            //Setting the MainFrame of the window object to the Passed in Page object.
            window.MainFrame.Content = page;
        }
    }
}




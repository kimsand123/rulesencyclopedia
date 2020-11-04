using rulesencyclopediaclient.Singletons;
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
using rulesencyclopediaclient.Pouch;
using rulesencyclopediaclient.Tools;
using rulesencyclopediabackend.Models;
using Newtonsoft.Json;
using rulesencyclopediaclient.Model.View;
using System.Collections.ObjectModel;

namespace rulesencyclopediaclient.View
{
    /// <summary>
    /// Interaction logic for MainInfoWindow.xaml
    /// </summary>
    public partial class MainInfoWindow : Page
    {
        CommunicationElements comElements = new CommunicationElements();
        public MainInfoWindow()
        {
            InitializeComponent();
            List<GameDTO> gamesList;
            HttpClient client = comElements.getClient();
            //setting address and port for the service.
            Uri uri = comElements.getUri("Game");
            var response = client.GetAsync(uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ObservableCollection<GameView> gameListView = new ObservableCollection<GameView>();
                var content = response.Content.ReadAsStringAsync();
                gamesList = JsonConvert.DeserializeObject<List<GameDTO>>(content.Result);
                foreach (GameDTO game in gamesList)
                {
                    new GameView() { Name = game.Name, Company = game.Company, Revision = game.Revision };
                }
            }
        }
        private void GamesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

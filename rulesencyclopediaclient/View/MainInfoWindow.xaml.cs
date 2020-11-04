using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Controls;
using rulesencyclopediaclient.Tools;
using rulesencyclopediabackend.Models;
using Newtonsoft.Json;
using rulesencyclopediaclient.Model.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Runtime.Remoting.Channels;

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
                GamesListBox.ItemsSource = gameListView;
                var content = response.Content.ReadAsStringAsync();
                gamesList = JsonConvert.DeserializeObject<List<GameDTO>>(content.Result);
                foreach (GameDTO game in gamesList)
                {
                    gameListView.Add(new GameView() { Id=game.Id, Name = game.Name, Company = game.Company, Revision = game.Revision });
                } 
            }
        }
        
        private void GamesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameView selectedGame = new GameView();
            selectedGame = (GameView)e.AddedItems[0];
            //Get Tocs on the basis of the game
            List<TOCDTO> tocList;
            HttpClient client = comElements.getClient();
            Uri uri = comElements.getUri("TOC");
            var response = client.GetAsync(uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ObservableCollection<TOCView> tocListView = new ObservableCollection<TOCView>();
                TOCListBox.ItemsSource = tocListView;
                var content = response.Content.ReadAsStringAsync();
                tocList = JsonConvert.DeserializeObject<List<TOCDTO>>(content.Result);
                foreach (TOCDTO toc in tocList)
                {
                    tocListView.Add(new TOCView() {Id=toc.Id, Text=toc.Text, Revision=toc.Revision });
                }
            }
        }

        private void OnPageLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new GamesListView();
        }
    }
}

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
using System.Net;
using rulesencyclopediaclient.Model;

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
            List<GameDTO> gamesDTOList;
            HttpClient client = comElements.getClient();
            //setting address and port for the service.
            Uri uri = comElements.getUri("Game");
            var response = client.GetAsync(uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ObservableCollection<GameView> gameListView = new ObservableCollection<GameView>();
                GamesListBox.ItemsSource = gameListView;
                var content = response.Content.ReadAsStringAsync();
                gamesDTOList = JsonConvert.DeserializeObject<List<GameDTO>>(content.Result);
                foreach (GameDTO game in gamesDTOList)
                {
                    gameListView.Add(new GameView() { Id=game.Id, Name = game.Name, Company = game.Company, Revision = game.Revision });
                } 
            }
        }
        
        private void GamesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            GameView selectedGame = new GameView();
            selectedGame = (GameView)e.AddedItems[0];
            this.TOCColLabel.Content = selectedGame.Name;
            //Get Tocs on the basis of the game
            List <TOCDTO> tocList;
            HttpClient client = comElements.getClient();
            Uri uri = comElements.getUri("TOC");
            var response = client.GetAsync(uri).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync();
                tocList = JsonConvert.DeserializeObject<List<TOCDTO>>(content.Result);
                int tocListId = tocList[0].Id;

                uri = comElements.getUri("Entry", "tocId="+tocListId);
                response = client.GetAsync(uri).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    List<EntryDTO> tocDTOList;
                    ObservableCollection<TocListView> tocListView = new ObservableCollection<TocListView>();
                    TOCListBox.ItemsSource = tocListView;
                    content = response.Content.ReadAsStringAsync();
                    tocDTOList = JsonConvert.DeserializeObject<List<EntryDTO>>(content.Result);
                    foreach (EntryDTO entry in tocDTOList)
                    {
                        tocListView.Add(new TocListView() { Id=entry.Id, ParagraphNumber = entry.ParagraphNumber, Headline=entry.Headline});
                    }
                }

            }


        }

        private void OnPageLoad(object sender, System.Windows.RoutedEventArgs e)
        {
           // DataContext = new GamesListView();
        }
    }
}

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
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace rulesencyclopediaclient.View
{
    /// <summary>
    /// Interaction logic for MainInfoWindow.xaml
    /// </summary>
    public partial class MainInfoWindow : Page
    {
        SelectionChangedEventArgs savedGame;
        int chosenTocId = -1;
        int chosenEntryId = -1;
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
            savedGame = e;
            List<EntryDTO> tocDTOList;
            ObservableCollection<TocListView> tocListView = new ObservableCollection<TocListView>();
            ObservableCollection<EntryListView> entryListView = new ObservableCollection<EntryListView>();
            GameView selectedGame = new GameView();

            selectedGame = (GameView)e.AddedItems[0];
            
            this.TOCColLabel.Content = selectedGame.Name;
            //Get Tocs on the basis of the game
            List <TOCDTO> tocList;
            var response = comElements.get( "TOC", "", Convert.ToString(selectedGame.Id));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync();
                tocList = JsonConvert.DeserializeObject<List<TOCDTO>>(content.Result);
                if (tocList.Count!=0)
                {
                    int tocListId = tocList[0].Id;
                    string body = "tocId=" + tocListId;
                    response = comElements.get( "Entry", body, "");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        TOCListBox.ItemsSource = tocListView;
                        EntryListBox.ItemsSource = entryListView;
                        content = response.Content.ReadAsStringAsync();
                        tocDTOList = JsonConvert.DeserializeObject<List<EntryDTO>>(content.Result);
                        foreach (EntryDTO entry in tocDTOList)
                        {
                            tocListView.Add(new TocListView() { Id = entry.Id, ParagraphNumber = entry.ParagraphNumber, Headline = entry.Headline });
                            entryListView.Add(new EntryListView() { Id = entry.Id, ParagraphNumber = entry.ParagraphNumber, Headline = entry.Headline, Editor = entry.Editor, Type = entry.Type, Txt = entry.Text });
                        }
                    }

                    chosenTocId = tocListId;
                }
                else
                {
                    tocListView.Clear();
                    TOCListBox.Items.Clear();
                    entryListView.Clear();
                    EntryListBox.Items.Clear();
                    chosenTocId = 0;
                }

            }


        }

        private void OnPageLoad(object sender, System.Windows.RoutedEventArgs e)
        {
           // DataContext = new GamesListView();
        }

        private void TocListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ListBoxObject.SelectedItem = ListBoxObject.Items.GetItemAt(itemIndex);

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewRule popup = new AddNewRule(chosenTocId);
            popup.ShowDialog();
            SelectionChangedEventArgs savedChoice=savedGame;
            GamesListBox_OnSelectionChanged(this, savedChoice );
        }

        private void deleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult diagResult = MessageBox.Show("Are you sure you want to delete this rule ?", "Delete Rule", buttons, MessageBoxIcon.Warning);
            if (diagResult == DialogResult.Yes)
            {
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                int index = EntryListBox.Items.IndexOf(button.DataContext);
                var listElement = EntryListBox.Items[index];
                var chosenEntry = (EntryListView)listElement;

                //Delete rule
                var response = comElements.delete("Entry/"+chosenEntry.Id, "");
                if (response.StatusCode == HttpStatusCode.NoContent) //NEEDS TO BE BETTER FEEDBACK FROM SERVICE
                {
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Rule deleted", "Rule deleted", buttons);
                };
                SelectionChangedEventArgs savedChoice = savedGame;
                GamesListBox_OnSelectionChanged(this, savedChoice);

            } 

        }

        private void EntryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (EntryListView)EntryListBox.SelectedItem;
            if (item != null)
            {
                chosenEntryId = item.Id;
            }
        }
    }
}

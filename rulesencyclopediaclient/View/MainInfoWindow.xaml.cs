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
using System.Net;
using rulesencyclopediaclient.Model;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace rulesencyclopediaclient.View
{
    /// <summary>
    /// Interaction logic for MainInfoWindow.xaml
    /// </summary>
    public partial class MainInfoWindow : Page
    {
        MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;

        //window.BackgroundImage.Opacity = 0.1;

        SelectionChangedEventArgs savedGame;
        private int chosenTocId = -1;
        private int chosenEntryId = -1;

        CommunicationElements comElements = new CommunicationElements();
        EntryListView entryViewData = new EntryListView();
        InterfaceAnimation interfaceAnim = new InterfaceAnimation();
        public MainInfoWindow()
        {
            InitializeComponent();
            
            List<GameDTO> gamesDTOList;
            HttpClient client = comElements.getClient();
            //setting address and port for the service.
            Uri uri = comElements.getUri("Game");
            var response = comElements.get("Game","","");
            //var response = client.GetAsync(uri).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ObservableCollection<GameView> gameListView = new ObservableCollection<GameView>();
                GamesListBox.ItemsSource = gameListView;
                var content = response.Content.ReadAsStringAsync();
                gamesDTOList = JsonConvert.DeserializeObject<List<GameDTO>>(content.Result);
                if (gamesDTOList != null)
                {
                    foreach (GameDTO game in gamesDTOList)
                    {
                        gameListView.Add(new GameView() { Id = game.Id, Name = game.Name, Company = game.Company, Revision = game.Revision });
                    }
                }
                else
                {
                    //Messagebox no games.
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
            var parameter = "gameId=" + Convert.ToString(selectedGame.Id);
            var response = comElements.get( "TOC", parameter,"");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync();
                tocList = JsonConvert.DeserializeObject<List<TOCDTO>>(content.Result);
                if (tocList.Count!=0)
                {
                    int tocListId = tocList[0].Id;
                    string parameters = "tocId=" + tocListId;
                    //Get the entries on the basis of the TOC
                    response = comElements.get( "Entry", parameters, "");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        TOCListBox.ItemsSource = tocListView;
                        EntryListBox.ItemsSource = entryListView;
                        content = response.Content.ReadAsStringAsync();
                        tocDTOList = JsonConvert.DeserializeObject<List<EntryDTO>>(content.Result);
                        foreach (EntryDTO entry in tocDTOList)
                        {
                            tocListView.Add(new TocListView() { Id = entry.Id, ParagraphNumber = entry.ParagraphNumber, Headline = entry.Headline });
                            entryListView.Add(new EntryListView() { Id = entry.Id, ParagraphNumber = entry.ParagraphNumber, Revision = entry.Revision, Headline = entry.Headline, Editor = entry.Editor, Type = entry.Type, Txt = entry.Text });
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
            /*TOCListBox.SelectedItem = TOCListBox.Items.GetItemAt();
            TocListView entryOnTocList = new TocListView();
            entryOnTocList =  (TocListView)TOCListBox.SelectedItem;
            Ent*/

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            int index = EntryListBox.Items.IndexOf(button.DataContext);
            var listElement = EntryListBox.Items[index];
            var chosenEntry = (EntryListView)listElement;
            EditRule popup = new EditRule(chosenTocId, chosenEntry);
            popup.ShowDialog();
            SelectionChangedEventArgs savedChoice = savedGame;
            GamesListBox_OnSelectionChanged(this, savedChoice);
        }

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewRule popup = new AddNewRule(chosenTocId);
            popup.ShowDialog();
            SelectionChangedEventArgs savedChoice = savedGame;
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
                this.chosenEntryId = chosenEntry.Id;

                //Delete rule
                var response = comElements.delete("Entry/"+this.chosenEntryId, "");
                if (response.StatusCode == HttpStatusCode.NoContent) //NEEDS TO BE BETTER FEEDBACK FROM SERVICE
                {
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Rule deleted", "Rule deleted", buttons);
                }
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

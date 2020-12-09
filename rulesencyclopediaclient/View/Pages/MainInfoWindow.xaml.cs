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
        SelectionChangedEventArgs savedGame;
        private int chosenTocId = -1;
        CommunicationElements comElements = new CommunicationElements();

        public MainInfoWindow()
        {
            InitializeComponent();
            //Getting the data for the GameListBox
            List<GameDTO> gamesDTOList;
            var response = comElements.get("Game","","");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Populating the GameListBox
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
            //Saving the selected game
            savedGame = e;

            List<EntryDTO> listOfEntries;
            //setting up toclistview and entrylistview as observablecollections, so that they can be monitored for changes for autoupdate
            ObservableCollection<TocListView> tocListView = new ObservableCollection<TocListView>();
            ObservableCollection<EntryListView> entryListView = new ObservableCollection<EntryListView>();
            
            GameView selectedGame = (GameView)e.AddedItems[0];
                   
            this.TOCColLabel.Content = selectedGame.Name;
            //Get Tocs on the basis of the game
            //There should be a list with multiple TOCs to choose from, but that will be in another iteration.
            //For now there is only 1 TOC in the DB for each game.

            List <TOCDTO> tocList;
            //Getting the TOCs. ps. only 1 for now
            var parameter = "gameId=" + Convert.ToString(selectedGame.Id);
            var response = comElements.get( "TOC", parameter,"");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync();
                //Deserialize the response
                tocList = JsonConvert.DeserializeObject<List<TOCDTO>>(content.Result);
                //If the list is not empty
                if (tocList.Count!=0)
                {
                    //For now get the first, and only, toc list
                    string parameters = "tocId=" + tocList[0].Id;

                    //Get entries on the basis of the TOC
                    response = comElements.get( "Entry", parameters, "");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //Setting the datasources of the TocListBox and EntryListBox
                        TOCListBox.ItemsSource = tocListView;
                        EntryListBox.ItemsSource = entryListView;
                        
                        //Getting the list of entries
                        content = response.Content.ReadAsStringAsync();
                        listOfEntries = JsonConvert.DeserializeObject<List<EntryDTO>>(content.Result);
                        if (listOfEntries.Count != 0)
                        {
                            foreach (EntryDTO entry in listOfEntries)
                            {
                                //Populate the toclistView, datasource for the TOCListBox, with entry.ID, paragraphnumber and headline
                                tocListView.Add(new TocListView() { Id = entry.Id, ParagraphNumber = entry.ParagraphNumber, Headline = entry.Headline });
                                //Populate the entryListView, datasource for the EntryListBox, with the Entry data.
                                entryListView.Add(new EntryListView() { Id = entry.Id, ParagraphNumber = entry.ParagraphNumber, Revision = entry.Revision, Headline = entry.Headline, Editor = entry.Editor, Type = entry.Type, Txt = entry.Text });
                            }
                        }
                    }
                    //Setting the variable chosenTocId to the tocList id of the first tocen in the toclist
                    //This makes no sence now, but when it will be able to choosen between multiple toc lists for a game
                    //It will make sence.
                    chosenTocId = tocList[0].Id;
                }
                else
                {
                    //If there is not data clear the listboxes
                    tocListView.Clear();
                    TOCListBox.Items.Clear();
                    entryListView.Clear();
                    EntryListBox.Items.Clear();
                    chosenTocId = 0;
                }
            }
        }

        private void TocListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If you choose an entry in the TocListBox, it should choose and show the entry in the EntryListBox
            EntryListBox.SelectedIndex = TOCListBox.SelectedIndex;
            EntryListBox.ScrollIntoView(EntryListBox.SelectedItem);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            //Getting the datacontext of the button which is the index of the listbox element the button is attached to.
            int index = EntryListBox.Items.IndexOf(button.DataContext);
            //Getting the listbox element data
            var chosenEntry = (EntryListView)EntryListBox.Items[index];
            //activate the EditRule window and pass the tocID and entry data to it.
            EditRule popup = new EditRule(chosenTocId, chosenEntry);
            popup.ShowDialog();
            //Update the context with the latest saved game for autoupdate of changes in the listboxes.
            SelectionChangedEventArgs savedChoice = savedGame;
            GamesListBox_OnSelectionChanged(this, savedChoice);
        }

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (savedGame != null)
            {
                //Activate the AddNewRule window and pass the chosenTocId to it.
                AddNewRule popup = new AddNewRule(chosenTocId);
                popup.ShowDialog();
                //Update the context with the latest saved game for autoupdate of changes in the listboxes.
                SelectionChangedEventArgs savedChoice = savedGame;
                GamesListBox_OnSelectionChanged(this, savedChoice);
            } else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Make sure you choose a game before you add a rule", "No Game is Chosen", buttons, MessageBoxIcon.Warning);

            }
        }

        private void deleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult diagResult = MessageBox.Show("Are you sure you want to delete this rule ?", "Delete Rule", buttons, MessageBoxIcon.Warning);
            if (diagResult == DialogResult.Yes)
            {
                System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
                //Getting the datacontext of the button which is the listbox index of the element it is embedded in.
                int index = EntryListBox.Items.IndexOf(button.DataContext);
                EntryListView chosenEntry = (EntryListView)EntryListBox.Items[index];

                //Delete rule
                var response = comElements.delete("Entry/"+ chosenEntry.Id, "");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Rule deleted", "Rule deleted", buttons);
                } else
                {
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Rule not found", "Database Error", buttons);
                }

                //Update the context with the latest saved game for autoupdate of changes in the listboxes.
                SelectionChangedEventArgs savedChoice = savedGame;
                GamesListBox_OnSelectionChanged(this, savedChoice);
            } 
        }

        private void addGameButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

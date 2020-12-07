using rulesencyclopediaclient.Model;
using rulesencyclopediaclient.Model.View;
using rulesencyclopediaclient.Tools;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace rulesencyclopediaclient.View
{
    /// <summary>
    /// Interaction logic for EditRule.xaml
    /// </summary>
    public partial class EditRule : Window
    {
        CommunicationElements comElements = new CommunicationElements();
        InterfaceAnimation interfaceAnim = new InterfaceAnimation();

        private int tocListId;
        private EntryListView entryData;
        public EditRule(int tocListId, EntryListView entryData)
        {
            InitializeComponent();
            this.tocListId = tocListId;        
            this.entryData = entryData;

            //Fill out the windows controls with data from the selected Entry
            paragraphNumberTextBox.Text = this.entryData.ParagraphNumber;
            headlineTextBox.Text = this.entryData.Headline;
            ruleTextBox.Text = this.entryData.Txt;
            revisionTextBox.Text = this.entryData.Revision;
            editorTextBox.Text = this.entryData.Editor;
        }

        private void editRuleButton_Click(object sender, RoutedEventArgs e)
        {
            //Check if user wants to submit the edited rules.
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult diagResult = MessageBox.Show("Do you want to edit the rule", "Rule edit", buttons, MessageBoxIcon.Warning);
            if (diagResult == System.Windows.Forms.DialogResult.Yes)
            {
                //Create an EntryDTO and fill it with the data from the window controls
                //to send to the server.
                EntryDTO entry = new EntryDTO();
                entry.Id = entryData.Id;
                entry.ParagraphNumber = paragraphNumberTextBox.Text;
                entry.Headline = headlineTextBox.Text;
                entry.Text = ruleTextBox.Text;
                entry.Revision = revisionTextBox.Text;
                entry.Editor = editorTextBox.Text;
                entry.TOC = this.tocListId;

                //edit the rule.
                var response = comElements.put("Entry", "", entry);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Rule has been updated", "Rule Updated", buttons);
                }
                else
                {
                    buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Rule has not been updated", "Server incident", buttons);
                }
                this.Close();
            }

            
        }

        //Pressing the return key is the same as clicking the button
        private void Keydown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            RoutedEventArgs z = new RoutedEventArgs();
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                //CHECK THAT ALL OBLIGATORY DATA IS FILLED OUT.
                editRuleButton_Click(this, z);
            }
        }

        //Control Animations
        private void txtBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox)
            {
                interfaceAnim.animateTextBox(sender as System.Windows.Controls.TextBox, "UP");
            }

            if (sender is PasswordBox)
            {
                interfaceAnim.animateTextBox(sender as System.Windows.Controls.PasswordBox, "UP");
            }

        }

        private void txtBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox)
            {
                interfaceAnim.animateTextBox(sender as System.Windows.Controls.TextBox, "DOWN");
            }

            if (sender is PasswordBox)
            {
                interfaceAnim.animateTextBox(sender as System.Windows.Controls.PasswordBox, "DOWN");
            }
        }
    }
}

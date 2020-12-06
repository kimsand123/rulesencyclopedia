using rulesencyclopediaclient.Model;
using rulesencyclopediaclient.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace rulesencyclopediaclient.View
{
    /// <summary>
    /// Interaction logic for AddNewRule.xaml
    /// </summary>
    public partial class AddNewRule : Window
    {
        CommunicationElements comElements = new CommunicationElements();
        InterfaceAnimation interfaceAnim = new InterfaceAnimation();
        private int tocListId;
        public AddNewRule(int tocListId)
        {
            InitializeComponent();
            this.tocListId = tocListId;
        }

        private void addRuleButton_Click(object sender, RoutedEventArgs e)
        {
            EntryDTO entry = new EntryDTO();
            
            entry.ParagraphNumber = paragraphNumberTextBox.Text;
            entry.Headline = headlineTextBox.Text;
            entry.Text = ruleTextBox.Text;
            entry.Revision = revisionTextBox.Text;
            entry.Editor = editorTextBox.Text;
            entry.TOC = tocListId;

            //add the new rule.
            var response = comElements.post( "Entry", entry);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Rule has been added", "Rule Created", buttons);
            } else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Rule has not been added", "Server incident", buttons);
            }
            this.Close();
        }

        //Pressing the return key is the same as clicking the button
        private void Keydown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RoutedEventArgs z = new RoutedEventArgs();
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                //CHECK THAT ALL OBLIGATORY DATA IS FILLED OUT.
                addRuleButton_Click(this, z);
            }
        }

        //Animations
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

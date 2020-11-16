﻿using rulesencyclopediaclient.Model;
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
        int tocListId;
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
            if (response.StatusCode == HttpStatusCode.NoContent)//NOT PROPER FEEDBACK FROM SERVICE... BETTER EXCEPTIONHANDLING!
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Rule has been added", "Rule Created", buttons);
            }
            this.Close();
        }
    }
}

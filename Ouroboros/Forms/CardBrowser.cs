# region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
# endregion

namespace Ouroboros {
    public partial class CardBrowser : Form {
        # region Fields
        SortedDictionary<string, Card> cardNames = new SortedDictionary<string, Card>(); // List of unique card names.
        bool userControlled = false; // Whether the checking of Set Browser items is user controlled or not.
        int currentCardIDX = -999; // Current card index selected.
        PriceCache cardPriceCache = null; // Card Prices Cache

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);   
        # endregion

        # region Initialization
        public CardBrowser() {
            InitializeComponent();
            InitializeSetBrowser();
            SetForegroundWindow(Handle.ToInt32());
        }

        private void InitializeSetBrowser() { // Initializes the Set Browser by adding the columns
            // Total Width of Control: 1048 (-4 to ensure no horizontal scrollbar) (-17 for vertical scrollbar) = 1027 usable pixels
            setBrowser.Columns.Add("Set Name", 397, HorizontalAlignment.Center);
            setBrowser.Columns.Add("Key", 45, HorizontalAlignment.Center);
            setBrowser.Columns.Add("Card Code", 150, HorizontalAlignment.Center);
            setBrowser.Columns.Add("Card Rarity", 165, HorizontalAlignment.Center);
            setBrowser.Columns.Add("Set Language", 185, HorizontalAlignment.Center);
            setBrowser.Columns.Add("Price", 85, HorizontalAlignment.Center);
        }

        private void CardBrowser_Load(object sender, EventArgs e) {
            // Set up all the custom fonts for all the labels...
            cardList.Font = new Font(DatabaseUpdater.pfc.Families[0], cardList.Font.Size);
            setBrowser.Font = new Font(DatabaseUpdater.pfc.Families[0], setBrowser.Font.Size);

            cardTypingBox.Font = new Font(DatabaseUpdater.pfc.Families[0], cardTypingBox.Font.Size);
            collectionBox.Font = new Font(DatabaseUpdater.pfc.Families[0], collectionBox.Font.Size);
            monsterDetailsBox.Font = new Font(DatabaseUpdater.pfc.Families[0], monsterDetailsBox.Font.Size);
            noPendulumTextGroup.Font = new Font(DatabaseUpdater.pfc.Families[0], noPendulumTextGroup.Font.Size);

            cardNameLabel.Font = new Font(DatabaseUpdater.pfc.Families[0], cardNameLabel.Font.Size);
            nameLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], nameLabel.Font.Size);
            setsLabel.Font = new Font(DatabaseUpdater.pfc.Families[0], setsLabel.Font.Size);

            cardAttribute.Font = new Font(DatabaseUpdater.pfc.Families[0], cardAttribute.Font.Size);
            attributeLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], attributeLabel.Font.Size);
            cardType.Font = new Font(DatabaseUpdater.pfc.Families[0], cardType.Font.Size);
            typeLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], typeLabel.Font.Size);
            cardSubType.Font = new Font(DatabaseUpdater.pfc.Families[0], cardSubType.Font.Size);
            subTypeLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], subTypeLabel.Font.Size);

            cardATK.Font = new Font(DatabaseUpdater.pfc.Families[1], cardATK.Font.Size);
            attackLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], attackLabel.Font.Size);
            cardDEF.Font = new Font(DatabaseUpdater.pfc.Families[1], cardDEF.Font.Size);
            defenseLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], defenseLabel.Font.Size);
            cardLevel.Font = new Font(DatabaseUpdater.pfc.Families[1], cardLevel.Font.Size);
            levelLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], levelLabel.Font.Size);

            textLabel_np.Font = new Font(DatabaseUpdater.pfc.Families[1], textLabel_np.Font.Size);
            cardText_np.Font = new Font(DatabaseUpdater.pfc.Families[0], cardText_np.Font.Size);
            textLabel_p.Font = new Font(DatabaseUpdater.pfc.Families[1], textLabel_p.Font.Size);
            cardText_p.Font = new Font(DatabaseUpdater.pfc.Families[0], cardText_p.Font.Size);
            pendulumTextLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], pendulumTextLabel.Font.Size);
            pendulumText.Font = new Font(DatabaseUpdater.pfc.Families[0], pendulumText.Font.Size);

            haveCount.Font = new Font(DatabaseUpdater.pfc.Families[1], haveCount.Font.Size);
            outOfLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], outOfLabel.Font.Size);
            forwardSlashLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], forwardSlashLabel.Font.Size);
            havePercentage.Font = new Font(DatabaseUpdater.pfc.Families[1], havePercentage.Font.Size);
            numberOwnedLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], numberOwnedLabel.Font.Size);
            collectionStatus.Font = new Font(DatabaseUpdater.pfc.Families[1], collectionStatus.Font.Size);
            statusLabel.Font = new Font(DatabaseUpdater.pfc.Families[1], statusLabel.Font.Size);

            cachedPriceAge.Font = new Font(DatabaseUpdater.pfc.Families[0], cachedPriceAge.Font.Size);
            getPricesButton.Font = new Font(DatabaseUpdater.pfc.Families[0], getPricesButton.Font.Size);

            // Add a list of card names to the card browser, unique card names only.
            foreach (Card c in DataStorage.database.listOfCards) {
                if (!cardNames.ContainsKey(c.cardName)) { 
                    cardNames.Add(c.cardName, c); 
                }
            }

            cardPriceCache = Utilities.loadPriceCache();

            // Sort the card names and then set the dataSource.
            cardList.DataSource = new BindingSource(cardNames, null);
            cardList.DisplayMember = "Key";
            cardList.ValueMember = "Value";
        }
        # endregion

        # region Card Label Events
        private void updateCardLabels(string cardName) { // Method to update all the card labels within the card browser.
            List<ListViewItem> listViewCards = new List<ListViewItem>();
            Card cardForDetails = cardNames[cardName];
            setBrowser.Items.Clear(); // Clear Set Browser
            userControlled = false; // Set this to false so that the ItemCheck event knows that the firing of the event is NOT user controlled.

            for (int i = 0; i < cardForDetails.listOfSets.Count(); i++) {
                ListViewItem item = new ListViewItem(System.Net.WebUtility.HtmlDecode(cardForDetails.listOfSets[i].cardSetName));  // Create a new list item with the set's name (first column)
                if (cardForDetails.listOfSets[i].cardOwned) { // If we owned this card...
                    item.Checked = true; // Check the item (we need userControlled = false here, otherwise event will fire).
                }

                item.SubItems.Add(assignKeyShortcut(i)); // Keyboard shortcut
                item.SubItems.Add(cardForDetails.listOfSets[i].cardSetID); // Full Card ID
                item.SubItems.Add(cardForDetails.listOfSets[i].cardSetRarity); // Card Rarity
                item.SubItems.Add(cardForDetails.listOfSets[i].cardSetLanguage); // Set Language
                
                // Card Price
                CardPrices cached = cardPriceCache.cache.FirstOrDefault(x => x.name == cardName);

                if (cached != null) {
                    bool foundPrice = false;
                    CardPrices.Datum cardPrice = cached.getCardPriceByID(cardForDetails.listOfSets[i].cardSetID, cardForDetails.listOfSets[i].cardSetRarity);

                    if (cardPrice == null) {
                        cardPrice = cached.getCardPriceByID(cardForDetails.listOfSets[i].cardSetID.Replace("EN", ""), cardForDetails.listOfSets[i].cardSetRarity);
                    }

                    if (cardPrice != null && cardPrice.price_data.status == "success") {
                        // If card-code and rarity are identical, then we can set the price.
                        item.SubItems.Add("$" + cardPrice.price_data.data.prices.average.ToString("n2"));
                        cachedPriceAge.Text = "Updated " + Utilities.getHumanReadableTime(cached.timeStamp);
                        foundPrice = true;
                    }

                    if (!foundPrice) { item.SubItems.Add("?"); } // Only if we didn't find a price in the cache, should we add "?".
                }
                else { // If no cached price, add "?"
                    item.SubItems.Add("?");
                    cachedPriceAge.Text = "No price data.";
                } 

                

                listViewCards.Add(item);
            }

            setBrowser.Items.AddRange(listViewCards.ToArray()); // Add the list of ListViewItems to the Set Browsers items.

            // Begin filling in details of cards into form
            // Card Name
            cardNameLabel.Text = System.Net.WebUtility.HtmlDecode(cardForDetails.cardName).Replace(" (card)", "");

            // Card Attribute
            if (cardForDetails.cardAttribute == "") { cardAttribute.Text = cardForDetails.cardType.ToUpper(); } // If the card has no attribute (spell or trap), then set attribute to card type.
            else { cardAttribute.Text = cardForDetails.cardAttribute.ToUpper(); } // Uppercase, as shown on card.

            // Card Type and Subtypes.
            cardType.Text = cardForDetails.cardType;
            string[] subTypes = cardForDetails.cardSubType.Split(new string[] { " / " }, StringSplitOptions.None);
            List<string> addedTypes = new List<string>();
            string subTypeText = "";
            foreach (string subtype in subTypes) { 
                if (!addedTypes.Contains(subtype)) {
                    subTypeText += subtype + " / ";
                     addedTypes.Add(subtype);
                }
            }
            if (subTypeText.Length > 0) { subTypeText = subTypeText.Substring(0, subTypeText.Length - 3); }
            cardSubType.Text = subTypeText;
            // Level / Rank
            if (cardForDetails.cardType.ToLower().Contains("xyz")) { levelLabel.Text = "Rank"; } // If the card is an Xyz Monster, then replace Level for Rank, as Xyz monsters have no level.
            else { levelLabel.Text = "Level"; }

            if (cardAttribute.Text.Equals("SPELL") || cardAttribute.Text.Equals("TRAP")) { // If card is a SPELL or TRAP, then it has no ATK, DEF or Level/Rank.
                cardATK.Text = "N/A";
                cardDEF.Text = "N/A";
                cardLevel.Text = "N/A";
            }
            else { // Otherwise fill in accordingly.
                cardATK.Text = cardForDetails.cardATK;
                cardDEF.Text = cardForDetails.cardDEF;
                cardLevel.Text = cardForDetails.cardLvl;
            }

            // Card Text
            if (cardForDetails.cardType.ToLower().Contains("pendulum")) {
                pendulumTextGroup.Visible = true;
                noPendulumTextGroup.Visible = false;
                cardText_p.Text = System.Net.WebUtility.HtmlDecode(cardForDetails.cardText).Replace("<br />", Environment.NewLine);
                pendulumText.Text = System.Net.WebUtility.HtmlDecode(cardForDetails.cardPendulumEffect).Replace("<br />", Environment.NewLine);
            }
            else {
                pendulumTextGroup.Visible = false;
                noPendulumTextGroup.Visible = true;
                cardText_np.Text = System.Net.WebUtility.HtmlDecode(cardForDetails.cardText).Replace("<br />", Environment.NewLine);
            }
            

            // Card Collection Status
            updateCardCollectionStatus(null); // Update the card collection status labels
            userControlled = true; // Set back to true as card details have been parsed now and user feedback is ready.
        }

        private string assignKeyShortcut(int index) { // Small method for assigning a keyboard shortcut string representation to a set item based on it's index
            if (index >= 40) { return ""; }
            else if (index == 39) { return "CA0"; }
            else if (index >= 30) { return "CA" + (index % 10 + 1); }
            else if (index == 29) { return "A0"; }
            else if (index >= 20) { return "A" + (index % 10 + 1); }
            else if (index == 19) { return "C0"; }
            else if (index >= 10) { return "C" + (index % 10 + 1); }
            else if (index == 9) { return "0"; }
            else { return (index + 1).ToString(); }
        }

        private void updateCardCollectionStatus(Boolean? check) { // Method to update the card collection status labels.
            KeyValuePair<string, Card> selectedKVPair = (KeyValuePair<string, Card>)cardList.SelectedItem; // Get the KeyValuePair of the selected item.

            haveCount.Text = selectedKVPair.Value.getHaveCount() + ""; // Set labels accordingly.
            outOfLabel.Text = selectedKVPair.Value.getTotalCount() + "";
            if (selectedKVPair.Value.getTotalCount() != 0) { // Calculate percentages of cards owned
                havePercentage.Text = "(" + (100.00f * selectedKVPair.Value.getHaveCount() / selectedKVPair.Value.getTotalCount()).ToString("n2") + "%)"; 
            }
            else { havePercentage.Text = "(100.00%)"; } // If attempting a division by zero, just set to 100%.
            
            // Update the collectionStatus text and color accordingly.
            if (selectedKVPair.Value.getHaveCount() == 0) { 
                collectionStatus.Text = "Not Owned";
                collectionStatus.ForeColor = Color.Red;
            }
            else if (selectedKVPair.Value.getHaveCount() == selectedKVPair.Value.getTotalCount()) {
                collectionStatus.Text = "Fully Collected";
                collectionStatus.ForeColor = Color.Green;
            }
            else {
                collectionStatus.Text = "Partially Collected";
                collectionStatus.ForeColor = Color.Gold;
            }
        }
        # endregion

        # region Card/Set Browser Events
        private void cardList_SelectedIndexChanged(object sender, EventArgs e) {
            if (cardList.SelectedIndex != currentCardIDX) { // If the selected index has actually changed, then update the card labels appropriately.
                currentCardIDX = cardList.SelectedIndex;
                KeyValuePair<string, Card> selectedKVPair = (KeyValuePair<string, Card>)cardList.SelectedItem;
                updateCardLabels(selectedKVPair.Key.ToString());
            }
        }
   
        private void cardList_DrawItem(object sender, DrawItemEventArgs e) {
            bool isSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            if (e.Index > -1) {
                /* If the item is selected set the background color to SystemColors.Highlight 
                 or else set the color to either WhiteSmoke or White depending if the item index is even or odd */
                Color color = (isSelected ? SystemColors.Highlight : (e.Index % 2 == 0 ? Color.WhiteSmoke : Color.White));

                KeyValuePair<string, Card> KVP = ((KeyValuePair<string, Card>)cardList.Items[e.Index]); // Retreive KeyValuePair of this item.

                int haveCount = KVP.Value.getHaveCount(); // Get number of cards we have.
                int totalCount = KVP.Value.getTotalCount(); // Get number of cards there are in total.

                // Background item brush
                SolidBrush backgroundBrush = new SolidBrush(color);
                // Text color brush
                SolidBrush textBrush = new SolidBrush(e.ForeColor); // Default to system forecolor.
                if (haveCount == 0 && totalCount != 0) { textBrush = new SolidBrush(Color.Red); } // Do not own any of this card.
                else if (haveCount > 0 && haveCount < totalCount) { textBrush = new SolidBrush(Color.Orange); } // Have some but not all.
                else if (haveCount == totalCount) { textBrush = new SolidBrush(Color.Green); } // Have all of this card.

                // Draw the background
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
                // Draw the text
                e.Graphics.DrawString(((KeyValuePair<string, Card>)cardList.Items[e.Index]).Key.ToString().Replace(" (card)", ""), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault); // Get only Key of KVP, which is the card's name.
                // Clean up
                backgroundBrush.Dispose();
                textBrush.Dispose();
            }
            e.DrawFocusRectangle();
        }

        private void cardList_KeyDown(object sender, KeyEventArgs e) { 
            // Custom override of the keydown to allow for checking of sets from the keyboard when in CardList.
            processSetCardKeyPress(e);
        }

        private void processSetCardKeyPress(KeyEventArgs e) {
            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) { // If the key is a numpad key...
                int value = -1; // Value of the number from the numpad key.
                // if (e.Modifiers)
                if (e.KeyCode == Keys.NumPad0) {
                    value = 10; // We want 0 to be 10 instead in this case.
                }
                else {
                    value = e.KeyValue - ((int)Keys.NumPad0); // Value is the keycode value - the keycode value of 0.
                }

                if (e.Control) { value += 10; }
                if (e.Alt) { value += 20; }

                if (value <= this.setBrowser.Items.Count) { // If the value is less than or equal to the set count: 
                    var item = setBrowser.Items[value - 1]; // Find the set from the index (value - 1).
                    if (item != null) { 
                        item.Checked = !item.Checked; // Negate it's current checked state.
                    }
                }
                e.SuppressKeyPress = true; // Suppress the keypress as we don't need to process it anymore.
            }
        }

        private void setBrowser_KeyDown(object sender, KeyEventArgs e) {
            // Custom override of the keydown to allow for checking of sets from the keyboard when in SetBrowser.
            processSetCardKeyPress(e);
        }

        private void setBrowser_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) { // Uses the ColumnWidthChanging event to deny the ability to resize columns.
            e.Cancel = true;
            e.NewWidth = setBrowser.Columns[e.ColumnIndex].Width;
        }

        private void setBrowser_ItemCheck(object sender, ItemCheckEventArgs e) { // On checking or unchecking an item in the Set Browser
            if (userControlled) { // If this change is user-controlled (not controlled by the card-parser...
                Boolean haveCardNow = (e.NewValue == CheckState.Checked); // Check if we actually have the card.

                KeyValuePair<string, Card> selectedKVPair = (KeyValuePair<string, Card>)cardList.SelectedItem;
                selectedKVPair.Value.listOfSets[e.Index].cardOwned = haveCardNow;
                cardList.Invalidate(); // Trigger a re-draw instantly.

                // Clear any previous selections on checking an item.
                if (this.setBrowser.SelectedIndices.Count > 0) { // If there are any selctions
                    for (int i = 0; i < this.setBrowser.SelectedIndices.Count; i++) { // Clear all selections first
                        this.setBrowser.Items[this.setBrowser.SelectedIndices[i]].Selected = false;
                    }
                }
                // Then select the item checked.
                var item = setBrowser.Items[e.Index];
                if (item != null) { 
                    item.Selected = true; 
                    item.EnsureVisible();
                }

                updateCardCollectionStatus(haveCardNow); // Update the collection status labels accordingly.
            }
        }
        # endregion

        # region Menu Strip Events
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close(); // Close the form.
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void findMismatchesFromUpdate_Click(object sender, EventArgs e) {
            CardDB old = Utilities.loadDB("v2_backup", null);
            CardDB current = DataStorage.database;

            Dictionary<SetCard, string> oldOwned = new Dictionary<SetCard, string>();

            Console.WriteLine("Finding mismatches...");
            foreach (Card o in old.listOfCards) { // For each card in the old database.
                Card n = current.listOfCards.Find(p => p.cardName.Equals(o.cardName, StringComparison.InvariantCultureIgnoreCase)); // We find the card in the new database (by checking for identical names).

                foreach (SetCard so in o.listOfSets) { // For each SetCard in the old card's list of sets...
                    SetCard sn = n.listOfSets.Find(s => (s.cardSetID == so.cardSetID && s.cardSetRarity == so.cardSetRarity)); // We find the matching SetCard in the new card's list of set...

                    if (so.cardOwned) { // Add it to oldOwned, it will be removed if imported successfully.
                        oldOwned.Add(so, o.cardName);
                    }

                    if (sn != null) {
                        sn.cardOwned = so.cardOwned; // And we copy across the cardOwned state from the old DB to the new DB.
                        if (so.cardOwned) {
                            oldOwned.Remove(so);
                        }
                    }
                }
            }

            foreach (SetCard sd in oldOwned.Keys) {
                Console.WriteLine("-> '" + oldOwned[sd] + "' with set ID: " + sd.cardSetID + " (" + sd.cardSetRarity + ")");
            }
        }

        private void collectorReportToolStripMenuItem_Click(object sender, EventArgs e) {
            CollectorReport form = new CollectorReport();
            form.ShowDialog(); // Show report.
        }
        private void saveDatabaseToolStripMenuItem_Click(object sender, EventArgs e) {
            // Save the database
            Utilities.saveDB(DataStorage.database);
            
            // Initializes the variables to pass to the MessageBox.Show method. 
            string message = "The database was successfully saved!";
            string caption = "Database saved";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
        }

        private void addCardSetToolStripMenuItem_Click(object sender, EventArgs e) {
            
            // Utilities.getSingleSetFromWikia("Duelist Pack: Battle City");
        }
        # endregion

        # region UI Events
        private void getPricesButton_Click(object sender, EventArgs e) {
            // Lock the get card prices button until we're done.
            getPricesButton.Text = "Retrieving...";
            getPricesButton.Enabled = false;

            // Fetch the card prices asynchronously!
            GetPrices i = new GetPrices(getCardPrices);
            IAsyncResult result1 = i.BeginInvoke(cardList.SelectedIndex, null, null);
        }

        delegate void GetPrices(int selectedIndex);

        private void getCardPrices(int selectedIndex) {
            KeyValuePair<string, Card> selectedKVPair = (KeyValuePair<string, Card>)cardList.Items[selectedIndex];
            CardPrices prices = Utilities.getCardPrices(selectedKVPair.Value.cardName); // Retreive a list of card prices from yugiohprices.com
            
            if (prices == null) { // No prices found :(
                this.Invoke(new Action(() => { // Return to main thread to proceed with updating UI elements.
                    // Return the button to the normal state.
                    getPricesButton.Text = "Get Prices!";
                    getPricesButton.Enabled = true;
                }));
                return;
            }

            CardPrices cached = cardPriceCache.cache.FirstOrDefault(x => x.name == selectedKVPair.Value.cardName);
            if (cached == null) { // If this result hasn't been cached already, add it.
                cardPriceCache.cache.Add(prices);
            }
            else { // Otherwise update the cache.
                cardPriceCache.cache.Remove(cached);
                cardPriceCache.cache.Add(prices);
            }

            this.Invoke(new Action(() => { // Return to main thread to proceed with updating UI elements.
                if (cardList.SelectedIndex == selectedIndex) { // We check if the current selected list element is the same as when the button was pressed.
                    // We need to check on a binary (i, j) matching.
                    for (int j = 0; j < this.setBrowser.Items.Count; j++) {
                        CardPrices.Datum cardPrice = prices.getCardPriceByID(this.setBrowser.Items[j].SubItems[2].Text, this.setBrowser.Items[j].SubItems[3].Text);

                        if (cardPrice == null) {
                            cardPrice = prices.getCardPriceByID(this.setBrowser.Items[j].SubItems[2].Text.Replace("EN", ""), this.setBrowser.Items[j].SubItems[3].Text);
                        }

                        if (cardPrice != null && cardPrice.price_data.status == "success") {
                            // If card-code and rarity are identical, then we can set the price.
                            this.setBrowser.Items[j].SubItems[5].Text = "$" + cardPrice.price_data.data.prices.average.ToString("n2");
                        }
                    }
                }
                // Return the button to the normal state.
                getPricesButton.Text = "Get Prices!";
                getPricesButton.Enabled = true;

                // Update cache age to be 0 seconds ago (we just grabbed them)
                cachedPriceAge.Text = "Updated 0 seconds ago";
            }));

            Utilities.savePriceCache(cardPriceCache);
        }

        private void CardBrowser_FormClosing(object sender, FormClosingEventArgs e) { 
            // Save database and price cache on form closing...
            Utilities.saveDB(DataStorage.database);
            Console.WriteLine("[DB Manager] Database was saved successfully before program closure!");

            Utilities.savePriceCache(this.cardPriceCache);
            Console.WriteLine("[Price Cache] Price Cache was saved successfully before program closure!");

        }
        #endregion
    }
}

# region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
# endregion

namespace Ouroboros {
    public partial class DatabaseUpdater : Form {
        # region Fields
        public static PrivateFontCollection pfc = null;

        private string[] listOfStatusMessages = new string[8];
        private int runningUpdateThreads = 0;
        private bool savingDB = false;
        private bool errorOccured = false;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        # endregion

        # region Initialization
        public DatabaseUpdater() {
            // Initialization
            InitializeCustomFonts(); // Initialize custom fonts
            InitializeComponent();
            DataStorage.database = Utilities.loadDB("v2", this); // Load the card DB from XML file.

            // Begin fetching new card sets asynchronously (allowing log message to update)
            CheckForDatabaseUpdate i = new CheckForDatabaseUpdate(checkForDatabaseUpdate); 
            IAsyncResult result1 = i.BeginInvoke(null, null);
        }

        private void Ouroboros_Load(object sender, EventArgs e) { 
            // On form-load (after initialization of custom fonts), set the custom fonts for the labels here.
            appName.Font = new Font(pfc.Families[0], appName.Font.Size);
            statusLabel.Font = new Font(pfc.Families[0], statusLabel.Font.Size);
            logMessage1.Font = new Font(pfc.Families[0], logMessage1.Font.Size);
            logMessage2.Font = new Font(pfc.Families[0], logMessage2.Font.Size);
            logMessage3.Font = new Font(pfc.Families[0], logMessage3.Font.Size);
            logMessage4.Font = new Font(pfc.Families[0], logMessage4.Font.Size);
            logMessage5.Font = new Font(pfc.Families[0], logMessage5.Font.Size);
            logMessage6.Font = new Font(pfc.Families[0], logMessage6.Font.Size);
            logMessage7.Font = new Font(pfc.Families[0], logMessage7.Font.Size);
        }

        private void InitializeCustomFonts() { 
            // Initialize custom fonts by calling AddCustomFont for each.
            AddCustomFont(Properties.Resources.Roboto_Light);
            AddCustomFont(Properties.Resources.Squares_Bold);
        }

        private void AddCustomFont(byte[] resourceName) {
            if (pfc == null) { pfc = new PrivateFontCollection(); } // Create the private font collection object.

            int fontLength = resourceName.Length; // First we get the font's length
            byte[] fontData = resourceName; // Allocate a byte array with the data.
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength); // Create an unsafe memory block for the font data
            Marshal.Copy(fontData, 0, data, fontLength); // Copy the bytes to the unsafe memory block

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fontData.Length, IntPtr.Zero, ref cFonts); // Add font to memory image on the system, no longer requiring CompatibilityStyles on.

            pfc.AddMemoryFont(data, fontLength); // Pass the font to the font collection
            Marshal.FreeCoTaskMem(data); // Free up the unsafe memory
            } 
        # endregion

        # region Asynchronous Card Fetching
        delegate void CheckForDatabaseUpdate();

        private void checkForDatabaseUpdate() {
            addMessageToLog("Checking age of your database file...");
            double numberOfDays = Double.PositiveInfinity;
            if (DataStorage.database.updateTimeString != "") {
                numberOfDays = ((TimeSpan)(DateTime.Now - DateTime.Parse(DataStorage.database.updateTimeString))).TotalDays;
            }
            if (numberOfDays > 45 && DataStorage.database.listOfCards.Count() > 0) { // If the database is older than 45 days... proceed with an update.
                DialogResult result = DialogResult.Ignore;

                this.Invoke(new Action(() => {
                    // Displays the MessageBox.
                    string message = "Your database is more than 45 days old, would you like to grab a full update? All your owned cards will be transferred! This can take up to an hour.";
                    result = MessageBox.Show(this, message, "Database update recommended!", MessageBoxButtons.YesNo);

                }));

                if (result == System.Windows.Forms.DialogResult.Yes) { // If user accepts update, begin it, otherwise just load the CardBrowser (at the bottom of this method).
                    updateDatabase();
                }
            }
            else if (DataStorage.database.listOfCards.Count() == 0) { // If there is no database yet, then we need to create one, so proceed with an update regardless.
                updateDatabase();
            }
            else { // Otherwise, database is less than or equal to 45 days old...
                // Check for cards with no sets.
                checkForCardsWithNoSets(DataStorage.database);

                // Update labels and log message accordingly...
                updateProgressBar(updateProgress, 100, 100); // Fill progress bar instantly.
                updateLabel(statusLabel, "You are up to date! :)", null);
                addMessageToLog("Your card database is totally up to date!");
                addMessageToLog("Launching Ouroboros... hold onto your hats...");
            }
            // Create and display the CardBrowser form
            Thread.Sleep(500); // Wait 500 ms for web threads to finish fully
            
            CardBrowser form = new CardBrowser();
            hideForm(); // Hide the main form before showing the secondary
            form.ShowDialog(); // Show secondary form, code execution stop until form is closed
            Application.Exit(); // Then close the application fully.
        }

        private void checkForCardsWithNoSets(CardDB db) { // Method to check for any cards with no sets, may need to regrab their data.
            List<Card> cardsWithNoSets = new List<Card>();
            List<Card> attemptedFixCards = new List<Card>();
            foreach(Card c in db.listOfCards) {
                if (c.getTotalCount() == 0) {
                    cardsWithNoSets.Add(c);
                }
            }
            if (cardsWithNoSets.Count() > 0) {
                addMessageToLog("Unfortunately, we found " + cardsWithNoSets.Count() + " cards with no sets, attempting to fix :)");
                foreach (Card c in cardsWithNoSets) {
                    Card cNew = Utilities.getCardData(c.cardName); // Grab the data...
                    attemptedFixCards.Add(cNew); // And then add it to the new DB.

                    string percentageString = " (" + (100.00f * (attemptedFixCards.Count() * 1.00f / cardsWithNoSets.Count() * 1.00f)).ToString("n3") + "%)";
                    addMessageToLog("Successfully grabbed data for card " + attemptedFixCards.Count() + " of " + cardsWithNoSets.Count().ToString("n0") + percentageString + ".");
                    updateProgressBar(updateProgress, attemptedFixCards.Count(), cardsWithNoSets.Count()); // Update the progress bar.

                    if (cNew.getTotalCount() == 0) {
                        Console.WriteLine("[ERROR] " + c.cardName + " still has no sets.");
                    }
                    else {
                        db.listOfCards.Remove(c);
                        db.listOfCards.Add(cNew);
                    }
                }
                db.listOfCards.OrderBy(x => x.cardName);
                Utilities.saveDB(db);
            }  
        }

        public void updateDatabase() { // Method to update the database file.
            CardDB newDB = Utilities.loadDB("v2_update", this); // Create a temporary update file DB.
            updateLabel(statusLabel, "Updating database, please wait :)...", null);
            bool resuming = false;

            if (newDB.listOfCards.Count() > 0) { // Check if there is already some cards, therefore resuming.
                addMessageToLog("There is already a partial update file, resuming! :)");
                resuming = true;
            }

            // Grab a list of cards from the Wikia
            addMessageToLog("Getting a list of all cards from the Yu-Gi-Oh! Wikia.");
            List<string> listOfCards = Utilities.getListOfCards(false);
            addMessageToLog("Successfully retrieved a list of " + listOfCards.Count().ToString("n0") + " cards.");

            // Resume functionality.
            List<string> cardsToGrab = listOfCards.Except(newDB.getListOfUniqueCardNames()).ToList<string>();
            if (resuming) { addMessageToLog("There are still " + cardsToGrab.Count() + " cards to retrieve."); } // Only display this if resuming.

            updateProgressBar(updateProgress, 0, listOfCards.Count()); // Set the maximum of the updateProgress to the number of cards left to grab.

            for (int i = 0; i < cardsToGrab.Count(); i++) { // For each card still left to grab... 
                // Begin fetching new card data asynchronously (and multi-threaded)
                while (runningUpdateThreads >= 3) {
                    if (errorOccured) { break; } // If there was an error during card grabbing, abort.
                    Thread.Sleep(500);
                }

                if (errorOccured) { break; } 

                AddCardData a = new AddCardData(addCardData);
                IAsyncResult result1 = a.BeginInvoke(newDB, cardsToGrab, i, null, null);
                runningUpdateThreads++;
            }

            // If an error occured during grabbing card data, display an error and exit.
            if (errorOccured) {
                this.Invoke(new Action(() => {
                    // Change progress bar to RED (1 = normal (green); 2 = error (red); 3 = warning (yellow))
                    SendMessage(updateProgress.Handle, 1040, (IntPtr)2, IntPtr.Zero);

                    // Displays the MessageBox.
                    MessageBox.Show(this, "There was a problem updating your database, could be Wikia has throttled our requests, try again in a few minutes :)", "An error has occured :<", MessageBoxButtons.OK);
                }));
                // Close the program
                Application.Exit();
            }
            
            // Once complete, wait for the running threads to go down to zero.
            bool printOnce = true;
            while (runningUpdateThreads > 0) {
                if (printOnce) {
                    addMessageToLog("Waiting for card data grabbing to complete :)");
                    printOnce = false;
                }
                if (listOfCards.Count() == newDB.listOfCards.Count()) { 
                    // If cards added are now equal to list of cards, then break out of loop anyway.
                    runningUpdateThreads = 0;
                    break; 
                }
                Thread.Sleep(500);
            }

            Utilities.saveDB(newDB); // Save the new database file completely.

            addMessageToLog("Importing owned cards to new DB file...");
            int numberImported = Utilities.importOwnedCards(DataStorage.database, newDB); // Import previously owned cards into new DB.
            addMessageToLog("Successfully imported " + numberImported.ToString("n0") + " owned cards.");

            if (File.Exists("CardDB_v2.xml")) { File.Move("CardDB_v2.xml", "CardDB_v2_backup.xml"); }

            newDB.updateTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Mark the time that the update is complete, next update is recommended 45 days from this time.
            newDB.databaseName = "v2"; // Change v2_update to v2 (no longer an "in progress" file).
            Utilities.saveDB(newDB);
            DataStorage.database = newDB;

            updateLabel(statusLabel, "Database was updated! :)", null);
            if (File.Exists("CardDB_v2_update.xml")) { File.Move("CardDB_v2_update.xml", "CardDB_v2_clean.xml"); } // Keep a clean file
        }

        delegate void AddCardData(CardDB newDB, List<string> cardsToGrab, int index);

        private void addCardData(CardDB newDB, List<string> cardsToGrab, int index) {
            Card c = Utilities.getCardData(cardsToGrab[index]); // Grab the data...
            if (c == null) { // If there was an error during card grabbing, abort
                errorOccured = true;
                return;
            }
            newDB.listOfCards.Add(c); // And then add it to the new DB.
            
            string percentageString = " (" + (100.00f * (newDB.listOfCards.Count() * 1.00f / updateProgress.Maximum * 1.00f)).ToString("n3") + "%)";
            addMessageToLog("Successfully grabbed data for card " + newDB.listOfCards.Count().ToString("n0") + " of " + updateProgress.Maximum.ToString("n0") + percentageString + ".");
            updateProgressBar(updateProgress, newDB.listOfCards.Count(), null); // Update the progress bar.
            
            if (newDB.listOfCards.Count() % 50 == 0) { // Save progression on update database every 50 items.
                if (!savingDB) { 
                    savingDB = true; // Mark DB as being saved, prevents multithreading errors.
                    Utilities.saveDB(newDB);
                    savingDB = false; // DB has been saved fully, proceed to unmark.

                    addMessageToLog("Update DB was saved successfully!");
                }
            }
            runningUpdateThreads--;
        }
        # endregion

        # region UI Controls
        public void addMessageToLog(string message) { // Adds a message to the log of the updater... 
            for (int i = 6; i >= 0; i--) { // Shifts all messages up one
                listOfStatusMessages[i + 1] = listOfStatusMessages[i];
            }
            listOfStatusMessages[0] = message; // Adds the new message...

            // Updates all labels accordingly..
            updateLabel(logMessage1, listOfStatusMessages[0], null);
            updateLabel(logMessage2, listOfStatusMessages[1], null);
            updateLabel(logMessage3, listOfStatusMessages[2], null);
            updateLabel(logMessage4, listOfStatusMessages[3], null);
            updateLabel(logMessage5, listOfStatusMessages[4], null);
            updateLabel(logMessage6, listOfStatusMessages[5], null);
            updateLabel(logMessage7, listOfStatusMessages[6], null);
        }

        delegate void UpdateProgressBar(ProgressBar pb, int value, double? max);

        private void updateProgressBar(ProgressBar pb, int value, double? max) { // Update progress bar on the UI thread-safely
            if (!pb.InvokeRequired) { // If no invocation is required, then just update...
                if (max != null) { pb.Maximum = (int)max; }
                pb.Value = value;
            }
            else { // Otherwise delegate back to the correct thread.
                UpdateProgressBar dele = new UpdateProgressBar(updateProgressBar);
                this.Invoke(dele, new object[] { pb, value, max });
            }
        }

        delegate void UpdateLabel(Label lb, string msg, Color? clr);

        private void updateLabel(Label lb, string msg, Color? clr) { // Update a label on the UI thread-safely
            if (!lb.InvokeRequired) { // If no invocation is required, then just update...
                lb.Text = msg;
                if (clr != null) { lb.ForeColor = (Color)clr; }
                lb.Invalidate();
                lb.Update();
            }
            else {  // Otherwise delegate back to the correct thread.
                try {
                    UpdateLabel dele = new UpdateLabel(updateLabel);
                    this.Invoke(dele, new object[] { lb, msg, clr });
                }
                catch { }
            }
        }

        delegate void HideForm();

        private void hideForm() { // Hide the form thread safely
            if (!this.InvokeRequired) { // If no invocation is required, then just update...
                this.Hide();
            }
            else { // Otherwise delegate back to the correct thread.
                try {
                    HideForm dele = new HideForm(hideForm);
                    this.Invoke(dele, new object[] { });
                }
                catch { }
            }
        }
        # endregion
    }
}

# region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
# endregion

namespace Ouroboros {
    public partial class CollectorReport : Form {
        # region Initialization
        public CollectorReport() {
            InitializeComponent();
            CalculateReport();
        }

        private void CollectorReport_Load(object sender, EventArgs e) {
            // Set up the custom fonts for the label.
            collectorReportLabel.Font = new Font(DatabaseUpdater.pfc.Families[0], collectorReportLabel.Font.Size);
            totalCardsCollected.Font = new Font(DatabaseUpdater.pfc.Families[0], totalCardsCollected.Font.Size);
            uniqueCardsCollected.Font = new Font(DatabaseUpdater.pfc.Families[0], uniqueCardsCollected.Font.Size);
        }
        # endregion

        # region Private Methods
        private void CalculateReport() { // Calculates the report...
            CardDB db = DataStorage.database; // Get the database

            int numberOfUniqueCards = db.listOfCards.Count(); // Number of unique cards in the DB
            int numberOfUniqueOwnedCards = 0; // Number of unique card owned
            int numberOfCards = 0; // Number of total cards in DB
            int numberOfOwnedCards = 0; // Number of total cards owned

            foreach (Card c in db.listOfCards) { // For each card in the DB
                if (c.getHaveCount() > 0 && c.getTotalCount() > 0) { numberOfUniqueOwnedCards++; }

                numberOfCards += c.getTotalCount();
                numberOfOwnedCards += c.getHaveCount();
            }

            // Update the total cards label and progress bar
            totalProgressBar.Maximum = numberOfCards;
            totalProgressBar.Value = numberOfOwnedCards;
            totalCardsCollected.Text = "Total Cards Collected: " + numberOfOwnedCards.ToString("n0") + " of " + numberOfCards.ToString("n0") + "  (" + (100.00f * numberOfOwnedCards / numberOfCards).ToString("n3") + "%)";

            // Update the unique cards label and progress bar
            uniqueProgressBar.Maximum = numberOfUniqueCards;
            uniqueProgressBar.Value = numberOfUniqueOwnedCards;
            uniqueCardsCollected.Text = "Unique Cards Collected: " + numberOfUniqueOwnedCards.ToString("n0") + " of " + numberOfUniqueCards.ToString("n0") + "  (" + (100.00f * numberOfUniqueOwnedCards / numberOfUniqueCards).ToString("n3") + "%)";
        }
        # endregion
    }
}
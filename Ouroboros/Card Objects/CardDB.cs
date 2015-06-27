using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ouroboros {
    public class CardDB {
        [XmlAttribute("DBVersion")]
        public double databaseFileVersion = 2.00; // Database File Format Version
        [XmlAttribute("DBUpdateTime")]
        public string updateTimeString = ""; // Time of update being completed.
        [XmlAttribute("DBSaveTime")]
        public string databaseSaveTimeString = ""; // Time of last modification/save.
        [XmlAttribute("DBName")]
        public string databaseName = ""; // Database name

        public List<Card> listOfCards = new List<Card>(); // List of cards in DB.

        public CardDB() { }

        public List<string> getListOfUniqueCardNames() { // Get a list of all unique card names in the DB.
            List<string> uniqueNames = new List<string>();

            foreach (Card c in listOfCards) {
                if (!uniqueNames.Contains(c.cardName)) { uniqueNames.Add(c.cardName); }
            }
            return uniqueNames;
        }

        public Card getCardByName(string name) { // Get the Card object for a card with a specified name
            foreach (Card c in listOfCards) {
                if (c.cardName == name) { // Return the card with same name as search...
                    return c;
                }
            }
            return null; // Otherwise return null.
        }
    }
}

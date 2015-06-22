using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ouroboros {
    public class Card {
        [XmlAttribute("Name")]
        public string cardName = "";

        [XmlElement("Attribute")]
        public string cardAttribute = "";
        [XmlElement("Type")]
        public string cardType = "";
        [XmlElement("SubType")]
        public string cardSubType = "";

        [XmlElement("Level")]
        public string cardLvl = "-1";
        [XmlElement("ATK")]
        public string cardATK = "-999";
        [XmlElement("DEF")]
        public string cardDEF = "-999";


        [XmlElement("Text")]
        public string cardText = "None";
        [XmlElement("PendulumEffect")]
        public string cardPendulumEffect = "None";

        public List<SetCard> listOfSets = new List<SetCard>();

        public Card() { } // Only used by XML deserialiser.

        public List<string> getListOfSetNamesCardIsIn() {
            List<string> setNames = new List<string>();
            foreach (SetCard sc in listOfSets) {
                setNames.Add(sc.cardSetID + "," + sc.cardSetRarity);
            }

            return setNames;
        }

        public int getHaveCount() { // Get number of cards we have... 
            int h = 0;
            foreach (SetCard sc in listOfSets) { // For each set card, check if cardOwned is true, then increment h by 1.
                if (sc.cardOwned) { h++; }
            }
            return h; // Return number of cards we have.
        }

        public int getTotalCount() { // Get number of sets this card is in.
            return listOfSets.Count();
        }
    }
}

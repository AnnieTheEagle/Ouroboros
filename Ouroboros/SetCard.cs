# region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
# endregion

namespace Ouroboros {
    public class SetCard { // An object for holding a single card in a set's data.
        [XmlAttribute("cardSetID")]
        public string cardSetID = "null"; // Card Set ID (for example LOB-EN001).
        [XmlElement("cardSetName")]
        public string cardSetName = "null"; // Name of the set the card resides in.
        [XmlElement("cardSetRarity")]
        public string cardSetRarity = "null"; // Rarity of the card within the set.
        [XmlElement("cardSetLanguage")]
        public string cardSetLanguage = "null"; // Language of the set.
        [XmlElement("cardOwned")]
        public bool cardOwned = false; // Is this card owned by user?

        public SetCard(string csvInput) {
            // PSV-EN004,Pharaoh's Servant,Common,English (EN)
            //     0              1          2       3
            string[] csvParts = csvInput.Split(',');

            cardSetID = csvParts[0];
            cardSetName = csvParts[1];
            cardSetRarity = csvParts[2];
            cardSetLanguage = csvParts[3];
            cardOwned = false;
        }

        public SetCard() { } // For the XML Serializer
    }
}

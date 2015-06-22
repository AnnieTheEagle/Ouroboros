# region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
# endregion

namespace Ouroboros {
    public class CardPrice { // Object to hold data for a card's prices.
        public string cardName = "undefined";
        public string cardCode = "XX00-EN000";
        public string cardRarity = "Non-existent";

        public Dictionary<string, double> conditionPrices = new Dictionary<string, double>();

        public CardPrice(string name, string code, string rarity, Dictionary<string, double> prices) {
            this.cardName = name;
            this.cardCode = code;
            this.cardRarity = rarity;
            this.conditionPrices = prices;
        }

        public double getBestConditionPrice() { // Get's the highest priced (also the highest condition, as better condition = higher price).
            return conditionPrices.Aggregate((r, s) => r.Value > s.Value ? r : s).Value;
        }
    }
}

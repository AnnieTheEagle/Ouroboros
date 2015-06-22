using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros {
    class CardCount { // Object for holding the number of cards we own and total for a unique card name.
        int haveCards = 0; // How many cards are owned...
        int totalCards = 0; // How many cards there are of this name...

        public CardCount(bool have) { // On creation, add a card and if we have it...
            addCard(have);
        }

        public void addCard(bool have) { // Adds a card and whether or not we have it...
            if (have) { this.haveCards += 1; }
            totalCards += 1;
        }

        public void updateCard(bool haveNow) { // Update card on checking of item.
            if (haveNow) { this.haveCards += 1; }
            else { this.haveCards -= 1; }
        }

        public int getHaveCount() { return haveCards; } // Get number of cards we have
        public int getTotalCount() { return totalCards; } // Get total number of cards

        public override string ToString() { // Special to-string implementation
            return (haveCards + ", " + totalCards);
        }
    }
}

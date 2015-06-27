using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ouroboros {
    public class PriceCache {
        [XmlElement("ListOfPrices")]
        public List<CardPrices> cache = new List<CardPrices>(); // A temporary cache for card prices.

        public PriceCache() { } // For XML Serializer
    }
}

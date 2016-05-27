# region Using Statements
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
# endregion

namespace Ouroboros {
    public class CardPrices {
        # region Fields
        [XmlElement("CardName")]
        public string name { get; set; }
        [XmlElement("Status")]
        public string status { get; set; }
        [XmlIgnore]
        public DateTime timeStamp { get; set; }
        [XmlElement("Timestamp")]
        public string timeStampString {
            get { return this.timeStamp.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { this.timeStamp = DateTime.Parse(value); }
        }
        [XmlElement("Data")]
        public List<Datum> data { get; set; }
        # endregion

        # region Inner Classes
        public class Datum {
            public string name { get; set; }
            public string print_tag { get; set; }
            public string rarity { get; set; }
            public PriceData price_data { get; set; }
        }

        public class PriceData {
            public string status { get; set; }
            public Data data { get; set; }
        }

        public class Data {
            public List<object> listings { get; set; }
            public Prices prices { get; set; }
        }

        public class Prices {
            public double high { get; set; }
            public double low { get; set; }
            public double average { get; set; }
            public double shift { get; set; }
            public double shift_3 { get; set; }
            public double shift_7 { get; set; }
            public double shift_21 { get; set; }
            public double shift_30 { get; set; }
            public double shift_90 { get; set; }
            public double shift_180 { get; set; }
            public double shift_365 { get; set; }
            public string updated_at { get; set; }
        }
        # endregion

        # region Public Methods
        public Datum getCardPriceByID(string cardID, string rarity) {
            if (data == null || data.Count == 0) { return null; }
            
            Datum cardPrices = null;
            for (int i = 0; i < data.Count; i++) {
                // Checking that the card ID and rarity are identical.
                if (data[i].print_tag.ToLower().Trim() == cardID.ToLower().Trim() && data[i].rarity.ToLower().Trim() == rarity.ToLower().Trim()) {
                    cardPrices = data[i];
                }
            }

            return cardPrices;
        }
        # endregion
    }
}

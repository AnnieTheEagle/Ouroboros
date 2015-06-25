# region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
# endregion

namespace Ouroboros {
    public class Utilities {
        # region Public Web Methods
        public static string getPageCode(string url) { // Get the HTML page code of a web URL.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            int tries = 0;
            while (true) { // While we dont have a real response.
                tries++;
                HttpWebResponse res = null;
                try { 
                    res = (HttpWebResponse)request.GetResponse(); 
                    if (res.StatusCode == HttpStatusCode.OK) { // If we got an OK status code, return the StreamReader as a string.
                        Stream stream = res.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        return reader.ReadToEnd();
                    }
                    else if (res.StatusCode == HttpStatusCode.Unauthorized || res.StatusCode == HttpStatusCode.Forbidden) { // If we get a forbidden or unauthorized response, return an error
                        return "error";
                    }
                    else { // Retry connection 3 times only.
                        Console.WriteLine("[W] Server returned response code " + res.StatusCode + "... retrying in 4 seconds.");
                        if (tries > Convert.ToInt32(3)) { Console.WriteLine("[E] Cannot get this page code after 3 attempts... URL: " + url); break; } // Give up here, returning null.
                        Thread.Sleep(Convert.ToInt32(4000)); // Wait 4 seconds between requests.
                    }
                }
                catch (WebException ex) { // If we have an exception...
                    if (ex.GetBaseException().Message.Contains("401")) { // Returning 401 from server
                        return "error";
                    }
                    if (ex.GetBaseException().Message.Contains("404")) { // Returning 404 from server
                        return "error";
                    }
                    Console.WriteLine("[E] " + ex.GetBaseException().Message + " ... retrying in 15 seconds."); // Retry every 15 seconds, 3 times.
                    if (tries > Convert.ToInt32(3)) { Console.WriteLine("Cannot get this page code after 3 attempts... URL: " + url); break; } // Give up here, returning null.
                    Thread.Sleep(15000);
                    
                }
            }
            return null;
        }

        public static List<string> getListOfCards(bool verbose) {
            List<string> listOfCards = new List<string>();

            string currentURL = "http://yugioh.wikia.com/wiki/Category:TCG_cards";
            int totalCards = 0;

            while (true) {
                string currentPageCode = Utilities.getPageCode(currentURL);
                string nextPageURL = "";

                int endOfURL = -1;

                // Check if there is a next page to scrub
                if (currentPageCode.IndexOf("(next 200)") == -1) {
                    int startOfURL = currentPageCode.IndexOf(") (<a href=\"") + ") (<a href=\"".Length;
                    endOfURL = currentPageCode.IndexOf("\"", startOfURL + 1);
                    nextPageURL = "http://yugioh.wikia.com" + currentPageCode.Substring(startOfURL, endOfURL - startOfURL);
                }
                else {
                    endOfURL = currentPageCode.IndexOf("(next 200)") + 15;
                }

                // Get the number of cards in this page of the category.
                int startOfCardCountInPage = currentPageCode.IndexOf("The following ") + "The following ".Length;
                int endOfCardCountInPage = currentPageCode.IndexOf(" pages are in this category, out of ");

                int cardCountInPage = Int32.Parse(currentPageCode.Substring(startOfCardCountInPage, endOfCardCountInPage - startOfCardCountInPage));
                totalCards += cardCountInPage;

                if (verbose) { Console.WriteLine("[WEB] " + cardCountInPage.ToString("n0") + " cards on this page (" + totalCards.ToString("n0") + " total so far)"); }

                // Find the end of the card table, and the starting point of our search
                int endOfTable = currentPageCode.IndexOf("</table>", currentPageCode.IndexOf(") ("));
                int startingIDX = endOfURL + 10;

                while (startingIDX < endOfTable) { // Until we reach the end of the table
                    int startOfCardName = currentPageCode.IndexOf("title=\"", startingIDX) + "title=\"".Length;
                    int endOfCardName = currentPageCode.IndexOf("\"", startOfCardName + 1);
                    string cardName = currentPageCode.Substring(startOfCardName, endOfCardName - startOfCardName);

                    if (startOfCardName < endOfTable) { listOfCards.Add(System.Net.WebUtility.HtmlDecode(cardName)); }

                    startingIDX = endOfCardName + 10;
                }

                if (nextPageURL == "") { 
                    break; 
                }
                currentURL = nextPageURL;
            }
            listOfCards.RemoveAll(x => x.StartsWith("Category:")); // Remove any error entries from the scrubbing process
            return listOfCards;
        }

        public static Card getCardData(string s) {
            Card c = new Card();
            string RDFCode = Utilities.getPageCode("http://yugioh.wikia.com/wiki/Special:ExportRDF/" + s.Replace("?", "%3F"));

            // Card Name and Attribute
            c.cardName = s;
            c.cardAttribute = Utilities.getRDFValues("Attribute_Text", RDFCode)[0];
            
            // Card Type and Subtype.
            if (c.cardAttribute.ToLower().Contains("spell") || c.cardAttribute.ToLower().Contains("trap")) { // Spell or Trap Cards
                c.cardType = Utilities.getRDFValues("Card_type_Text", RDFCode)[1];
                c.cardSubType = Utilities.getRDFValues("Types", RDFCode)[0];
            }
            else { // Monster Cards
                c.cardType = Utilities.getRDFValues("Card_type_Text", RDFCode)[0];
                List<string> types = Utilities.getRDFValues("Types", RDFCode);
                string typeString = "";
                List<string> addedTypes = new List<string>();
                for (int i = 0; i < types.Count(); i++) {
                    if (!addedTypes.Contains(types[i])) {
                        addedTypes.Add(types[i]);
                        typeString += types[i] + " / ";
                    }
                    
                }
                c.cardSubType = typeString.Substring(0, typeString.Length - " / ".Length);
            }

            // ATK, DEF and Level/Rank
            c.cardATK = Utilities.getRDFValues("ATK_string", RDFCode)[0];
            c.cardDEF = Utilities.getRDFValues("DEF_string", RDFCode)[0];
            c.cardLvl = c.cardType.ToLower().Contains("xyz") ? Utilities.getRDFValues("Rank", RDFCode)[0] : Utilities.getRDFValues("Level", RDFCode)[0];

            // Card Text
            c.cardText = Utilities.cleanWikiaEncoding(Utilities.getRDFValues("Lore", RDFCode)[0]); // Clean the Wikia Link Encoding first then set it.

            // Pendulum Effect
            if (c.cardType.ToLower().Contains("pendulum")) { // If the card is a pendulum monster, grab the pendulum effect text too.
                string pendulumText = Utilities.getRDFValues("Pendulum_Effect", RDFCode)[0];
                c.cardPendulumEffect = Utilities.cleanWikiaEncoding(pendulumText);
            }

            // List of sets the card is in.
            c.listOfSets = Utilities.getSetsFromWikiaArticle(s); // Get a list of sets from the Wikia Article, rather than RDF. More complete.
            if (c.listOfSets == null) {
                return null;
            }
            if (c.listOfSets.Count() == 0) { // If there is no sets found, there seems to be an error.
                Console.WriteLine("Something wrong occurred with card (NO_SETS): " + s);
            }

            return c;
        }

        public static void getSingleSetFromWikia(string setName) {
            getCardsFromTCGList(setName, "(TCG-EN)");
            getCardsFromTCGList(setName, "(TCG-NA)");
            getCardsFromTCGList(setName, "(TCG-EU)");
        }

        public static void getCardsFromTCGList(string setName, string languageCode) {
            string listCode = getPageCode("http://yugioh.wikia.com/wiki/Set_Card_Lists:" + setName.Replace("?", "%3F") + " " + languageCode);

            int startTable = listCode.IndexOf("<table class=\"wikitable sortable card-list\">");
            int endTable = listCode.IndexOf("</table>", startTable) + "</table>".Length;
            string tableCode = listCode.Substring(startTable, endTable - startTable);

            string[] parts = tableCode.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); // Split the HTML code into lines.
            List<string> dataParts = new List<string>();
            for (int i = 0; i < parts.Length; i++) {
                if (Regex.Replace(parts[i].Trim(), @"<[^>]*>", String.Empty).Trim() != "") { // If it starts with a table-cell
                    dataParts.Add(Regex.Replace(parts[i].Trim(), @"<[^>]*>", String.Empty).Trim()); // Trim string and then remove all HTML tags.
                }
            }
            for (int i = 0; i < 4; i++) { dataParts.RemoveAt(0); } // Remove table headers.

            for (int i = 0; i < (dataParts.Count() / 4); i++) { // Now for each 4-tuple of data-parts...
                // [setID, cardName, setRarity, cardType] // We only care about element 0, 1 and 2.
                Card c = DataStorage.database.getCardByName(dataParts[(4 * i) + 1]);

                SetCard s = new SetCard();
                s.cardSetName = setName; // Grab the set name parameters
                s.cardSetID = dataParts[(4 * i)]; // Grab the set ID from the 4-tuple.
                s.cardSetLanguage = lookupLanguageFromCode(languageCode); // Add the language selected
                s.cardSetRarity = (dataParts[(4 * i) + 3] == "" ? "Unknown" : dataParts[(4 * i) + 2]); // Grab the rarity from the 3-tuple.

                if (c != null) { // If card exists, add new set object.
                    c.listOfSets.Add(s);
                }
                else { // Otherwise we need to try and grab the card data
                    Card newCard = getCardData(dataParts[(4 * i) + 1]);

                    string rarityPair = s.cardSetID + "," + s.cardSetRarity; // Convert two string elements into a pair with comma separation.

                    if (!newCard.getListOfSetNamesCardIsIn().Contains(rarityPair)) { // If this <ID, rarity> pair is not in the list of sets, add it again, to be sure.
                        newCard.listOfSets.Add(s);
                    }
                }
            }
        }

        public static string lookupLanguageFromCode(string languageCode) { // Small method to map the language code to a human-readable language string.
            switch (languageCode) {
                case "(TCG-EN)": return "English - Worldwide";
                case "(TCG-EU)": return "English - Europe";
                case "(TCG-NA)": return "English - North America";
                default: return "English";
            }
        }

        public static List<SetCard> getSetsFromWikiaArticle(string cardName) {
            List<SetCard> listOfSets = new List<SetCard>();
            string articleCode = getPageCode("http://yugioh.wikia.com/wiki/" + cardName.Replace("?", "%3F"));

            if (articleCode == null) { return null; } // Signal an error occured...

            listOfSets = listOfSets.Union(getSetsOfLanguage("English", articleCode)).ToList<SetCard>(); // Add English Sets
            listOfSets = listOfSets.Union(getSetsOfLanguage("English—Worldwide", articleCode)).ToList<SetCard>(); // Add English—Worldwide Sets
            listOfSets = listOfSets.Union(getSetsOfLanguage("English—North America", articleCode)).ToList<SetCard>(); // Add English—North America Sets
            listOfSets = listOfSets.Union(getSetsOfLanguage("English—Europe", articleCode)).ToList<SetCard>(); // Add English—Europe Sets

            return listOfSets; // Finally return the sets.
        }

        public static List<SetCard> getSetsOfLanguage(string language, string articleCode) {
            List<SetCard> listOfSets = new List<SetCard>(); // List of sets for this language.

            int startOfEnglishSets = articleCode.IndexOf(">" + language + "</span>", articleCode.IndexOf("TCG</a></i> sets")); // Find the beginning of the TCG Sets HTML Code for the selected language.
            if (startOfEnglishSets == -1) { return listOfSets; } // No sets for this language, so return nothing basically.
            int endOfEnglishSets = articleCode.IndexOf("</table>", startOfEnglishSets); // Find the end of the code for this language
            
            string englishSetCode = articleCode.Substring(startOfEnglishSets, endOfEnglishSets - startOfEnglishSets); // Cut out the language table code to make it faster to work with

            if (englishSetCode.Contains("wikitable")) {
                string[] parts = englishSetCode.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); // Split the HTML code into lines.
                List<string> dataParts = new List<string>();
                for (int i = 0; i < parts.Length; i++) {
                    if (parts[i].Trim().StartsWith("<td")) { // If it starts with a table-cell
                        dataParts.Add(Regex.Replace(parts[i].Trim(), @"<[^>]*>", String.Empty)); // Trim string and then remove all HTML tags.
                    }
                }

                for (int i = 0; i < (dataParts.Count() / 4); i++) { // Now for each 4-tuple of data-parts...
                    // [NOT USED, setID, setName, setRarity]
                    SetCard s = new SetCard();
                    s.cardSetName = dataParts[4 * i + 2]; // Grab the set name from the 4-tuple.
                    s.cardSetID = dataParts[(4 * i) + 1]; // Grab the set ID from the 4-tuple.
                    s.cardSetLanguage = language.Replace("—", " - "); // Add the language selected
                    s.cardSetRarity = (dataParts[(4 * i) + 3] == "" ? "Unknown" : dataParts[(4 * i) + 3]); // Grab the rarity from the 3-tuple.

                    listOfSets.Add(s); // Add this SetCard to the list
                }
            }
            else {
                string compactSetCode = englishSetCode.Substring(englishSetCode.IndexOf("</th></tr>"), englishSetCode.Length - englishSetCode.IndexOf("</th></tr>"));
                string[] parts = compactSetCode.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); // Split HTML code into lines.
                List<string> dataParts = new List<string>(); ;
                for (int i = 0; i < parts.Length; i++) {
                    parts[i] = Regex.Replace(parts[i].Trim(), @"<[^>]*>", String.Empty); // Trim string and then remove all HTML tags.
                    if (parts[i] != "") { // If line isn't empty, then add it.
                        dataParts.Add(parts[i]);
                    }   
                }
                foreach (string dp in dataParts) { // For each string in the data part.
                    string dpFix = dp.Replace(" - (", " - "); // Remove any erroneous formatting.
                    int splitIDX = dpFix.LastIndexOf(" ("); // Split at the last instance of " ("
                    string setName = dpFix.Substring(0, splitIDX); // First string is the set name.
                    string rarityString = dpFix.Substring(splitIDX + 2); // The rest is the rarity that needs to be split.

                    // Rarities within this set.
                    string[] dpPart2Pieces = rarityString.Split(new string[] { " - " }, StringSplitOptions.None); // Split rarityString to get the SetID and rarities separately.
                    string[] rarities = null;
                    if (dpPart2Pieces.Length != 1) { 
                        dpPart2Pieces[1] = dpPart2Pieces[1].Replace(")", "");
                        rarities = dpPart2Pieces[1].Split('/');
                    }
                    else { // If there was no rarity, then specify "Unknown"
                        rarities = new string[] { "Unknown" };
                    }
                    
                    foreach (string rarity in rarities) { 
                        SetCard s = new SetCard();
                        s.cardSetName = setName; // Grab the set name dpParts
                        s.cardSetID = dpPart2Pieces[0]; // Grab the set ID pieces of dpParts[1]
                        s.cardSetLanguage = language.Replace("—", " - "); // Add the language selected
                        s.cardSetRarity = lookupRarity(rarity); // Grab the rarity from the pieces of dpParts[1]

                        listOfSets.Add(s); // Add this SetCard to the list
                    }
                }
            }

            return listOfSets; // Return the full list of SetCard.
        }
        # endregion

        # region Public Wikia Processing Methods
        public static string cleanWikiaEncoding(string input) { 
            while (true) { // Clean all wikia code from the card text (such as links, etc).
                int currentIDX = input.IndexOf("[["); // Find the beginning of any link
                if (currentIDX == -1) { break; } // String is now all clean.

                int endIDX = input.IndexOf("]]", currentIDX) + 2; // Calculate the end of this link.
                string subString = input.Substring(currentIDX, endIDX - currentIDX); // Cut out the part between "[[" and "]]"
                string originalSubString = subString; // Keep a note of the original substring, so we can replace it later.

                string[] ssParts = subString.Split('|'); // If this link contains an alias, we only want the alias, as this is what is displayed.
                if (ssParts.Length > 1) { // If there is more than one part, then we know there is an alias.
                    subString = ssParts[ssParts.Length - 1]; // So we only get the last part (the alias).
                }

                subString = subString.Replace("[[", "").Replace("]]", ""); // Replace all "[[" and "]]" with nothing, as we've cleaned this part of the string.
                input = input.Replace(originalSubString, subString); // Replace old part with cleaned part in string.
            }
            return input;
        }

        public static List<string> getRDFValues(string key, string RDFcode) { // Method for getting data out of an RDF-formatted XML code.
            List<string> values = new List<string>(); // List of values.
            int currentIDX = 0;

            while (true) { // Until the end of the code...
                if (RDFcode.IndexOf("<property:" + key + " ", currentIDX) == -1) { break; } // Find the next instance of the  key...
                int startIDX = RDFcode.IndexOf(">", RDFcode.IndexOf("<property:" + key + " ", currentIDX)) + 1; // Find the end of the property tag...
                int endIDX = RDFcode.IndexOf("<", startIDX); // Find the beginning of the terminating data tag.

                values.Add(RDFcode.Substring(startIDX, endIDX - startIDX)); // Add the substring (the value) of the RDF Key.

                currentIDX = endIDX + 2; // Update cursor to ensure the same value isn't read multiple times.
            }

            if (values.Count() == 0) { values.Add(""); } // Prevent empty exceptions by adding a blank value if no results.
            return values;
        }

        public static string lookupRarity(string rarityCode) { // Method for mapping a rarity code to the respective proper-name of that rarity.
            switch (rarityCode) { 
                // Commons
                case "C": return "Common";
                case "SP": return "Short Print";
                // Rares
                case "R": return "Rare";
                case "SR": return "Super Rare";
                case "UR": return "Ultra Rare";
                case "UtR": return "Ultimate Rare";
                case "GR": return "Ghost Rare";
                case "PlR": return "Platinum Rare";
                // Secret Rares
                case "ScR": return "Secret Rare";
                case "PScR": return "Prismatic Secret Rare";
                case "PlScR": return "Platinum Secret Rare";
                case "UScR": return "Ultra Secret Rare";
                case "ScUR": return "Secret Ultra Rare";
                // Parallel Rares
                case "PR": return "Parallel Rare";
                case "NPR": return "Normal Parallel Rare";
                case "SPR": return "Super Parallel Rare";
                case "UPR": return "Ultra Parallel Rare";
                case "SFR": return "Starfoil Rare";
                case "MSR": return "Mosaic Rare";
                case "SHR": return "Shatterfoil Rare";
                // Gold Rares
                case "GUR": return "Gold Rare";
                case "GScR": return "Gold Secret Rare";
                case "GGR": return "Ghost Gold Rare";
                // Duel Terminal Rares
                case "DNPR": return "Duel Terminal Normal Parallel Rare";
                case "DNRPR": return "Duel Terminal Normal Rare Parallel Rare";
                case "DRPR": return "Duel Terminal Rare Parallel Rare";
                case "DSPR": return "Duel Terminal Super Parallel Rare";
                case "DUPR": return "Duel Terminal Ultra Parallel Rare";
                case "DScPR": return "Duel Terminal Secret Parallel Rare";
                // Default
                case "Unknown": return "Unknown";
                default: return "Common";
            }
        }
        # endregion

        # region Public Troll-and-Toad Methods
        public static List<CardPrice> getCardPrices(string cardName) {
            List<CardPrice> results = new List<CardPrice>();
            // Retrieve the listing from Troll-and-Toad.
            string pageCode = getPageCode("http://www.trollandtoad.com/products/search.php?search_words=" + cardName.Replace(" ", "+") + "&search_category=4736&search_order=relevance_desc");

            int cursorIDX = 0; // Cursor for adding the results.
            List<string> cardResults = new List<string>();
            while (true) { // Until we can't find anymore.
                int startIDX = pageCode.IndexOf("<div class=\"search_result_wrapper\">", cursorIDX);
                if (startIDX == -1) { break; }
                int endIDX = pageCode.IndexOf("</table></div></div>", startIDX);
                
                // Extract the single result from between the div class and end-of-table tags.
                string singleResult = pageCode.Substring(startIDX, endIDX - startIDX);
                if (singleResult.Contains("Yugioh Card")) { cardResults.Add(pageCode.Substring(startIDX, endIDX - startIDX)); } // Only add this result if it is for a card, and not a box/set/deck.
                cursorIDX = endIDX; // Update the cursor.
            }

            // Process the results.
            for (int i = 0; i < cardResults.Count(); i++) { // For each result...
                string resultCode = cardResults[i];

                string regexPattern = "> - [A-Z]+"; // Matches the first instance of the beginning of a set code in HTML.
                int codeRarityStart = Regex.Match(resultCode, regexPattern).Index + "> - ".Length;
                int codeRarityEnd = resultCode.IndexOf("\"", codeRarityStart);
                string codeAndRarity = resultCode.Substring(codeRarityStart, codeRarityEnd - codeRarityStart);

                // Extract the card's code and rarity.
                string[] crParts = codeAndRarity.Split(new string[] { " - " }, StringSplitOptions.None);
                string cardCode = crParts[0];
                string rarity = "null";

                if (crParts.Length == 1) { // If the rarity is BEFORE the card name, crParts will only have 1 part.
                    // Extract the rarity in the alt-text of the image.
                    rarity = resultCode.Substring(resultCode.IndexOf("alt=\"") + "alt=\"".Length, resultCode.IndexOf(" - ", resultCode.IndexOf("alt=\"")) - (resultCode.IndexOf("alt=\"") + "alt=\"".Length));
                }
                else { // Otherwise extract it from crParts[1], removing any 1st edition and unlimited nonsense.
                    rarity = crParts[1];
                }

                // Clean First Edition and Unlimited Strings
                rarity = rarity.Replace(" 1st Edition", "").Replace(" Unlimited", "");
                cardCode = cardCode.Replace(" 1st Edition", "").Replace(" Unlimited", "");

                Dictionary<string, double> conditionPrices = new Dictionary<string, double>(); // Dictionary mapping condition text to price.

                int resultCursorIDX = 0;
                while (true) { // Until we can no longer find any conditions.
                    int startIDX = resultCode.IndexOf("return false;\">", resultCursorIDX) + "return false;\">".Length;
                    if (startIDX - "return false;\">".Length == -1) { break; }
                    int endIDX = resultCode.IndexOf("</a>", startIDX);

                    // Extract the condition name between the a-href tags.
                    string condition = resultCode.Substring(startIDX, endIDX - startIDX);

                    startIDX = resultCode.IndexOf("\"price_text\">", endIDX) + "\"price_text\">".Length;
                    endIDX = resultCode.IndexOf("</td>", startIDX);

                    // Extract the price between the price_text tags.
                    double price = Double.Parse(resultCode.Substring(startIDX, endIDX - startIDX).Replace("$", ""));

                    if (!conditionPrices.ContainsKey(condition)) { conditionPrices.Add(condition, price); } // If we don't already have this condition, add it.
                    resultCursorIDX = endIDX + 1; // Update the cursor.
                }

                results.Add(new CardPrice(cardName.Trim(), cardCode.Trim(), rarity.Trim(), conditionPrices)); // Add this result in the form of a CardPrice object.
            }
            return results;
        }
        # endregion

        # region Public Database IO Methods

        public static int importOwnedCards(CardDB oldDB, CardDB newDB) {
            int owned = 0; // Number of owned cards imported.
            foreach (Card o in oldDB.listOfCards) { // For each card in the old database.
                Card n = newDB.listOfCards.Find(p => p.cardName == o.cardName); // We find the card in the new database (by checking for identical names).

                foreach (SetCard so in o.listOfSets) { // For each SetCard in the old card's list of sets...
                    SetCard sn = n.listOfSets.Find(s => (s.cardSetID == so.cardSetID && s.cardSetRarity == so.cardSetRarity)); // We find the matching SetCard in the new card's list of set...

                    if (sn != null) {
                        sn.cardOwned = so.cardOwned; // And we copy across the cardOwned state from the old DB to the new DB.
                        if (so.cardOwned) { owned++; } // Add 1 to imported.
                    }
                }
            }
            return owned;
        }

        public static CardDB FromXML<CardDB>(string xml) { // Returns a CardDB out of an input XML string
            using (StringReader stringReader = new StringReader(xml)) {
                XmlSerializer serializer = new XmlSerializer(typeof(CardDB));
                return (CardDB)serializer.Deserialize(stringReader); // Deserialize the XML into an object.
            }
        }

        public static string ToXML<CardDB>(CardDB obj) { // Turns a CardDB object into an XML string.
            using (StringWriter stringWriter = new StringWriter(new StringBuilder())) {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CardDB));
                xmlSerializer.Serialize(stringWriter, obj); // Serialize the object into XML.
                return stringWriter.ToString();
            }
        }

        public static void saveDB(CardDB database) { // Small method to save the database to file.
            database.databaseSaveTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Set the save-time string.
            string xmlDB = ToXML<CardDB>(database);
            try {
                using (StreamWriter sw = new StreamWriter("CardDB_" + database.databaseName + ".xml")) {
                    sw.Write(xmlDB);
                }
            }
            catch (IOException IOX) {
                Console.WriteLine("An error occured trying to save the DB... retrying in 5 seconds. " + IOX.StackTrace);
                Thread.Sleep(5000);
                using (StreamWriter sw = new StreamWriter("CardDB_" + database.databaseName + ".xml")) {
                    sw.Write(xmlDB);
                }
            }
        }

        public static CardDB loadDB(string dbName, DatabaseUpdater parent) { // Loads the DB from the file.
            parent.addMessageToLog("Loading the database file: CardDB_" + dbName + ".xml");

            if (File.Exists("CardDB_" + dbName + ".xml")) { // If it exists, load the file using the deserializer.
                string xmlDB = "";
                using (StreamReader indexer = new StreamReader("CardDB_" + dbName + ".xml")) {
                    xmlDB = indexer.ReadToEnd();
                }
                CardDB db = Utilities.FromXML<CardDB>(xmlDB);
                parent.addMessageToLog("Successfully loaded card database, there are " + db.listOfCards.Count().ToString("n0") + " cards currently stored.");
                return db;
            }
            else { // Otherwise create a new DB.
                parent.addMessageToLog("There is no card database with that name, creating a new one...");
                CardDB db = new CardDB();
                db.databaseName = dbName;
                return db;
            }
        }
        # endregion
    }
}

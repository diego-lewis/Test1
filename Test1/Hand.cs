namespace Test1
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public Dictionary<string, int> Pairs { get; set; }
        public Hand()
        {
            Cards = new List<Card>();
            Pairs = new Dictionary<string, int>();
        }
        /// <summary>
        /// Takes a string and parses it out into a 5 card poker hand
        /// </summary>
        /// <param name="input">string to represent cards</param>
        /// <returns>Successfull or not</returns>
        public bool getHand(string input)
        {
            Cards = new List<Card>();
            input = input.Trim();
            string[] scards = input.Split(' ');

            foreach (string sc in scards)
            {
                if (!string.IsNullOrEmpty(sc))
                {
                    int i = sc.Length - 1;
                    if (i > 0)
                    {
                        string r = sc.Substring(0, i);
                        string st = sc.Substring(i);
                        Card thisCard = new Card();
                        if (thisCard.assign(r, st))
                        {
                            Cards.Add(thisCard);
                        }
                    }
                }
            }

            return Cards.Count == 5;
        }
        /// <summary>
        /// Evaluates hand
        /// </summary>
        /// <returns>A string that discribes how the hand would play in poker</returns>
        public string readHand()
        {
            string result = string.Empty;
            if (Cards.Count != 5)
            {
                return "Error The card count is not 5";
            }
            Pairs = new Dictionary<string, int>();
            Combinations();
            bool isStraight = Straight();
            bool isFlush = Flush();
            Ranks highCard = HighCard();
            List<string> sigs = SignificantCards();
            int highCombo = 1;
            int lowCombo = 1;
            List<string> combo = Pairs.Keys.ToList();
            foreach (string c in combo)
            {
                if (Pairs[c] >= highCombo)
                {
                    lowCombo = highCombo;
                    highCombo = Pairs[c];
                }
                else if (Pairs[c] > lowCombo)
                {
                    lowCombo = Pairs[c];
                }
            }

            if (isStraight && isFlush && highCard == Ranks.Ace)
            {
                return "Royal Flush";
            }
            if (isStraight && isFlush)
            {
                return string.Format("Straight Flush - {0} high", highCard.ToString());
            }
            
            if (highCombo == 4)
            {
                return string.Format("Four of a kind - {0}", sigs.FirstOrDefault());
            }
            if (highCombo == 3 && lowCombo == 2)
            {
                return string.Format("Full House, {0} over {1}", sigs.FirstOrDefault(), sigs.LastOrDefault());
            }
            if (isFlush)
            {
                return "Flush";
            }
            if (isStraight)
            {
                return string.Format("Straight - {0} high", highCard.ToString());
            }
            if (highCombo == 3)
            {
                return string.Format("Three of a kind - {0}", sigs.FirstOrDefault());
            }
            if (lowCombo == 2)
            {
                return string.Format("Two Pair - {0} and {1}", sigs.FirstOrDefault(), sigs.LastOrDefault());
            }
            if (highCombo == 2)
            {
                return string.Format("Pair of {0}", sigs.FirstOrDefault());
            }
            return highCard.ToString() + " high";
        }

        private bool Straight()
        {
            bool isStraight = true;
            Ranks temp = Ranks.Two;
            Ranks[] s = Cards.OrderByDescending(x => x.Rank).Select(c => c.Rank).ToArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (i != 0)
                {
                    isStraight &= temp -1 == s[i];
                }
                temp = s[i];
            }
            return isStraight; 
        }

        private bool Flush()
        {
            bool isFlush = true;
            var temp = Cards.FirstOrDefault().Suit;
            foreach (var card in Cards)
            {
                isFlush &= card.Suit == temp;
            }
            return isFlush;
        }

        private Ranks HighCard()
        {
            Ranks temp = Ranks.Two;
            Ranks[] s = Cards.OrderByDescending(x => x.Rank).Select(c => c.Rank).ToArray();
            return s[0];
        }

        private void Combinations()
        {
            foreach(Card card in Cards)
            {
                string rank = card.Rank.ToString();
                if (Pairs.ContainsKey(rank))
                {
                    Pairs[rank] = Pairs[rank] + 1;
                }
                else
                {
                    Pairs.Add(rank, 1);
                }
            }
        }

        private List<string> SignificantCards()
        {
            List<string> cards = new List<string>();
            var sig = from entry in Pairs where entry.Value > 1 orderby entry.Value descending select entry;
            foreach (var item in sig)
            {
                string formated = item.Key;
                if (formated == "Six") formated += "e";
                formated += "s";
                cards.Add(formated);
            }
            return cards;
        }
    }
}

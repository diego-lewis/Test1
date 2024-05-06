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

        public bool getHand(string input)
        {
            Cards = new List<Card>();
            input = input.Trim();
            string[] scards = input.Split(' ');
            if (scards.Length == 5 ) 
            {
                return false;
            }

            return true;
        }

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

            if (isStraight && isFlush && highCard == Ranks.Ace)
            {
                return "Royal Flush";
            }
            if (isStraight && isFlush)
            {
                return "Straight Flush";
            }
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
            if (highCombo == 4)
            {
                return "Four of a kind";
            }
            if (highCombo == 3 && lowCombo == 2)
            {
                return "Full House";
            }
            if (isFlush)
            {
                return "Flush";
            }
            if (isStraight)
            {
                return "Straight";
            }
            if (highCombo == 3)
            {
                return "Three of a kind";
            }
            if (lowCombo == 2)
            {
                return "Two Pair";
            }
            if (highCombo == 2)
            {
                return "Pair";
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
                    isStraight &= temp + 1 == s[i];
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
            return s[4];
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
    }
}

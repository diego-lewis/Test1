namespace Test1
{
    public class Card
    {
        public Suits Suit { get; set; }
        public Ranks Rank { get; set; }

        public bool assign(string r, string s)
        {
            bool valid = true;
            switch(s)
            {
                case "H":
                    Suit = Suits.Hearts;
                    break;
                case "D":
                    Suit = Suits.Diamonds;
                    break;
                case "C":
                    Suit = Suits.Clubs;
                    break;
                case "S":
                    Suit = Suits.Spades;
                    break;
                default:
                    valid = false; 
                    break;
            }

            int notFace;
            if (int.TryParse(r, out notFace))
            {
                if (notFace >= 2 && notFace <= 10)
                {
                    Rank = (Ranks)notFace;
                }
                else 
                { 
                    valid = false;
                }
            }
            else
            {
                switch(r)
                {
                    case "J":
                        Rank = Ranks.Jack;
                        break;
                    case "Q":
                        Rank = Ranks.Queen;
                        break;
                    case "K":
                        Rank = Ranks.King;
                        break;
                    case "A":
                        Rank = Ranks.Ace;
                        break;
                    default:
                        valid = false;
                        break;
                }
            }
            return valid;
        }
    }

    public enum Suits { 
        Hearts = 0,
        Diamonds = 1,
        Clubs = 2,
        Spades = 3
    }

    public enum Ranks { 
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14
    }
}

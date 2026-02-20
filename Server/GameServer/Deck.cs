using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameServer
{
    public readonly struct Deck
    {
        public readonly int[] heroIds;
        public readonly int[] cardIds;

        public Deck(int[] heroIds, int[] cardIds)
        {
            this.heroIds = heroIds;
            this.cardIds = cardIds;
        }

        public override string ToString()
        {
            string heroText = "";
            string cardText = "";

            for (int i = 0; i < heroIds.Length; i++)
            {
                heroText += heroIds[i].ToString() + ", ";
            }

            for (int i = 0; i < cardIds.Length; i++)
            {
                cardText += cardIds[i].ToString() + ", ";
            }

            return $"deckInfo :\n   heroCount : {heroIds.Length}, cardCount : {cardIds.Length}\n    hero : {heroText}\n    card : {cardText}";
        }
    }
}

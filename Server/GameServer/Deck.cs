using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

using HeroCollection = int[];
using CardCollection = Server.MultiSet<int>;

namespace Server.GameServer
{
    public readonly struct Deck
    {
        public readonly HeroCollection heroIds;
        public readonly CardCollection cardIds;

        public Deck(HeroCollection heroIds, CardCollection cardIds)
        {
            this.heroIds = heroIds;
            this.cardIds = cardIds;
        }

        public Deck(List<int> heroIds,List<int> cardIds)
        {
            this.heroIds = heroIds.ToArray();
            this.cardIds = new(cardIds);
        }

        public override string ToString()
        {
            string heroText = "";
            string cardText = "";

            foreach (var item in heroIds)
            {
                heroText += item.ToString() + ", ";
            }

            foreach (var item in cardIds)
            {
                cardText += item + ", ";
            }

            return $"deckInfo :\n   heroCount : {heroIds.Length}, cardCount : {cardIds.TotalCount}\n    hero : {heroText}\n    card : {cardText}";
        }
    }
}

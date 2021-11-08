using Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Game
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public GameEvent Event { get; set; }
        public string CurrentUserId { get; set; }
        public string NextUserId { get; set; }
        public GameCard NextCard { get; set; }
        public List<GameCard> Deck { get; set; } = new List<GameCard>();
        public List<GameCard> DiscardPile { get; set; } = new List<GameCard>();
        public List<Character> Characters { get; set; } = new List<Character>();
        public List<Message> Messages { get; set; } = new List<Message>();
        
        public List<GameCard> GetCards(int count) 
        {
            if(Deck.Count < count) 
            {
                var lastCard = DiscardPile.Last();
                DiscardPile.Remove(lastCard);
                var rnd = new Random();
                Deck.AddRange(DiscardPile.OrderBy(item => rnd.Next()).ToList());
                DiscardPile = new List<GameCard> { lastCard };
            }

            return Deck.Take(count).ToList();
        }

        public List<GameCard> GetAndRemoveCards(int count)
        {
            var result = GetCards(count);
            Deck.RemoveRange(0, count);
            return result;
        }
        public Character GetCharacterById(string userId)
        {
            return Characters.SingleOrDefault(x => x.Id == userId);
        }

        public List<string> Neigbours(string playerId) 
        {
            var playerCharacter = Characters.Where(x => x.Id == playerId).SingleOrDefault();

            if(playerCharacter is null) 
            {
                //TODO: Exception
            }

            List<string> result = new List<string>();

            if(playerCharacter.AimDistance >= Characters.Count()) 
            {
                result.AddRange(Characters.Select(x => x.Id).ToList());
                return result;
            }

            var indexof = Characters.IndexOf(playerCharacter);
            var startpos = indexof - playerCharacter.AimDistance < 0 ? indexof - playerCharacter.AimDistance - Characters.Count : indexof - playerCharacter.AimDistance;
            var stoppos = indexof + playerCharacter.AimDistance >= Characters.Count ? indexof + playerCharacter.AimDistance - Characters.Count : indexof + playerCharacter.AimDistance;

            for (int i = startpos; i == stoppos; i++)
            {
                if (i >= Characters.Count)
                    i = i - Characters.Count;
                //Megnézni hogy a sima vagy a plusz listahossz a közelebbi érték if nagyobbak vagyunk az indexnél akkor + count ha nem akkor count - index
            }

            return result;
        }

        public List<string> GetAllPlayer()
        {
            return Characters.Select(x => x.Id).ToList();
        }
    }
}

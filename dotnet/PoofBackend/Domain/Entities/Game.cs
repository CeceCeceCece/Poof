using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Card> Deck { get; set; } = new List<Card>();
        public List<Character> Characters { get; set; } = new List<Character>();

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
    }
}

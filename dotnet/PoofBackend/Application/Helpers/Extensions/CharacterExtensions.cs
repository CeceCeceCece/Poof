using Application.Constants;
using Application.Exceptions;
using Application.Models.CharacterLogic;
using Application.Models.CharacterLogics;
using Application.SignalR;

namespace Domain.Entities
{
    public static class CharacterExtensions
    {
        public static BaseCharacterLogic Map(this Character character, PoofGameHub hub) => character.PersonalCard.Name switch
        {
            "Bart Cassidy" => new BartCassidyCharacter(character, hub),
            "Black Jack" => new BlackJackCharacter(character, hub),
            "El Gringo" => new ElGringoCharacter(character, hub),
            "Jesse Jones" => new JesseJonesCharacter(character, hub),
            "Kit Carlson" => new KitCarlsonCharacter(character, hub),
            "Paul Regret" => new PaulRegretCharacter(character, hub),
            "Pedro Ramirez" => new PedroRamirezCharacter(character, hub),
            "Rose Doolan" => new RoseDoolanCharacter(character, hub),
            "Sid Ketchum" => new SidKetchumCharacter(character, hub),
            "Suzy Lafayette" => new SuzyLafayetteCharacter(character, hub),
            "Willy the Kid" => new WillytheKidCharacter(character, hub),
            _ => throw new PoofException(CharacterMessages.ILYEN_KARAKTER_NEM_LETEZIK)
        };
    }
}

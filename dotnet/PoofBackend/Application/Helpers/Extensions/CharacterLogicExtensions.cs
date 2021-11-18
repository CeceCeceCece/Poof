using Application.Constants;
using Application.Exceptions;
using Application.Models;
using Application.Models.CharacterLogic;
using Application.Models.CharacterLogics;
using Application.SignalR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public static class CharacterLogicExtensions
    {
        public static BaseCharacterLogic Map(this Character character, PoofGameHub hub) => character.Name switch
        {
            "Bart Cassidy" => new BartCassidyCharacter(character, hub),
            "Black Jack" => new BlackJackCharacter(character, hub),
            "Calamity Janet" => new CalamityJanetCharacter(character, hub),
            "El Gringo" => new ElGringoCharacter(character, hub),
            "Jesse Jones" => new JesseJonesCharacter(character, hub),
            "Jourdonnais" => new JourdonnaisCharacter(character, hub),
            "Kit Carlson" => new KitCarlsonCharacter(character, hub),
            "Lucky Duke" => new LuckyDukeCharacter(character, hub),
            "Paul Regret" => new PaulRegretCharacter(character, hub),
            "Pedro Ramirez" => new PedroRamirezCharacter(character, hub),
            "Rose Doolan" => new RoseDoolanCharacter(character, hub),
            "Sid Ketchum" => new SidKetchumCharacter(character, hub),
            "Slab the Killer" => new SlabtheKillerCharacter(character, hub),
            "Suzy Lafayette" => new SuzyLafayetteCharacter(character, hub),
            "Vulture Sam" => new VultureSamCharacter(character, hub),
            "Willy the Kid" => new WillytheKidCharacter(character, hub),
            _ => throw new PoofException(CharacterMessages.ILYEN_KARAKTER_NEM_LETEZIK)
        };
    }
}

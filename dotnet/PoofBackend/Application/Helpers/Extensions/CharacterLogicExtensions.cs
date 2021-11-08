using Application.Constants;
using Application.Exceptions;
using Application.Models;
using Application.Models.CharacterLogic;
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
        public static CharacterLogic Map(this Character character) => character.Name switch
        {
            "Bart Cassidy" => new BartCassidyCharacter(character),
            "Black Jack" => new BlackJackCharacter(character),
            "Calamity Janet" => new CalamityJanetCharacter(character),
            "El Gringo" => new ElGringoCharacter(character),
            "Jesse Jones" => new JesseJonesCharacter(character),
            "Jourdonnais" => new JourdonnaisCharacter(character),
            "Kit Carlson" => new KitCarlsonCharacter(character),
            "Lucky Duke" => new LuckyDukeCharacter(character),
            "Paul Regret" => new PaulRegretCharacter(character),
            "Pedro Ramirez" => new PedroRamirezCharacter(character),
            "Rose Doolan" => new RoseDoolanCharacter(character),
            "Sid Ketchum" => new SidKetchumCharacter(character),
            "Slab the Killer" => new SlabtheKillerCharacter(character),
            "Suzy Lafayette" => new SuzyLafayetteCharacter(character),
            "Vulture Sam" => new VultureSamCharacter(character),
            "Willy the Kid" => new WillytheKidCharacter(character),
            _ => throw new PoofException(CharacterMessages.ILYEN_KARAKTER_NEM_LETEZIK)
        };
    }
}

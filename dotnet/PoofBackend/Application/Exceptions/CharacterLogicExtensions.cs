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
            _ => throw new PoofException()
        };
    }
}

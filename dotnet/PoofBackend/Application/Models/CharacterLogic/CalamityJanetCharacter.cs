using Application.Constants;
using Application.Exceptions;
using Application.Models.CardLogic;
using Application.Models.DTOs;
using Application.SignalR;
using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class CalamityJanetCharacter : BaseCharacterLogic
    {
        public CalamityJanetCharacter(Character character, PoofGameHub hub) :base(character, hub){}

        public override async Task CardOptionAsync(string cardId)
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            if(card.Card.Name == "Missed!") 
            {
                await new BangCardLogic(card).OptionAsync(this);
            }
            else 
            {
                await card.Map().OptionAsync(this);
            }
        }

        public override async Task ActivateCardAsync(string cardId, OptionDto option)
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId);
            if (card is null)
                throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            if(card.Card.Name == "Missed!") 
            {
                await new BangCardLogic(card).ActivateAsync(this, option);
            }
            else 
            {
                await card.Map().ActivateAsync(this, option);
            }
        }

        public override async Task AnswearCardAsync(OptionDto dto)
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == dto.CardIds.First()); 

            if (Character.Game.NextCard.Card.Name == "Indians!") 
            {
            
            }
            //TODO implementálás
        }

        public override async Task CheckAnswearCardAsync(OptionDto dto)
        {
            if (Character.Game.NextCard != null && Character.Game.NextCard.Card.Name == "Missed!")
            {
                await new BangCardLogic(Character.Game.NextCard).AnswearAsync(this, dto);
            }
            else
            {
                await Character.Game.NextCard.Map().AnswearAsync(this, dto);
            }
        }
    }
}

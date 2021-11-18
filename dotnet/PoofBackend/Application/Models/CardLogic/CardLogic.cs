using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public abstract class CardLogic
    {
        public GameCard Card { get; set; }

        public CardLogic(GameCard card)
        {
            Card = card;
        }

        public abstract Task OptionAsync(BaseCharacterLogic character);
        public abstract Task ActivateAsync(BaseCharacterLogic character, OptionDto dto);
        public virtual Task AnswearAsync(BaseCharacterLogic character, OptionDto dto) { return Task.CompletedTask; }
        public virtual Task DeactivateAsync(BaseCharacterLogic character) { return Task.CompletedTask; }
        public virtual Task OnActiveAsync(BaseCharacterLogic character) { return Task.CompletedTask; }
    }
}

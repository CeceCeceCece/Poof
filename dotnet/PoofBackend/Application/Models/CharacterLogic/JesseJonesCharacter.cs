using Application.Constants;
using Application.Exceptions;
using Application.Models.DTOs;
using Application.SignalR;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class JesseJonesCharacter : BaseCharacterLogic
    {

        public JesseJonesCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

        public override async Task DrawAsync()
        {
            Character.Game.Event = GameEvent.Draw;
            await Hub.Clients.Client(Character.ConnectionId).SetGameEvent(new GameEventViewModel(GameEvent.Draw, Character.Id, null));
            //HUB draw event és válaszolni kell

            //return new Option
            //{
            //    Description = CardMessages.CHOOSE_ONE_PLAYER,
            //    NumberOfCards = 0,
            //    PossibleCards = null,
            //    PossibleTargets = game.GetAllPlayer(),
            //    RequireAnswear = true,
            //    RequireCards = false
            //};
        }

        public override async Task DrawReactAsync(OptionDto option)
        {
            if (string.IsNullOrEmpty(option.UserId) || option.UserId == Character.Id)
            {
                var cards = Character.Game.GetAndRemoveCards(2);
                await DrawAsync(cards);
            }
            else
            {
                var card = await Character.Game.GetCharacterById(option.UserId).Map(Hub).LeaveCardRandomAsync();

                var cards = Character.Game.GetAndRemoveCards(1);
                cards.Add(card);

                await DrawAsync(cards);
            }

            await Character.Game.EndReactionAsync(Hub);
        }
    }
}

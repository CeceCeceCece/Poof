﻿using Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Characters
{
    public class PaulRegretCharacterCard : CharacterCard
    {
        public override Character ToCharacter(Connection connection, RoleType role)
        {
            return new Character
            {
                Id = connection.UserId,
                Name = connection.Username,
                DistanceFromOthers = 2,
                BangState = BangState.One,
                ConnectionId = connection.ConnectionId,
                AimDistance = 0,
                Deck = new List<GameCard>(),
                EquipedCards = new List<GameCard>(),
                LifePoint = 3,
                MaxLifePoint = 3,
                PersonalCard = this,
                Role = role,
                Weapon = null,
                WeaponDistance = 1
            };
        }
    }
}
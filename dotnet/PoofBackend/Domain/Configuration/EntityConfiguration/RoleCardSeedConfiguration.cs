using Domain.Constants.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configuration.EntityConfiguration
{
    public class RoleCardSeedConfiguration : IEntityTypeConfiguration<RoleCard>
    {
        public void Configure(EntityTypeBuilder<RoleCard> builder)
        {
            builder.HasData(
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Bart Cassidy", Description = "Each time he is hit, he draws a card", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Black Jack", Description = "He shows the second card he draw. On Heart or Diamonds, he draws one more card", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Calamity Janet", Description = "She can use BANG! cards as Missed! cards and vice versa. If she plays a Missed! as a BANG!, she cannot play another BANG! card that turn (unless she has a Volcanic in play).", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "El Gringo", Description = "Each time he loses a life point due to a card played by another player, he draws a random card from the hands of that player (one card for each life point). If that player has no more cards, too bad!, he does not draw. Note that Dynamite damages are not caused by any player.", LifePoint = 3},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Jesse Jones", Description = "During phase 1 of his turn, he may choose to draw the first card from the deck, or randomly from the hand of any other player. Then he draws the second card from the deck.", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Jourdonnais", Description = "He is considered to have a Barrel in play at all times; he can draw! when he is the target of a BANG!, and on a Heart he is missed. If he has another real Barrel card in play, he can count both of them, giving him two chances to cancel the BANG! before playing a Missed!.", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Kit Carlson", Description = "During phase 1 of his turn, he looks at the top three cards of the deck: he chooses 2 to draw, and puts the other one back on the top of the deck, face down.", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Lucky Duke", Description = "Each time he is required to draw!, he flips the top two cards from the deck and chooses the result he prefers. Discard both cards afterward.", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Paul Regret", Description = "he is considered to have a Mustang in play at all times; all other players must add 1 to the distance to him. If he has another real Mustang in play, he can count both of them, increasing all distances to him by a total of 2.", LifePoint = 3},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Pedro Ramirez", Description = "During phase 1 of his turn, he may choose to draw the first card from the top of the discard pile or from the deck. Then, he draws the second card from the deck.", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Rose Doolan", Description = "She is considered to have a Scope in play at all times; she sees the other players at a distance decreased by 1. If she has another real Scope in play, she can count both of them, reducing her distance to all other players by a total of 2.", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Sid Ketchum", Description = "At any time, he may discard 2 cards from his hand to regain one life point. If he is willing and able, he can use this ability more than once at a time. But remember: you cannot have more life points than your starting amount!", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Slab the Killer", Description = "Players trying to cancel his BANG! cards need to play 2 Missed!. The Barrel effect, if successfully used, only counts as one Missed!.", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Suzy Lafayette", Description = "As soon as she has no cards in her hand, she draws a card from the draw pile.", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Vulture Sam", Description = "Whenever a character is eliminated from the game, Sam takes all the cards that player had in his hand and in play, and adds them to his hand.", LifePoint = 4},
                new RoleCard { Id = Guid.NewGuid().ToString(), Name = "Willy the Kid", Description = "He can play any number of BANG! cards during his turn.", LifePoint = 4}
                );
        }
    }
}

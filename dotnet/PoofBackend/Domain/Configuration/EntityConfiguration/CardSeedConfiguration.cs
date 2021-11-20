﻿using Domain.Constants.Enums;
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
    public class CardSeedConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasData(
                new Card { Id = Guid.NewGuid().ToString(), Name = "Barrel", Description = "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", Value = CardValues.Queen, Suite = CardSuits.Spades, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Barrel", Description = "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", Value = CardValues.King, Suite = CardSuits.Spades, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Dynamite", Description = "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", Value = CardValues.Two, Suite = CardSuits.Hearths, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Dynamite", Description = "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", Value = CardValues.Two, Suite = CardSuits.Hearths, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Jail", Description = "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", Value = CardValues.Jack, Suite = CardSuits.Spades, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Jail", Description = "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", Value = CardValues.Four, Suite = CardSuits.Hearths, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Jail", Description = "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", Value = CardValues.Ten, Suite = CardSuits.Spades, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Mustang", Description = "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", Value = CardValues.Eight, Suite = CardSuits.Hearths, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Mustang", Description = "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", Value = CardValues.Eight, Suite = CardSuits.Hearths, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Remington", Description = "", Value = CardValues.King, Suite = CardSuits.Clubs, Type = CardType.Weapon},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Carabine", Description = "", Value = CardValues.Ace, Suite = CardSuits.Clubs, Type = CardType.Weapon},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Schofield", Description = "", Value = CardValues.Jack, Suite = CardSuits.Clubs, Type = CardType.Weapon},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Schofield", Description = "", Value = CardValues.Queen, Suite = CardSuits.Clubs, Type = CardType.Weapon},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Schofield", Description = "", Value = CardValues.King, Suite = CardSuits.Clubs, Type = CardType.Weapon},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Volcanic", Description = "", Value = CardValues.Ten, Suite = CardSuits.Clubs, Type = CardType.Weapon},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Volcanic", Description = "", Value = CardValues.Ten, Suite = CardSuits.Spades, Type = CardType.Weapon},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Winchester", Description = "", Value = CardValues.Ten, Suite = CardSuits.Clubs, Type = CardType.Weapon},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Scope", Description = "When you have a Scope in play, you see all the other players at a distance decreased by 1.", Value = CardValues.Ace, Suite = CardSuits.Spades, Type = CardType.Equipment},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Ace, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Two, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Three, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Four, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Five, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Six, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Seven, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Eight, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Nine, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Ten, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Jack, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Queen, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.King, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Ace, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Two, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Three, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Four, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Five, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Six, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Seven, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Eight, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Nine, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Queen, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.King, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Bang!", Description = "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", Value = CardValues.Ace, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Beer", Description = "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", Value = CardValues.Six, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Beer", Description = "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", Value = CardValues.Seven, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Beer", Description = "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", Value = CardValues.Eight, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Beer", Description = "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", Value = CardValues.Nine, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Beer", Description = "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", Value = CardValues.Ten, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Beer", Description = "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", Value = CardValues.Jack, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Cat Balou", Description = "Force any one player to discard a card, regardless of the distance.", Value = CardValues.King, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Cat Balou", Description = "Force any one player to discard a card, regardless of the distance.", Value = CardValues.Nine, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Cat Balou", Description = "Force any one player to discard a card, regardless of the distance.", Value = CardValues.Ten, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Cat Balou", Description = "Force any one player to discard a card, regardless of the distance.", Value = CardValues.Jack, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Stagecoach", Description = "Draw two cards from the top of the deck", Value = CardValues.Nine, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Stagecoach", Description = "Draw two cards from the top of the deck", Value = CardValues.Nine, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Duel", Description = "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", Value = CardValues.Queen, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Duel", Description = "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", Value = CardValues.Jack, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Duel", Description = "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", Value = CardValues.Eight, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Gatling", Description = "The Gatling shoots a BANG! to all the other players, regardless of the distance. Even though the Gatling shoots a BANG! to all the other players, it is not considered a BANG! card. During your turn you can play any number of Gatling, but only one BANG! card.", Value = CardValues.Ten, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "General Store", Description = "When you play this card, turn as many cards from the deck face up as the players still playing. Starting with you and proceeding clockwise, each player chooses one of those cards and puts it in his hands.", Value = CardValues.Nine, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "General Store", Description = "When you play this card, turn as many cards from the deck face up as the players still playing. Starting with you and proceeding clockwise, each player chooses one of those cards and puts it in his hands.", Value = CardValues.Queen, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Indians!", Description = "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", Value = CardValues.King, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Indians!", Description = "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", Value = CardValues.Ace, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Ten, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Jack, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Queen, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.King, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Ace, Suite = CardSuits.Clubs, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Two, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Three, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Four, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Five, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Six, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Seven, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Missed!", Description = "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", Value = CardValues.Eight, Suite = CardSuits.Spades, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Panic!", Description = "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", Value = CardValues.Jack, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Panic!", Description = "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", Value = CardValues.Queen, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Panic!", Description = "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", Value = CardValues.Ace, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Panic!", Description = "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", Value = CardValues.Eight, Suite = CardSuits.Diamonds, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Saloon", Description = "Cards with symbols on two lines have two simultaneous effects, one for each line. Here symbols say: Regain one life point, and this applies to All the other players, and on the next line: [You] regain one life point. The overall effect is that all players in play regain one life point. You cannot play a Saloon out of turn when you are losing your last life point: the Saloon is not a Beer!", Value = CardValues.Five, Suite = CardSuits.Hearths, Type = CardType.Action},
                new Card { Id = Guid.NewGuid().ToString(), Name = "Wells Fargo", Description = "Draw three cards from the top of the deck", Value = CardValues.Three, Suite = CardSuits.Hearths, Type = CardType.Action}
                );
        }
    }
}
using Application.Exceptions;
using Application.Models.CardLogic;

namespace Domain.Entities
{
    public static class CardLogicExtensions
    {
        public static CardLogic Map(this GameCard card) => card.Card.Name switch
        {
            "Barrel" => new BarrelCardLogic(card),
            "Dynamite" => new DynamiteCardLogic(card),
            "Jail" => new JailCardLogic(card),
            "Mustang" => new MustangCardLogic(card),
            "Remington" => new RemingtonCardLogic(card),
            "Carabine" => new RevCarabineCardLogic(card),
            "Schofield" => new SchofieldCardLogic(card),
            "Volcanic" => new VolcanicCardLogic(card),
            "Winchester" => new WinchesterCardLogic(card),
            "Scope" => new ScopeCardLogic(card),
            "Bang!" => new BangCardLogic(card),
            "Beer" => new BeerCardLogic(card),
            "Cat Balou" => new CatBalouCardLogic(card),
            "Stagecoach" => new StagecoachCardLogic(card),
            "Duel" => new DuelCardLogic(card),
            "Gatling" => new GatlingCardLogic(card),
            "Indians!" => new IndiansCardLogic(card),
            "Missed!" => new MissedCardLogic(card),
            "Panic!" => new PanicCardLogic(card),
            "Saloon" => new SaloonCardLogic(card),
            "Wells Fargo" => new WellsFargoCardLogic(card),
            _ => throw new PoofException()
        };
    }
}

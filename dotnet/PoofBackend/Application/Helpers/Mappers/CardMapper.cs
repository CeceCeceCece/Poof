using Application.Exceptions;
using Application.Models.CardLogic;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Mappers
{
    public class CardMapper
    {
        private readonly IMapper mapper;

        public CardMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public CardLogic Map(Card card) => card.Name switch
        {
            "Barrel" => mapper.Map<BangCardLogic>(card),
            "Dynamite" => mapper.Map<DynamiteCardLogic>(card),
            "Jail" => mapper.Map<JailCardLogic>(card),
            "Mustang" => mapper.Map<MustangCardLogic>(card),
            "Remington" => mapper.Map<RemingtonCardLogic>(card),
            "Carabine" => mapper.Map<RevCarabineCardLogic>(card),
            "Schofield" => mapper.Map<SchofieldCardLogic>(card),
            "Volcanic" => mapper.Map<VolcanicCardLogic>(card),
            "Winchester" => mapper.Map<WinchesterCardLogic>(card),
            "Scope" => mapper.Map<ScopeCardLogic>(card),
            "Bang" => mapper.Map<BangCardLogic>(card),
            _ => throw new PoofException()
        };
    }
}

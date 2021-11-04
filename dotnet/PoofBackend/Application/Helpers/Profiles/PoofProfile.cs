using Application.Models.CardLogic;
using Application.Models.ViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Profiles
{
    public class PoofProfile : Profile
    {
        public PoofProfile()
        {
            //Card
            CreateMap<Card, BangCardLogic>().ReverseMap();
            CreateMap<Card, BarrelCardLogic>().ReverseMap();
            CreateMap<Card, DynamiteCardLogic>().ReverseMap();
            CreateMap<Card, JailCardLogic>().ReverseMap();
            CreateMap<Card, MustangCardLogic>().ReverseMap();
            CreateMap<Card, RemingtonCardLogic>().ReverseMap();
            CreateMap<Card, RevCarabineCardLogic>().ReverseMap();
            CreateMap<Card, SchofieldCardLogic>().ReverseMap();
            CreateMap<Card, ScopeCardLogic>().ReverseMap();
            CreateMap<Card, VolcanicCardLogic>().ReverseMap();
            CreateMap<Card, WinchesterCardLogic>().ReverseMap();

            //Lobby
            CreateMap<Lobby, LobbyViewModel>().ReverseMap();

            //Message
            CreateMap<Message, MessageViewModel>().ReverseMap();
        }
    }

}

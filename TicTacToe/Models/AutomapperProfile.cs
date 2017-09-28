using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<DtoFields, Fields>();

            CreateMap<Fields, DtoFields>()
              .ForMember("PlayerName", opt => opt.MapFrom(c => c.Game.PlayerName))              
              .ForMember("LevelName", opt => opt.MapFrom(c => c.Game.Level.LevelName))
              .ForMember("PlayerTeamName", opt => opt.MapFrom(c => c.Game.PlayerTeamName))
              .ForMember("PlayerTeamId", opt => opt.MapFrom(c => c.Game.PlayerTeamId)); 

        }
    }
}
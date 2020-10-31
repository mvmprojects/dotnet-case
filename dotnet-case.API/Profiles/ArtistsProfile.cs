using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_case.BL.Models;

namespace dotnet_case.API.Profiles
{
    public class ArtistsProfile : Profile
    {
        public ArtistsProfile()
        {
            CreateMap<ArtistModel, Dtos.ArtistDto>();
        }
    }
}

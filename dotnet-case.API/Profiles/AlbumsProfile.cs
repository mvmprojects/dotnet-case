using AutoMapper;
using dotnet_case.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.API.Profiles
{
    public class AlbumsProfile : Profile
    {
        public AlbumsProfile()
        {
            CreateMap<AlbumModel, Dtos.AlbumDto>()
                .ForMember(
                    destinationMember => destinationMember.ArtistName,
                    memberOption => memberOption.MapFrom(src => src.Artist.Name));
        }
    }
}

using AutoMapper;
using dotnet_case.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_case.API.Profiles
{
    public class TracksProfile : Profile
    {
        public TracksProfile()
        {
            CreateMap<TrackModel, Dtos.TrackDto>()
                .ForMember(
                    destinationMember => destinationMember.AlbumName,
                    memberOption => memberOption.MapFrom(src => src.Album.Name))
                .ForMember(
                    destinationMember => destinationMember.ArtistName,
                    memberOption => memberOption.MapFrom(src => src.Album.Artist.Name));

            // TODO TrackCreationDto back to TrackModel
        }
    }
}

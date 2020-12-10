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
                //.ForMember(
                //    destinationMember => destinationMember.AlbumName,
                //    memberOption => memberOption.MapFrom(src => src.Album.Name))
                //.ForMember(
                //    destinationMember => destinationMember.ArtistName,
                //    memberOption => memberOption.MapFrom(src => src.Album.Artist.Name))
                .ForMember(
                    destinationMember => destinationMember.Minutes,
                    memberOptions => memberOptions.MapFrom(src => src.Duration / 60000))
                .ForMember(
                    destinationMember => destinationMember.Seconds,
                    memberOptions => memberOptions.MapFrom(src => (src.Duration % 60000) / 1000));

            CreateMap<Dtos.TrackDto, TrackModel>()
                .ForMember(
                destinationMember => destinationMember.Duration,
                memberOptions => memberOptions.MapFrom(
                    src => (src.Minutes * 60000) + (src.Seconds * 1000)));

            // TODO TrackCreationDto back to TrackModel
        }
    }
}

﻿using dotnet_case.DATA.Repositories;
using dotnet_case.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_case.BL.Services
{
    public class TrackService : ITrackService
    {
        private readonly ICaseRepository _repo;

        public TrackService(ICaseRepository repo)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
        }

        public List<TrackModel> FindTracksByAlbumId(long albumId)
        {
            return _repo.FindTracksByAlbumId(albumId);
        }

        public TrackModel GetTrack(long trackId)
        {
            return _repo.GetTrack(trackId);
        }

        public void CreateTrack(TrackModel track)
        {
            // method is also void in tutorial
            _repo.CreateTrack(track);
        }

        public void UpdateTrack(TrackModel track)
        {
            _repo.UpdateTrack(track);
        }

        public void DeleteTrack(TrackModel track)
        {
            _repo.DeleteTrack(track);
        }

        public bool Save()
        {
            return _repo.Save();
        }
    }
}

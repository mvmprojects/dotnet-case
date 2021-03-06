﻿using dotnet_case.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_case.BL.Services
{
    public interface ITrackService
    {
        public List<TrackModel> FindTracksByAlbumId(long albumId);
        public TrackModel GetTrack(long trackId);
        public void CreateTrack(TrackModel track);
        public void UpdateTrack(TrackModel track);
        public void DeleteTrack(TrackModel track);
        public bool Save();
    }
}

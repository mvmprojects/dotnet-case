using dotnet_case.DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_case.DATA.Repositories
{
    public class CaseRepository : ICaseRepository, IDisposable
    {
        private readonly CaseContext _context;

        public CaseRepository(CaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool ArtistExists(long artistId)
        {
            return _context.Artists.Any(a => a.ArtistId == artistId);
        }

        public void DeleteArtist(ArtistModel artist)
        {
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            _context.Artists.Remove(artist);
        }

        public ArtistModel GetArtist(long artistId)
        {
            return _context.Artists.FirstOrDefault(a => a.ArtistId == artistId);
        }

        public ArtistModel FindArtistByName(string artistName)
        {
            return _context.Artists
                .Where(a => a.Name == artistName)
                .FirstOrDefault();
        }

        public List<ArtistModel> GetArtists()
        {
            // OrderBy should probably be moved to a service in the BL layer.
            // even better: OrderBy usage should be decided by the client and so
            // should be passed as an optional query string in the GET request.
            // example: api/artists?orderby=name
            return _context.Artists.OrderBy(c => c.Name).ToList();
        }

        // interesting to see if all controllers can be made async in the future
        public async Task<List<ArtistModel>> GetArtistsAsync()
        {
            return await _context.Artists.OrderBy(c => c.Name).ToListAsync();
        }

        public void UpdateArtist(ArtistModel artist)
        {
            // "no code in this implementation"
            // Kevin Dockx has an explanation for why this repo method is left empty in
            // his pluralsight tutorial, even though we still call it from a controller.
            // It has to do with enforcing the repository pattern:

            // "We're working on a contract, not on an implementation. Always have a set of
            // methods matching the required functionality and call them, even if they don't
            // do anything in the current implementation. (...) Always have this update 
            // method on your repository contract if an update is allowed. (...)

            // In EF Core, these entities are tracked by the context. By executing a
            // _mapper.Map statement (in the controller), the entity has changed to a 
            // modified state and executing Save() will write these changes to our database.
            // So all we have to do is call into the Save() method (on the repo). (...)
            
            // If changes happen to the underlying datastore, or to the repository, this is
            // the type of code (referring to the _repo.UpdateThing statement in the
            // controller) that will ensure it will keep on working as expected."
        }

        // Albums

        public List<AlbumModel> FindAlbumsByArtistId(long artistId)
        {
            return _context.Albums.Where(a => a.ArtistId == artistId).ToList();
        }

        // Tracks

        public List<TrackModel> FindTracksByAlbumId(long albumId)
        {
            return _context.Tracks.Where(t => t.AlbumId == albumId).ToList();
        }

        // method is also void in tutorial
        public void CreateTrack(TrackModel track)
        {
            _context.Tracks.Add(track);
        }

        public void UpdateTrack(TrackModel track)
        {
            _context.Tracks.Update(track);
        }

        public void DeleteTrack(TrackModel track)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            _context.Tracks.Remove(track);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources if needed
            }
        }
    }
}

using dotnet_case.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void UpdateArtist(ArtistModel artist)
        {
            // no code in this implementation
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

using dotnet_case.BL.Models;
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

        public ArtistModel FindByName(string artistName)
        {
            return _context.Artists
                .Where(a => a.Name == artistName)
                .FirstOrDefault();
        }

        public List<ArtistModel> GetArtists()
        {
            return _context.Artists.OrderBy(c => c.Name).ToList();
        }

        public async Task<List<ArtistModel>> GetArtistsAsync()
        {
            return await _context.Artists.OrderBy(c => c.Name).ToListAsync();
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

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DataAccess.Data;
using UrlShortener.Domain;

namespace UrlShortener.DataAccess.Repository
{
    public class EFShortenedUrlRepository : IShortenedUrlRepository
    {
        private readonly UrlShortenerDBContext _context;

        public EFShortenedUrlRepository(UrlShortenerDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ShortenedUrl entity)
        {
            await _context.Urls.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Urls.FindAsync(id);
            _context.Urls.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShortenedUrl>> GetAllAsync()
        {
            return await _context.Urls.ToListAsync();

        }

        public async Task<ShortenedUrl> GetAsync(int id)
        {
            return await _context.Urls.FindAsync(id);
        }

        public async Task<ShortenedUrl> GetByHashAsync(string hash)
        {

            return await _context.Urls.FirstOrDefaultAsync(x => x.Hash == hash);

        }

        public async Task UpdateAsync(ShortenedUrl entity)
        {
            _context.Urls.Update(entity);
            await _context.SaveChangesAsync();

        }


    }
}

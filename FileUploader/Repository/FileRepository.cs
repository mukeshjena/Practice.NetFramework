using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FileModel>> GetAllFiles()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task<FileModel> GetFileById(int id)
        {
            return await _context.Files.FindAsync(id);
        }

        public async Task AddFile(FileModel file)
        {
            _context.Files.Add(file);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFile(FileModel file)
        {
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }
    }
}

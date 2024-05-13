using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Repository
{
    public interface IFileRepository
    {
        Task<IEnumerable<FileModel>> GetAllFiles();
        Task<FileModel> GetFileById(int id);
        Task AddFile(FileModel file);
        Task DeleteFile(FileModel file);
    }
}

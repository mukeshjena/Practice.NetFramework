using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public interface IFileService
    {
        Task<IEnumerable<FileModel>> GetAllFiles();
        Task<FileModel> GetFileById(int id);
        Task UploadFile(FileModel file);
        Task DeleteFile(int id);
    }
}

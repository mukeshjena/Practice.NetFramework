using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Repository;

namespace BLL
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<IEnumerable<FileModel>> GetAllFiles()
        {
            return await _fileRepository.GetAllFiles();
        }

        public async Task<FileModel> GetFileById(int id)
        {
            return await _fileRepository.GetFileById(id);
        }

        public async Task UploadFile(FileModel file)
        {
            await _fileRepository.AddFile(file);
        }

        public async Task DeleteFile(int id)
        {
            var file = await _fileRepository.GetFileById(id);
            if (file != null)
            {
                _fileRepository.DeleteFile(file);
            }
        }
    }
}

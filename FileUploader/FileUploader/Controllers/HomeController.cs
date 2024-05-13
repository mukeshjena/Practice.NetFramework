using System.Diagnostics;
using BLL;
using DAL;
using FileUploader.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var files = await _fileService.GetAllFiles();
            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                await _fileService.UploadFile(new FileModel { FileName = fileName, FilePath = filePath });
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(int id)
        {
            var file = await _fileService.GetFileById(id);
            if (file == null)
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(file.FilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(file.FilePath));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var file = await _fileService.GetFileById(id);
            if (file == null)
                return NotFound();

            // Delete the file from the server
            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }

            // Delete the file entry from the database
            await _fileService.DeleteFile(id);

            return RedirectToAction("Index");
        }
    }
}

using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Services.FileHandling
{
    public class FileHandlingService : IFileHandlingService
    {
        
        public FileHandlingService() {
        
        }
        public dynamic SaveFile(IFormFile file, string folder, string path, string[] allowedExtensions)
        {
            string fileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(fileName);
            if (!allowedExtensions.Contains(fileExtension.ToLower()))
            {
                return null;
            }

            string relativeFilePath = Path.Combine(folder, fileName);
            string filePath = Path.Combine(path, relativeFilePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return new
            {
                fileName = fileName,
                filePath = filePath,
            };
        }
        public dynamic DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return new { message = "File deleted successfully" };
            }

            return null;
        }
        public string ConvertFilePathForImage(string filePath)
        {

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                byte[] imageBytes = File.ReadAllBytes(filePath);
                string base64Image = Convert.ToBase64String(imageBytes);
                return base64Image;
            }
            return filePath;
        }
    }
}

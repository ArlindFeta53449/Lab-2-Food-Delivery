using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.FileHandling
{
    public interface IFileHandlingService
    {
        dynamic SaveFile(IFormFile file, string folder, string path, string[] allowedExtensions);
        string ConvertFilePathForImage(string filePath);
        dynamic DeleteFile(string filePath);
    }
}

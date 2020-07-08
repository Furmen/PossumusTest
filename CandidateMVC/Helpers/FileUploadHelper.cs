using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateMVC.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string> CopyAndCreateFileAsync(IFormFile resumeFile)
        {
            var directory = Directory.GetCurrentDirectory() + "\\Upload\\" + DateTime.Now.ToString("HHmmssss");
            var filePath = Path.Combine(directory, resumeFile.FileName);

            CreateDirectoryIfNotExists(directory);

            using var stream = new FileStream(filePath, FileMode.Create);
                await resumeFile.CopyToAsync(stream);

            return filePath;
        }

        private static void CreateDirectoryIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public static bool CheckFileExtension(IFormFile resumeFile, string validExtensions)
        {
            return validExtensions.Split(",").Any(x => resumeFile.FileName.Contains(x));
        }
    }
}

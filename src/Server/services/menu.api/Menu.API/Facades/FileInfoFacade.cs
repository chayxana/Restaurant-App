using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Menu.API.Abstraction.Facades;
using Microsoft.AspNetCore.Hosting;

namespace Menu.API.Facades
{
    [ExcludeFromCodeCoverage]
    public class FileInfoFacade : IFileInfoFacade
    {
        private readonly IHostingEnvironment _appEnvironment;

        public FileInfoFacade(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        public Stream GetFileStream(string filePath, FileMode mode)
        {
            return new FileStream(filePath, mode);
        }

        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public string GetFilePathWithWebRoot(string folderPath, string fileName)
        {
            var folder = _appEnvironment.WebRootPath + folderPath;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder + fileName;
        }

        public string GetUniqName()
        {
            return $"{DateTime.Now:dd_mm_yyyy_H_mm_ss}_{Guid.NewGuid()}";
        }
    }
}
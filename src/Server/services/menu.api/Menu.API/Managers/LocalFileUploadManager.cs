using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Menu.API.Abstraction.Facades;
using Menu.API.Abstraction.Managers;
using Microsoft.AspNetCore.Http;

namespace Menu.API.Managers
{
    public class LocalFileUploadManager : IFileUploadManager
    {
        private readonly IFileInfoFacade _fileInfoFacade;

        public LocalFileUploadManager(IFileInfoFacade fileInfoFacade)
        {
            _fileInfoFacade = fileInfoFacade;
        }

        public async Task<string> Upload(IFormFile file)
        {
            var uploadedFileName = $"{_fileInfoFacade.GetUniqName()}_{file.FileName}";
            var filePath = _fileInfoFacade.GetFilePathWithWebRoot(Folders.UploadFilesPath, uploadedFileName);
            using (var fileStream = _fileInfoFacade.GetFileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
			return uploadedFileName;
        }

        public void Remove(string fileName)
        {
            if (_fileInfoFacade.Exists(fileName))
                _fileInfoFacade.Delete(fileName);
        }

        public bool HasFile(string fileName)
        {
            return _fileInfoFacade.Exists(fileName);
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Menu.API.Abstraction.Facades;
using Menu.API.Abstraction.Managers;
using Microsoft.AspNetCore.Http;

namespace Menu.API.Managers
{
	public class FileUploadManager : IFileUploadManager
	{
		private readonly IFileInfoFacade _fileInfoFacade;
		private readonly IDictionary<string, string> _uploadedFiles;

		public FileUploadManager(IFileInfoFacade fileInfoFacade)
		{
			_fileInfoFacade = fileInfoFacade;
			_uploadedFiles = new Dictionary<string, string>();
		}


		public async Task Upload(IFormFile file, string uniqId)
		{
			var uploadedFileName = $"{_fileInfoFacade.GetUniqName()}{Path.GetExtension(file.FileName)}";
			var filePath = _fileInfoFacade.GetFilePathWithWeebRoot(uploadedFileName);
			using (var fileStream = _fileInfoFacade.GetFileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
				_uploadedFiles.Add(uniqId, uploadedFileName);
			}
		}

		public void Remove(string fileName)
		{
			if (_fileInfoFacade.Exists(fileName))
				_fileInfoFacade.Delete(fileName);
		}

		public void Reset()
		{
			_uploadedFiles.Clear();
		}

		public string GetUploadedFileByUniqId(string uniqId)
		{
			return _uploadedFiles[uniqId];
		}

		public void RemoveUploadedFileByUniqId(string uniqId)
		{
			var fileName = GetUploadedFileByUniqId(uniqId);
			Remove(fileName);
			_uploadedFiles.Remove(uniqId);
		}

		public bool HasFile(string uniqId)
		{
			return _uploadedFiles.ContainsKey(uniqId);
		}
	}
}
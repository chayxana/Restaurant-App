using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Restaurant.Server.Api.Abstractions.Providers;
using Restaurant.Server.Api.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Restaurant.Server.Api.Providers
{
	public class FileUploadProvider : IFileUploadProvider
	{
		private readonly IHostingEnvironment _appEnvironment;
		private readonly IDictionary<string, string> _uploadedFiles;

		public FileUploadProvider(IHostingEnvironment appEnvironment)
		{
			_appEnvironment = appEnvironment;
			_uploadedFiles = new Dictionary<string, string>();
		}


		public async Task Upload(IFormFile file, string uniqId)
		{
			var uploadedFileName = $"{DateTime.Now:dd_mm_yyyy_H_mm_ss}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			using (var fileStream = new FileStream(GetFullPath(uploadedFileName), FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
				_uploadedFiles.Add(uniqId, uploadedFileName);
			}
		}

		public void Remove(string fileName)
		{
			FileInfo fileInfo = new FileInfo(GetFullPath(fileName));
			if (fileInfo.Exists)
				fileInfo.Delete();
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

		private string GetFullPath(string fileName)
		{
			var filePath = Folders.UploadFilesPath + fileName;
			return _appEnvironment.WebRootPath + filePath;
		}

	}
}

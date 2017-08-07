using Restaurant.Server.Api.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Restaurant.Server.Api.Providers
{
	public class FileUploadProvider : IFileUploadProvider
	{
		private readonly IHostingEnvironment _appEnvironment;

		public FileUploadProvider(IHostingEnvironment appEnvironment)
		{
			_appEnvironment = appEnvironment;
		}

		public string UploadedFileName { get; set; }

		public async Task Upload(IFormFile file)
		{
			UploadedFileName = $"{DateTime.Now.ToString("dd_mm_yyyy_H_mm_ss")}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
			var filePath = Folders.UploadFilesPath + UploadedFileName;
			using (var fileStream = new FileStream(_appEnvironment.WebRootPath + filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}
		}
	}
}

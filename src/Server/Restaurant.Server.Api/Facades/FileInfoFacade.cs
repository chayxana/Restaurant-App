using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Restaurant.Server.Api.Abstraction.Facades;
using Restaurant.Server.Api.Constants;

namespace Restaurant.Server.Api.Facades
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

		public string GetFilePathWithWeebRoot(string fileName)
		{
			return _appEnvironment.WebRootPath + Folders.UploadFilesPath + fileName;
		}

		public string GetUniqName()
		{
			return $"{DateTime.Now:dd_mm_yyyy_H_mm_ss}_{Guid.NewGuid()}";
		}
	}
}

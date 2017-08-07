using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Restaurant.Server.Api.Providers
{
	public interface IFileUploadProvider
	{
		string UploadedFileName { get; set; }
		Task Upload(IFormFile File);
	}
}
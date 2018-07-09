using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Restaurant.Server.Api.Abstraction.Providers
{
	public interface IFileUploadProvider
	{
		Task Upload(IFormFile file, string uniqId);

		void Remove(string fileName);

		void Reset();

		string GetUploadedFileByUniqId(string uniqId);

		void RemoveUploadedFileByUniqId(string uniqId);

		bool HasFile(string uniqId);
	}
}
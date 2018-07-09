using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Identity.API.Abstraction
{
	public interface IAccountFileUploadManager
	{
		Task Upload(IFormFile file, string uniqId);

		void Remove(string fileName);

		void Reset();

		string GetUploadedFileByUniqId(string uniqId);

		void RemoveUploadedFileByUniqId(string uniqId);

		bool HasFile(string uniqId);
	}
}
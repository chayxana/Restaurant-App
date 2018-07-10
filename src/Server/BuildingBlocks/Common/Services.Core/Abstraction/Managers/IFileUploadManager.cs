using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.Core.Abstraction.Managers
{
	public interface IFileUploadManager
	{
		Task Upload(IFormFile file, string uniqId);

		void Remove(string fileName);

		void Reset();

		string GetUploadedFileByUniqId(string uniqId);

		void RemoveUploadedFileByUniqId(string uniqId);

		bool HasFile(string uniqId);
	}
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Menu.API.Abstraction.Managers
{
	public interface IFileUploadManager
	{
		Task<string> Upload(IFormFile file);

		void Remove(string fileName);

		bool HasFile(string fileName);
	}
}
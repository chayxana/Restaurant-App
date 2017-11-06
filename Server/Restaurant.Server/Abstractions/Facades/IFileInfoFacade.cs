using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Server.Api.Abstractions.Facades
{
    public interface IFileInfoFacade
    {
	    Stream GetFileStream(string filePath, FileMode mode);

		bool Exists(string filePath);

		void Delete(string path);

	    string GetFilePathWithWeebRoot(string fileName);

	    string GetUniqName();
    }
}

using System.IO;

namespace Services.Core.Abstraction.Facades
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

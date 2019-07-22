using System.IO;

namespace Menu.API.Abstraction.Facades
{
    public interface IFileInfoFacade
    {
	    Stream GetFileStream(string filePath, FileMode mode);

		bool Exists(string filePath);

		void Delete(string path);

	    string GetFilePathWithWebRoot(string folderPath, string fileName);

	    string GetUniqName();
    }
}

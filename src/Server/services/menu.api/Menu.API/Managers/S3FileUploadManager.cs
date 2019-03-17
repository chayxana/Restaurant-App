using System.Threading.Tasks;
using Menu.API.Abstraction.Managers;
using Microsoft.AspNetCore.Http;

namespace Menu.API.Managers
{
    public class S3FileUploadManager : IFileUploadManager
    {
        public string GetUploadedFileByUniqId(string uniqId)
        {
            throw new System.NotImplementedException();
        }

        public bool HasFile(string uniqId)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveUploadedFileByUniqId(string uniqId)
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public Task Upload(IFormFile file, string uniqId)
        {
            throw new System.NotImplementedException();
        }
    }
}
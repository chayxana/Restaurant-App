using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using AutoMapper.Configuration;
using Menu.API.Abstraction.Managers;
using Microsoft.AspNetCore.Http;

namespace Menu.API.Managers
{
    public class S3FileUploadManager : IFileUploadManager
    {
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;
        
        public S3FileUploadManager(IConfiguration configuration, IAmazonS3 s3Client)
        {
            _configuration = configuration;
            _s3Client = s3Client;
        }

        public bool HasFile(string uniqId)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string fileName)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<string> Upload(IFormFile file)
        {
            throw new System.NotImplementedException();
        }
    }
}
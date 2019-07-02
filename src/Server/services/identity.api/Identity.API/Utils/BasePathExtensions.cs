using Microsoft.Extensions.Configuration;

namespace Identity.API.Utils
{
    public static class BasePathExtensions
    {
        public static (bool hasBasePath, string path) BasePath(this IConfiguration configuration) 
        {
            var pathBase = configuration["PATH_BASE"];
            if(!string.IsNullOrEmpty(pathBase)) 
            {
                return (true, pathBase);
            }

            return (false, string.Empty);
        }
    }
}
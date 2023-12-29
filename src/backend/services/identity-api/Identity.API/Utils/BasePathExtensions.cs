using Microsoft.Extensions.Configuration;

namespace Identity.API.Utils
{
    public static class BasePathExtensions
    {
        public static string GetBasePath(this IConfiguration configuration)
        {
            var pathBase = configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                return pathBase;
            }

            return string.Empty;
        }
    }
}
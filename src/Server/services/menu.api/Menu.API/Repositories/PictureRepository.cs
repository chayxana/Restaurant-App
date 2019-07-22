using Menu.API.Data;
using Menu.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Menu.API.Repositories
{
    public class PictureRepository : RepositoryBase<FoodPicture>
    {
        public PictureRepository(ApplicationDbContext context, ILogger<PictureRepository> logger) : base(context, logger)
        {
        }
    }
}
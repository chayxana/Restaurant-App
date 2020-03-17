using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Menu.API.Data;
using Menu.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Menu.API.Repositories
{
    [ExcludeFromCodeCoverage]
    public class FoodRepository : RepositoryBase<Food>
    {
        private readonly ApplicationDbContext context;
        public FoodRepository(ApplicationDbContext context, ILogger<FoodRepository> logger)
            : base(context, logger)
        {
            this.context = context;
        }

        public override IQueryable<Food> GetAll()
        {
            return base.GetAll()
                .Include(x => x.Category)
                .Include(x => x.Pictures);
        }

        public override Food Get(Guid id)
        {
            var food = base.Get(id);
			context.Entry(food).Reference(x => x.Category).Load();
			context.Entry(food).Collection(x => x.Pictures).Load();

			return food;
        }
    }
}
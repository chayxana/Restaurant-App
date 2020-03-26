using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Menu.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace Menu.API.Data
{
    public class ApplicationDbContextSeed
    {
        public void SeedAsync(ApplicationDbContext context,
            IWebHostEnvironment env,
            ILogger<ApplicationDbContext> logger)
        {
            var contentRootPath = env.ContentRootPath;
            
            var retry = Policy.Handle<SqlException>()
                .WaitAndRetry(new TimeSpan[]
                {
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(15),
                });
            retry.Execute(() =>
            {
                if (!context.Set<Category>().Any())
                {
                    logger.LogInformation("Categories are empty!");
                    var categoryList = GetCategories(contentRootPath);
                    var categories = categoryList.Select(c => new Category()
                    {
                        Id = Guid.NewGuid(),
                        Color = "White",
                        Name = c
                    });
                    context.AddRange(categories);
                    context.SaveChanges();
                    logger.LogInformation("Seed categories are created!");
                }
                if (!context.Set<Food>().Any())
                {
                    logger.LogInformation("Foods are empty!");
                    var categories = context.Set<Category>();
                    var foods = GetFoods(contentRootPath, categories);
                    context.AddRange(foods);
                    context.SaveChanges();
                    logger.LogInformation("Seed foods are created!");
                }
            });
        }

        private IEnumerable<string> GetCategories(string contentRootPath)
        {
            var categoriesFile = Path.Combine(contentRootPath, "Setup", "categories.csv");
            return File.Exists(categoriesFile)
                ? File.ReadAllText(categoriesFile).Split(',')
                : Enumerable.Empty<string>();
        }

        private IEnumerable<Food> GetFoods(string contentRootPath, IQueryable<Category> categories)
        {
            var categoriesFile = Path.Combine(contentRootPath, "Setup", "dishes.csv");
            if (File.Exists(categoriesFile))
            {
                return File.ReadAllLines(categoriesFile)
                    .Skip(1)
                    .Select(x => x.Split(','))
                    .Select(row => getFoodFromColumns(contentRootPath, row, categories))
                    .ToList();
            }
            return Enumerable.Empty<Food>();
        }

        private Food getFoodFromColumns(string contentRoot, string[] columns, IQueryable<Category> categories)
        {
            var food = new Food 
            {
                Name = columns[2].Trim(),
                Currency = "USD",
                Pictures = new List<FoodPicture>
                {
                    new FoodPicture { FileName = $"{columns[0].Trim()}.jpg" }
                },
            };
            
            var categoryColumn = columns[1].Trim();
            var category = categories.FirstOrDefault(c => c.Name == categoryColumn);
            if (category != null)
            {
                food.CategoryId = category.Id;
            }

            var descriptionColumn = columns[3].Trim();
            food.Description = descriptionColumn.Replace(':', ',');

            var priceColumn = columns[4].Trim();
            food.Price = Convert.ToDecimal(priceColumn);
            return food;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Restaurant.Server.Models;

namespace Restaurant.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Food")]
    public class FoodsController : BaseApiContoller
    {
        [Route("GetFoods")]
        public IEnumerable<Food> GetFoods()
        {
            var foods = Context.Foods;
            return foods;
        }
        [Route("AddFood")]
        public IHttpActionResult AddFood(Food food)
        {
            try
            {
                if (Context.Foods.Add(food))
                {
                    Context.SaveChanges();
                    return Ok(new ResultResponce { IsSucceeded = true });
                }
            }
            catch (Exception)
            {
                Context.GetValidationErrors();
            }

            return InternalServerError();
        }

        [Route("DeleteFood")]
        public IHttpActionResult DeleteFood(Food food)
        {
            try
            {
                bool result = Context.Foods.Remove(food);
                if (result)
                {
                    Context.SaveChanges();
                    return Ok(new ResultResponce { IsSucceeded = true });
                }
            }
            catch (Exception)
            {
                Context.GetValidationErrors();
            }

            return InternalServerError();
        }

        [Route("UpdateFood")]
        public IHttpActionResult UpdateFood(Food food)
        {
            try
            {
                var result = GetFoods().SingleOrDefault(f => f.Id == food.Id);
                if (result != null)
                {
                    result.Name = food.Name;
                    result.Orders = food.Orders;
                    result.Price = food.Price;
                    result.Type = food.Type;
                    Context.SaveChanges();
                    return Ok(new ResultResponce { IsSucceeded = true });
                }
            }
            catch (Exception)
            {
                Context.GetValidationErrors();
            }

            return InternalServerError();
        }
    }
    internal class ResultResponce : IHttpActionResult
    {
        public bool IsSucceeded { get; set; }

        public Dictionary<string, Exception> Errors { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

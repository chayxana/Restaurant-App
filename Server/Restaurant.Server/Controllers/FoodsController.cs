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
        /// <summary>
        /// Adding a new food
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        [Route("Add")]
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

        /// <summary>
        /// Deleting existed food
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        [Route("Delete")]
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

        /// <summary>
        /// Update existed food
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        [Route("Update")]
        public IHttpActionResult UpdateFood(Food food)
        {
            try
            {
                if (food != null)
                {
                    Context.Entry(food).State = System.Data.Entity.EntityState.Modified;
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

}

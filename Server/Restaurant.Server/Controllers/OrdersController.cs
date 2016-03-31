using System;
using System.Linq;
using System.Web.Http;
using Restaurant.Server.Models;

namespace Restaurant.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Order")]
    public class OrdersController : BaseApiContoller
    {
        /// <summary>
        /// Adding new order to current user
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [Route("Add")]
        public IHttpActionResult AddOrder(Order order)
        {
            try
            {
                if (CurrentUser != null && order != null)
                {
                    order.Id = Guid.NewGuid();
                    order.OrderedDate = DateTime.Now;
                    CurrentUser.Orders.Add(order);
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
        /// Deleting the order from current user
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [Route("Delete")]
        public IHttpActionResult DeleteOrder(Order order)
        {
            ResultResponce result = new ResultResponce() { IsSucceeded = false };
            try
            {
                if (CurrentUser != null)
                {
                    Order orderToDelete = CurrentUser.Orders.FirstOrDefault(o => o.Id == order.Id);
                    if (orderToDelete == null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        CurrentUser.Orders.Remove(orderToDelete);
                        Context.SaveChanges();
                        result.IsSucceeded = true;
                        return Ok(result);
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.Errors.Add("Error", ex);
                return Ok(result);
            }
        }

        /// <summary>
        /// Updating the order in current user
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [Route("Update")]
        public IHttpActionResult UpdateOrder(Order order)
        {
            try
            {
                if (order != null)
                {
                    Context.Entry(order).State = System.Data.Entity.EntityState.Modified;
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

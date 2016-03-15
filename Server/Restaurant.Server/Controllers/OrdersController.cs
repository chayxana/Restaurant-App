using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
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
                if (CurrentUser != null)
                {
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
            try
            {
                if (CurrentUser != null)
                {
                    CurrentUser.Orders.Remove(order);
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

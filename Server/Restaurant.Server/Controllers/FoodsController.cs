using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Restaurant.Server.Models;

namespace Restaurant.Server.Controllers
{
    public class FoodsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Foods
        public IQueryable<Food> GetFoods()
        {
            return db.Foods;
        }

        // GET: api/Foods/5
        [ResponseType(typeof(Food))]
        public async Task<IHttpActionResult> GetFood(Guid id)
        {
            Food food = await db.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }

        // PUT: api/Foods/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFood(Guid id, Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != food.Id)
            {
                return BadRequest();
            }

            db.Entry(food).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Foods
        [ResponseType(typeof(Food))]
        public async Task<IHttpActionResult> PostFood(Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Foods.Add(food);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FoodExists(food.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = food.Id }, food);
        }

        // DELETE: api/Foods/5
        [ResponseType(typeof(Food))]
        public async Task<IHttpActionResult> DeleteFood(Guid id)
        {
            Food food = await db.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            db.Foods.Remove(food);
            await db.SaveChangesAsync();

            return Ok(food);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodExists(Guid id)
        {
            return db.Foods.Count(e => e.Id == id) > 0;
        }
    }
}
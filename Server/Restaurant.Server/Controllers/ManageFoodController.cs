using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Server.Models;

namespace Restaurant.Server.Controllers
{
    public class ManageFoodController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManageFood
        public async Task<ActionResult> Index()
        {
            return View(await db.Foods.ToListAsync());
        }

        // GET: ManageFood/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = await db.Foods.FindAsync(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: ManageFood/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageFood/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Type,Price")] Food food, HttpPostedFileBase file)
        {
            if (file == null)
            {
                var modelState = new ModelState();
                modelState.Errors.Add("The file is required! Please choose some file from your computer.");
                ModelState.Add("FileName", modelState);
            }
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Uploads/");
                string newFileName = Guid.NewGuid() + Path.GetExtension(file?.FileName);
                var url = HttpContext.ApplicationInstance.Request.Url;
                string host = $"{url.Scheme}://{url.Authority}";
                string filePath = Path.Combine(path, newFileName);
                file?.SaveAs(filePath);
                food.Id = Guid.NewGuid();
                food.Picture = Path.Combine(host + "/Uploads/", newFileName);
                db.Foods.Add(food);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: ManageFood/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = await db.Foods.FindAsync(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: ManageFood/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Type,Price")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: ManageFood/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = await db.Foods.FindAsync(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: ManageFood/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Food food = await db.Foods.FindAsync(id);
            db.Foods.Remove(food);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

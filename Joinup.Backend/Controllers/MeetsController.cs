using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Joinup.Common.Models.DatabaseModels;
using Joinup.Domain.Models;

namespace Joinup.Backend.Controllers
{
    public class MeetsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Meets
        public async Task<ActionResult> Index()
        {
            return View(await db.Meets.ToListAsync());
        }

        // GET: Meets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meet meet = await db.Meets.FindAsync(id);
            if (meet == null)
            {
                return HttpNotFound();
            }
            return View(meet);
        }

        // GET: Meets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MeetId,PlanId,UserId")] Meet meet)
        {
            if (ModelState.IsValid)
            {
                db.Meets.Add(meet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(meet);
        }

        // GET: Meets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meet meet = await db.Meets.FindAsync(id);
            if (meet == null)
            {
                return HttpNotFound();
            }
            return View(meet);
        }

        // POST: Meets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MeetId,PlanId,UserId")] Meet meet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(meet);
        }

        // GET: Meets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meet meet = await db.Meets.FindAsync(id);
            if (meet == null)
            {
                return HttpNotFound();
            }
            return View(meet);
        }

        // POST: Meets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Meet meet = await db.Meets.FindAsync(id);
            db.Meets.Remove(meet);
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

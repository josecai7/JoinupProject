using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Joinup.Backend.Models;
using Joinup.Common.Models;
using Joinup.Backend.Helpers;

namespace Joinup.Backend.Controllers
{
    public class PlansController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Plans
        public async Task<ActionResult> Index()
        {
            return View(await db.Plans.ToListAsync());
        }

        // GET: Plans/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = await db.Plans.FindAsync(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // GET: Plans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Plans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( PlanView planView)
        {
            if (ModelState.IsValid)
            {

                var pic = string.Empty;
                var folder = "~/Content/Plans";

                if ( planView.ImageFile != null )
                {
                    pic = FilesHelper.UploadPhoto( planView.ImageFile, folder );
                    pic = $"{folder}/{pic}";
                }

                var plan = planView.ToPlan(pic);

                db.Plans.Add(plan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View( planView );
        }

        // GET: Plans/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = await db.Plans.FindAsync(id);
            if (plan == null)
            {
                return HttpNotFound();
            }

            var view = ToView(plan);

            return View( view );
        }

        private object ToView(Plan plan)
        {
            return new PlanView
            {
                PlanId = plan.PlanId,
                UserId = plan.UserId,
                PlanType = plan.PlanType,
                Name = plan.Name,
                Description = plan.Description,
                ImagePath = plan.ImagePath,
                Latitude = plan.Latitude,
                Longitude = plan.Longitude,
                MaxParticipants = plan.MaxParticipants,
                PlanDate = plan.PlanDate,
                EndPlanDate = plan.EndPlanDate,
            };
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PlanView planView)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Plans";

                if ( planView.ImageFile != null )
                {
                    pic = FilesHelper.UploadPhoto( planView.ImageFile, folder );
                    pic = $"{folder}/{pic}";
                }

                var plan = planView.ToPlan( pic );

                db.Entry(plan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View( planView );
        }

        // GET: Plans/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = await db.Plans.FindAsync(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Plan plan = await db.Plans.FindAsync(id);
            db.Plans.Remove(plan);
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

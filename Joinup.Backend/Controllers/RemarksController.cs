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
using Joinup.Common.Models.DatabaseModels;

namespace Joinup.Backend.Controllers
{
    public class RemarksController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Remarks
        public async Task<ActionResult> Index()
        {
            return View(await db.Remarks.ToListAsync());
        }

        // GET: Remarks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Remark remark = await db.Remarks.FindAsync(id);
            if (remark == null)
            {
                return HttpNotFound();
            }
            return View(remark);
        }

        // GET: Remarks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Remarks/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RemarkId,UserId,UserDisplayName,UserDisplayImage,PlanId,PlanName,Score,CommentText,CommentDate")] Remark remark)
        {
            if (ModelState.IsValid)
            {
                db.Remarks.Add(remark);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(remark);
        }

        // GET: Remarks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Remark remark = await db.Remarks.FindAsync(id);
            if (remark == null)
            {
                return HttpNotFound();
            }
            return View(remark);
        }

        // POST: Remarks/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RemarkId,UserId,UserDisplayName,UserDisplayImage,PlanId,PlanName,Score,CommentText,CommentDate")] Remark remark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(remark).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(remark);
        }

        // GET: Remarks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Remark remark = await db.Remarks.FindAsync(id);
            if (remark == null)
            {
                return HttpNotFound();
            }
            return View(remark);
        }

        // POST: Remarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Remark remark = await db.Remarks.FindAsync(id);
            db.Remarks.Remove(remark);
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

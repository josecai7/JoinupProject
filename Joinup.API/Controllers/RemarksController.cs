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
using Joinup.Common.Models.DatabaseModels;
using Joinup.Domain.Models;

namespace Joinup.API.Controllers
{
    public class RemarksController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Remarks
        public IQueryable<Remark> GetRemarks()
        {
            return db.Remarks;
        }

        // GET: api/Remarks/5
        [ResponseType(typeof(Remark))]
        public async Task<IHttpActionResult> GetRemark(int id)
        {
            Remark remark = await db.Remarks.FindAsync(id);
            if (remark == null)
            {
                return NotFound();
            }

            return Ok(remark);
        }

        // PUT: api/Remarks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRemark(int id, Remark remark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != remark.RemarkId)
            {
                return BadRequest();
            }

            db.Entry(remark).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RemarkExists(id))
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

        // POST: api/Remarks
        [ResponseType(typeof(Remark))]
        public async Task<IHttpActionResult> PostRemark(Remark remark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Remarks.Add(remark);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = remark.RemarkId }, remark);
        }

        // DELETE: api/Remarks/5
        [ResponseType(typeof(Remark))]
        public async Task<IHttpActionResult> DeleteRemark(int id)
        {
            Remark remark = await db.Remarks.FindAsync(id);
            if (remark == null)
            {
                return NotFound();
            }

            db.Remarks.Remove(remark);
            await db.SaveChangesAsync();

            return Ok(remark);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RemarkExists(int id)
        {
            return db.Remarks.Count(e => e.RemarkId == id) > 0;
        }
    }
}
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
    public class MeetsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Meets
        public IQueryable<Meet> GetMeets()
        {
            return db.Meets;
        }
        // GET: api/Meets
        [Route( "~/api/Meets/ByPlan/{pPlanId}" )]
        public IQueryable<Meet> GetMeetsByPlan(int pPlanId)
        {
            return db.Meets
                .Where(item=>item.PlanId==pPlanId)
                .Include(meet=>meet.User);
        }

        // GET: api/Meets/5
        [ResponseType(typeof(Meet))]
        public async Task<IHttpActionResult> GetMeet(int id)
        {
            Meet meet = await db.Meets.FindAsync(id);
            if (meet == null)
            {
                return NotFound();
            }

            return Ok(meet);
        }

        // PUT: api/Meets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMeet(int id, Meet meet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meet.MeetId)
            {
                return BadRequest();
            }

            db.Entry(meet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetExists(id))
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

        // POST: api/Meets
        [ResponseType(typeof(Meet))]
        public async Task<IHttpActionResult> PostMeet(Meet meet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meets.Add(meet);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = meet.MeetId }, meet);
        }

        // DELETE: api/Meets/5
        [ResponseType(typeof(Meet))]
        public async Task<IHttpActionResult> DeleteMeet(Meet pMeet)
        {
            Meet meet = await db.Meets.Where(item => item.PlanId == pMeet.PlanId && item.UserId == pMeet.UserId).FirstOrDefaultAsync();
            if (meet == null)
            {
                return NotFound();
            }

            db.Meets.Remove(meet);
            await db.SaveChangesAsync();

            return Ok(meet);
        }

        // POST: api/Meets/5
        [Route("api/Meets/DeleteMeet")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteConfirmed(Meet pMeet)
        {
            Meet meet = await db.Meets.Where(item=>item.PlanId==pMeet.PlanId&& item.UserId == pMeet.UserId).FirstOrDefaultAsync();
            if (meet == null)
            {
                return NotFound();
            }

            db.Meets.Remove(meet);
            await db.SaveChangesAsync();

            return Ok(meet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetExists(int id)
        {
            return db.Meets.Count(e => e.MeetId == id) > 0;
        }
    }
}
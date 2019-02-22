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
    public class SportsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Sports
        public IQueryable<Sport> GetSports()
        {
            return db.Sports;
        }

        // GET: api/Sports/5
        [ResponseType(typeof(Sport))]
        public async Task<IHttpActionResult> GetSport(int id)
        {
            Sport sport = await db.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            return Ok(sport);
        }

        // PUT: api/Sports/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSport(int id, Sport sport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sport.SportId)
            {
                return BadRequest();
            }

            db.Entry(sport).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportExists(id))
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

        // POST: api/Sports
        [ResponseType(typeof(Sport))]
        public async Task<IHttpActionResult> PostSport(Sport sport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sports.Add(sport);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sport.SportId }, sport);
        }

        // DELETE: api/Sports/5
        [ResponseType(typeof(Sport))]
        public async Task<IHttpActionResult> DeleteSport(int id)
        {
            Sport sport = await db.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            db.Sports.Remove(sport);
            await db.SaveChangesAsync();

            return Ok(sport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SportExists(int id)
        {
            return db.Sports.Count(e => e.SportId == id) > 0;
        }
    }
}
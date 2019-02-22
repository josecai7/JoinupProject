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
    public class SkillLevelsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/SkillLevels
        public IQueryable<SkillLevel> GetSkillLevel()
        {
            return db.SkillLevel;
        }

        // GET: api/SkillLevels/5
        [ResponseType(typeof(SkillLevel))]
        public async Task<IHttpActionResult> GetSkillLevel(int id)
        {
            SkillLevel skillLevel = await db.SkillLevel.FindAsync(id);
            if (skillLevel == null)
            {
                return NotFound();
            }

            return Ok(skillLevel);
        }

        // PUT: api/SkillLevels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSkillLevel(int id, SkillLevel skillLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skillLevel.SkillLevelId)
            {
                return BadRequest();
            }

            db.Entry(skillLevel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillLevelExists(id))
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

        // POST: api/SkillLevels
        [ResponseType(typeof(SkillLevel))]
        public async Task<IHttpActionResult> PostSkillLevel(SkillLevel skillLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SkillLevel.Add(skillLevel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = skillLevel.SkillLevelId }, skillLevel);
        }

        // DELETE: api/SkillLevels/5
        [ResponseType(typeof(SkillLevel))]
        public async Task<IHttpActionResult> DeleteSkillLevel(int id)
        {
            SkillLevel skillLevel = await db.SkillLevel.FindAsync(id);
            if (skillLevel == null)
            {
                return NotFound();
            }

            db.SkillLevel.Remove(skillLevel);
            await db.SaveChangesAsync();

            return Ok(skillLevel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillLevelExists(int id)
        {
            return db.SkillLevel.Count(e => e.SkillLevelId == id) > 0;
        }
    }
}
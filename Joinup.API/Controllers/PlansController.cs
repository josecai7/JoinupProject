using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Joinup.API.Helpers;
using Joinup.Common.Models;
using Joinup.Common.Models.DatabaseModels;
using Joinup.Domain.Models;
using Newtonsoft.Json;

namespace Joinup.API.Controllers
{
    public class PlansController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Plans
        public IQueryable<Plan> GetPlans()
        {
            var plans = db.Plans
            .Include(plan => plan.Remarks)
            .Include(plan => plan.Images)
            .Include(plan => plan.Comments)
            .Include(plan => plan.Meets)
            .Include(plan => plan.User);

            return plans;
        }

        // GET: api/Plans
        [Route("~/api/Plans/ByUser/{pUserId}")]
        public IQueryable<Plan> GetPlansByUser(string pUserId)
        {
            var plans = db.Plans
                .Where(plan => plan.UserId == pUserId)
                .Include(plan => plan.Remarks)
                .Include(plan => plan.Images)
                .Include(plan => plan.Meets)
                .Include(plan => plan.Comments)
                .Include(plan => plan.User);

            return plans;
        }

        // GET: api/Plans/5
        [ResponseType(typeof(Plan))]
        public IHttpActionResult GetPlan(int id)
        {
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return NotFound();
            }

            return Ok(plan);
        }

        // PUT: api/Plans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlan(int id, Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plan.PlanId)
            {
                return BadRequest();
            }

            db.Entry(plan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
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

        // POST: api/Plans
        [ResponseType(typeof(Plan))]
        public IHttpActionResult PostPlan(Plan plan)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Plans.Add(plan);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PlanExists(plan.PlanId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = plan.PlanId }, plan);
        }

        // DELETE: api/Plans/5
        [ResponseType(typeof(Plan))]
        public IHttpActionResult DeletePlan(int id)
        {
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return NotFound();
            }

            db.Plans.Remove(plan);
            db.SaveChanges();

            return Ok(plan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlanExists(int id)
        {
            return db.Plans.Count(e => e.PlanId == id) > 0;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Joinup.Common.Models;
using Joinup.Domain.Models;

namespace Joinup.API.Controllers
{
    public class PlansController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Plans
        public IQueryable<Plan> GetPlans()
        {
            return db.Plans;
        }

        // GET: api/Plans/5
        [ResponseType(typeof(Plan))]
        public IHttpActionResult GetPlan(string id)
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
        public IHttpActionResult PutPlan(string id, Plan plan)
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
        public IHttpActionResult DeletePlan(string id)
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

        private bool PlanExists(string id)
        {
            return db.Plans.Count(e => e.PlanId == id) > 0;
        }
    }
}
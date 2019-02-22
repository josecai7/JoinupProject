using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Joinup.Common.Models;
using Joinup.Common.Models.DatabaseModels;
using Joinup.API.Helpers;
using Joinup.Domain.Models;

namespace Joinup.API.Controllers
{
    public class ImagesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Images
        public IQueryable<Image> GetImages()
        {
            return db.Images;
        }

        // GET: api/Images/5
        [ResponseType(typeof(Image))]
        public async Task<IHttpActionResult> GetImage(string id)
        {
            Image image = await db.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }

        // PUT: api/Images/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutImage(int id, Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != image.ImageId)
            {
                return BadRequest();
            }

            db.Entry(image).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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

        // POST: api/Images
        [ResponseType(typeof(Image))]
        public async Task<IHttpActionResult> PostImage(Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (image.ImageArray != null && image.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(image.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Images";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    image.ImagePath = fullPath;
                }
            }

            db.Images.Add(image);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImageExists(image.ImageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = image.ImageId }, image);
        }

        // DELETE: api/Images/5
        [ResponseType(typeof(Image))]
        public async Task<IHttpActionResult> DeleteImage(string id)
        {
            Image image = await db.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            db.Images.Remove(image);
            await db.SaveChangesAsync();

            return Ok(image);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImageExists(int id)
        {
            return db.Images.Count(e => e.ImageId == id) > 0;
        }
    }
}
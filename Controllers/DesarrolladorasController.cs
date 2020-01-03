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
using Videogames_backend.Models;

namespace Videogames_backend.Controllers
{
    public class DesarrolladorasController : ApiController
    {
        private VideogamesEntities db = new VideogamesEntities();

        // GET: api/Desarrolladoras
        public IQueryable<DesarrolladoraDTO> GetDesarrolladora()
        {
            var developer = from a in db.Desarrolladora
                            select new DesarrolladoraDTO()
                            {
                                Id = a.Id,
                                Nom = a.Nom,
                                País = a.País
                            };
            return developer;
        }

        // GET: api/Desarrolladoras/5
        [ResponseType(typeof(DesarrolladoraDTO))]
        public async Task<IHttpActionResult> GetDesarrolladora(int id)
        {
            var developer = await db.Desarrolladora.Select(a => new DesarrolladoraDTO()
            {
                Id = a.Id,
                Nom = a.Nom,
                País = a.País
            }).SingleOrDefaultAsync(a => a.Id == id);

            if (developer == null)
            {
                return NotFound();
            }

            return Ok(developer);
        }

        // PUT: api/Desarrolladoras/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDesarrolladora(int id, Desarrolladora desarrolladora)
        {
            var developer = await db.Desarrolladora.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (developer != null)
            {
                developer.Nom = desarrolladora.Nom;
                developer.País = desarrolladora.País;
                await db.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != desarrolladora.Id)
            //{
            //    return BadRequest();
            //}

            //db.Entry(desarrolladora).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!DesarrolladoraExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Desarrolladoras
        [ResponseType(typeof(DesarrolladoraDTO))]
        public async Task<IHttpActionResult> PostDesarrolladora(Desarrolladora desarrolladora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Desarrolladora.Add(desarrolladora);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = desarrolladora.Id }, desarrolladora);
        }

        // DELETE: api/Desarrolladoras/5
        [ResponseType(typeof(DesarrolladoraDTO))]
        public async Task<IHttpActionResult> DeleteDesarrolladora(int id)
        {
            Desarrolladora desarrolladora = await db.Desarrolladora.FindAsync(id);
            if (desarrolladora == null)
            {
                return NotFound();
            }

            db.Desarrolladora.Remove(desarrolladora);
            await db.SaveChangesAsync();

            return Ok(desarrolladora);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DesarrolladoraExists(int id)
        {
            return db.Desarrolladora.Count(e => e.Id == id) > 0;
        }
    }
}
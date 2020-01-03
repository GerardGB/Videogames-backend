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
    public class GamesController : ApiController
    {
        private VideogamesEntities db = new VideogamesEntities();

        // GET: api/Games
        public IQueryable<GamesDTO> GetGames()
        {
            var jocs = from a in db.Games
                       select new GamesDTO()
                       {
                           Id = a.Id,
                           Nom = a.Nom,
                           Gènere = a.Gènere,
                           Release = a.Release,

                           Desarrolladora = a.Desarrolladora == null ? null : new DesarrolladoraDTO()
                           {
                               Id = a.Desarrolladora.Id,
                               Nom = a.Desarrolladora.Nom,
                               País = a.Desarrolladora.País
                           },

                           PEGI = a.PEGI,
                           PS4 = a.PS4,
                           NSwitch = a.NSwitch,
                           XboxOne = a.XboxOne,
                           PC = a.PC
                       };
            return jocs;
        }

        // GET: api/Games/5
        [ResponseType(typeof(GamesDTO))]
        public async Task<IHttpActionResult> GetGames(int id)
        {
            var jocs = await db.Games.Select(a => new GamesDTO()
            {
                Id = a.Id,
                Nom = a.Nom,
                Gènere = a.Gènere,
                Release = a.Release,
                Desarrolladora = a.Desarrolladora == null ? null : new DesarrolladoraDTO()
                {
                    Id = a.Desarrolladora.Id,
                    Nom = a.Desarrolladora.Nom,
                    País = a.Desarrolladora.País
                },
                PEGI = a.PEGI,
                PS4 = a.PS4,
                NSwitch = a.NSwitch,
                XboxOne = a.XboxOne,
                PC = a.PC
            }).SingleOrDefaultAsync(a => a.Id == id);

            if (jocs == null)
            {
                return NotFound();
            }

            return Ok(jocs);
        }

        // PUT: api/Games/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGames(int id, Games games)
        {
            var jocs = await db.Games.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (jocs != null)
            {
                jocs.Nom = games.Nom;
                jocs.Gènere = games.Gènere;
                jocs.Release = games.Release;
                jocs.id_desarrolladora = games.id_desarrolladora;
                jocs.PEGI = games.PEGI;
                jocs.PS4 = games.PS4;
                jocs.NSwitch = games.NSwitch;
                jocs.XboxOne = games.XboxOne;
                jocs.PC = games.PC;
                await db.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != games.Id)
            //{
            //    return BadRequest();
            //}

            //db.Entry(games).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!GamesExists(id))
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

        // POST: api/Games
        [ResponseType(typeof(GamesDTO))]
        public async Task<IHttpActionResult> PostGames(Games games)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Games.Add(games);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                if (GamesExists(games.Id))
                {
                    return Conflict();
                }
                else {
                    throw;
                }

            }

            return CreatedAtRoute("DefaultApi", new { id = games.Id }, games);
        }

        // DELETE: api/Games/5
        [ResponseType(typeof(GamesDTO))]
        public async Task<IHttpActionResult> DeleteGames(int id)
        {
            Games games = await db.Games.FindAsync(id);
            if (games == null)
            {
                return NotFound();
            }

            db.Games.Remove(games);
            await db.SaveChangesAsync();

            return Ok(games);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GamesExists(int id)
        {
            return db.Games.Count(e => e.Id == id) > 0;
        }
    }
}
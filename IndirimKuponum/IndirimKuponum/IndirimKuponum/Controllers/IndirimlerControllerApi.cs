using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IndirimKuponum.Data;
using IndirimKuponum.Models;

namespace IndirimKuponum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndirimlerControllerApi : ControllerBase
    {
        private IndirimlerContext db = new IndirimlerContext();

        private readonly ApplicationDbContext _context;
        public IndirimlerControllerApi(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/IndirimlerControllerApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Indirimler>>> GetIndirimler()
        {
            return db.Indirim.ToList();
        }

        // GET: api/IndirimlerControllerApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Indirimler>> GetIndirimler(int id)
        {
            var indirimler = await db.Indirim.FindAsync(id);

            if (indirimler == null)
            {
                return NotFound();
            }

            return indirimler;
        }

        // PUT: api/IndirimlerControllerApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndirimler(int id, Indirimler indirimler)
        {
            if (id != indirimler.Id)
            {
                return BadRequest();
            }

            db.Entry(indirimler).State = (System.Data.Entity.EntityState)EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndirimlerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/IndirimlerControllerApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Indirimler>> PostIndirimler(Indirimler indirimler)
        {
            db.Indirim.Add(indirimler);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetIndirimler", new { id = indirimler.Id }, indirimler);
        }

        // DELETE: api/IndirimlerControllerApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndirimler(int id)
        {
            var indirimler = await db.Indirim.FindAsync(id);
            if (indirimler == null)
            {
                return NotFound();
            }

            db.Indirim.Remove(indirimler);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool IndirimlerExists(int id)
        {
            return db.Indirim.Any(e => e.Id == id);
        }
    }
}

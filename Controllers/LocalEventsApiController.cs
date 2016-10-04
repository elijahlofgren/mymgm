using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mymgm.Data;
using mymgm.Models;
using Microsoft.AspNetCore.Authorization;

namespace mymgm.Controllers
{
    [Produces("application/json")]
    [Route("api/LocalEventsApi")]
    public class LocalEventsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalEventsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LocalEventsApi
        [HttpGet]
        public IEnumerable<LocalEvent> GetLocalEvent()
        {
            // Show only upcoming events ordered by start date with earlier items showing first.
            return _context.LocalEvent.OrderBy(item => item.StartDate).Where(item => item.StartDate > DateTime.Now);
        }

        // GET: api/LocalEventsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocalEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LocalEvent localEvent = await _context.LocalEvent.SingleOrDefaultAsync(m => m.ID == id);

            if (localEvent == null)
            {
                return NotFound();
            }

            return Ok(localEvent);
        }

        // PUT: api/LocalEventsApi/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutLocalEvent([FromRoute] int id, [FromBody] LocalEvent localEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != localEvent.ID)
            {
                return BadRequest();
            }

            _context.Entry(localEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalEventExists(id))
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

        // POST: api/LocalEventsApi
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostLocalEvent([FromBody] LocalEvent localEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LocalEvent.Add(localEvent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LocalEventExists(localEvent.ID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLocalEvent", new { id = localEvent.ID }, localEvent);
        }

        // DELETE: api/LocalEventsApi/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLocalEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LocalEvent localEvent = await _context.LocalEvent.SingleOrDefaultAsync(m => m.ID == id);
            if (localEvent == null)
            {
                return NotFound();
            }

            _context.LocalEvent.Remove(localEvent);
            await _context.SaveChangesAsync();

            return Ok(localEvent);
        }

        private bool LocalEventExists(int id)
        {
            return _context.LocalEvent.Any(e => e.ID == id);
        }
    }
}
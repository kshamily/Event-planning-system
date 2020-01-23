using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EEPS.API.Contexts;
using EEPS.API.Entities;

namespace EEPS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDetailsController : ControllerBase
    {
        private readonly EEPSDBContext _context;

        public EventDetailsController()
        {
            _context = new EEPSDBContext();
        }

        // GET: api/EventDetails
        [HttpGet]
        public IEnumerable<EventDetail> GetEventDetails()
        {
            return _context.EventDetails.Include(x => x.Customer);
        }

        // GET: api/EventDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventDetail = await _context.EventDetails.FindAsync(id);

            if (eventDetail == null)
            {
                return NotFound();
            }

            return Ok(eventDetail);
        }

        // PUT: api/EventDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventDetail([FromRoute] int id, [FromBody] EventDetail eventDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventDetail.EventId)
            {
                return BadRequest();
            }

            _context.Entry(eventDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventDetailExists(id))
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

        // POST: api/EventDetails
        [HttpPost]
        public async Task<IActionResult> PostEventDetail([FromBody] EventDetail eventDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EventDetails.Add(eventDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventDetail", new { id = eventDetail.EventId }, eventDetail);
        }

        // DELETE: api/EventDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventDetail = await _context.EventDetails.FindAsync(id);
            if (eventDetail == null)
            {
                return NotFound();
            }

            _context.EventDetails.Remove(eventDetail);
            await _context.SaveChangesAsync();

            return Ok(eventDetail);
        }

        private bool EventDetailExists(int id)
        {
            return _context.EventDetails.Any(e => e.EventId == id);
        }
    }
}
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
    public class GuestDetailsController : ControllerBase
    {
        private readonly EEPSDBContext _context;

        public GuestDetailsController()
        {
            _context = new EEPSDBContext();
        }

        // GET: api/GuestDetails
        [HttpGet]
        public IEnumerable<GuestDetail> GetGuestDetails()
        {
            return _context.GuestDetails;
        }

        // GET: api/GuestDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestDetail = await _context.GuestDetails.FindAsync(id);

            if (guestDetail == null)
            {
                return NotFound();
            }

            return Ok(guestDetail);
        }

        // PUT: api/GuestDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuestDetail([FromRoute] int id, [FromBody] GuestDetail guestDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guestDetail.GuestId)
            {
                return BadRequest();
            }

            _context.Entry(guestDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestDetailExists(id))
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

        // POST: api/GuestDetails
        [HttpPost]
        public async Task<IActionResult> PostGuestDetail([FromBody] GuestDetail guestDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GuestDetails.Add(guestDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuestDetail", new { id = guestDetail.GuestId }, guestDetail);
        }

        // DELETE: api/GuestDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestDetail = await _context.GuestDetails.FindAsync(id);
            if (guestDetail == null)
            {
                return NotFound();
            }

            _context.GuestDetails.Remove(guestDetail);
            await _context.SaveChangesAsync();

            return Ok(guestDetail);
        }

        private bool GuestDetailExists(int id)
        {
            return _context.GuestDetails.Any(e => e.GuestId == id);
        }
    }
}
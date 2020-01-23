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
    public class EventDocumentController : ControllerBase
    {
        private readonly EEPSDBContext _context;

        public EventDocumentController()
        {
            _context = new EEPSDBContext();
        }

        [HttpPost]
        public async Task<IActionResult> PostEventDetail([FromForm] int eventid, [FromForm] string filepath)
        {

            var eventDetail = _context.EventDetails.Where(x => x.EventId == eventid).FirstOrDefault();
            eventDetail.FilePath = filepath;

            _context.Entry(eventDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        private bool EventDetailExists(int id)
        {
            return _context.EventDetails.Any(e => e.EventId == id);
        }
    }
}
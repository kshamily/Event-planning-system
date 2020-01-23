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
    public class AuthenticationController : ControllerBase
    {
        private readonly EEPSDBContext _context;

        public AuthenticationController()
        {
            _context = new EEPSDBContext();
        }

        // POST: api/FindUser
        [HttpPost]
        public async Task<IActionResult> PostFindUser([FromForm] string username, [FromForm] string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDetail = _context.UserDetails.Where(x => x.UserName == username && x.Password == password && x.IsActive).FirstOrDefault();

            if (userDetail == null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }


        // GET: api/UserDetails/5
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserDetail([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDetail = _context.UserDetails.Where(x => x.UserName == username).FirstOrDefault();

            if (userDetail == null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

    }
}
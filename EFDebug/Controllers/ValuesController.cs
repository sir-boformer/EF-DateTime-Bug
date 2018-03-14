using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDebug.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFDebug.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ValuesController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Membership>> GetAll()
        {
            return await _context.Memberships.ToListAsync();
        }

        [HttpGet("min")]
        public async Task<IEnumerable<Membership>> GetMin()
        {
            return await _context.Memberships.Where(m => m.StartDate == DateTime.MinValue).ToListAsync();
        }

        [HttpGet("max")]
        public async Task<IEnumerable<Membership>> GetMax()
        {
            return await _context.Memberships.Where(m => m.EndDate > DateTime.MaxValue.Date).ToListAsync();
        }
    }
}

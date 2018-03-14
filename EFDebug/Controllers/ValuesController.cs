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
[Controller]
public class ValuesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ValuesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Membership>> GetAll()
    {
        return await _context.Memberships.ToListAsync();
    }

    [HttpGet("max")]
    public async Task<IEnumerable<Membership>> GetMax()
    {
        // Error occurs here, result set is always empty!
        return await _context.Memberships.Where(m => m.EndDate == DateTime.MaxValue).ToListAsync();
    }

    [HttpGet("min")]
    public async Task<IEnumerable<Membership>> GetMin()
    {
        // no problems here
        return await _context.Memberships.Where(m => m.StartDate == DateTime.MinValue).ToListAsync();
    }
}
}

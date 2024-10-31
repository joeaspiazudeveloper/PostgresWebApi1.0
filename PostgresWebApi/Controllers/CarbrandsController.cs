using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresWebApi.Data;
using PostgresWebApi.Models;

namespace PostgresWebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class CarbrandsController: ControllerBase
{
    private readonly ILogger<CarbrandsController> _logger;
    private readonly ApiDbContext _context;

    public CarbrandsController(
        ILogger<CarbrandsController> logger,
        ApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var allCarbrands = await _context.Carbrands.ToListAsync();
        return Ok(allCarbrands);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var carbrand = await _context.Carbrands.FindAsync(id);
        if (carbrand == null)
        {
            return NotFound();
        }
        return Ok(carbrand);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Carbrand carbrand)
    {
        if (carbrand == null)
        {
            return BadRequest();
        }

        _context.Carbrands.Add(carbrand);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = carbrand.Id }, carbrand);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Carbrand carbrand)
    {
        if (id != carbrand.Id)
        {
            return BadRequest();
        }

        _context.Entry(carbrand).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CarbrandExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var carbrand = await _context.Carbrands.FindAsync(id);
        if (carbrand == null)
        {
            return NotFound();
        }

        _context.Carbrands.Remove(carbrand);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CarbrandExists(int id)
    {
        return _context.Carbrands.Any(e => e.Id == id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServet.Models;
using ClassLibrary;

namespace WebServet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class relationsController : ControllerBase
    {
        private readonly UserContext _context;

        public relationsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/relations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<relation>>> Getrelation()
        {
            return await _context.relation.Include(i => i.User_actor).Include(j => j.User_object).ToListAsync();
        }

        // GET: api/relations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<relation>>> Getrelation(int id)
        {
            var query =
                from re in _context.relation
                where re.actor_id == id
                select re;

            var relation = await query.ToListAsync();

            if (relation == null)
            {
                return NotFound();
            }

            int num = 0;
            foreach (relation obj in relation)
            {
                System.Diagnostics.Debug.WriteLine(obj.object_id);
                num++;
            }
            System.Diagnostics.Debug.WriteLine(num);

            return relation;
        }

        // PUT: api/relations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putrelation(int id, relation relation)
        {
            if (id != relation.relation_id)
            {
                return BadRequest();
            }

            _context.Entry(relation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!relationExists(id))
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

        // POST: api/relations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<relation>> Postrelation(relation relation)
        {
            _context.relation.Add(relation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getrelation", new { id = relation.relation_id }, relation);
        }

        // DELETE: api/relations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<relation>> Deleterelation(int id)
        {
            var relation = await _context.relation.FindAsync(id);
            if (relation == null)
            {
                return NotFound();
            }

            _context.relation.Remove(relation);
            await _context.SaveChangesAsync();

            return relation;
        }

        private bool relationExists(int id)
        {
            return _context.relation.Any(e => e.relation_id == id);
        }
    }
}

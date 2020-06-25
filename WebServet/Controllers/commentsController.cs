using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServet.Models;

namespace WebServet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class commentsController : ControllerBase
    {
        private readonly UserContext _context;

        public commentsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<comment>>> Getcomment()
        {
            return await _context.comment.ToListAsync();
        }

        // GET: api/comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<comment>> Getcomment(int id)
        {
            var comment = await _context.comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcomment(int id, comment comment)
        {
            if (id != comment.comment_id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!commentExists(id))
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

        // POST: api/comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<comment>> Postcomment(comment comment)
        {
            _context.comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcomment", new { id = comment.comment_id }, comment);
        }

        // DELETE: api/comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<comment>> Deletecomment(int id)
        {
            var comment = await _context.comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.comment.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        private bool commentExists(int id)
        {
            return _context.comment.Any(e => e.comment_id == id);
        }
    }
}

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
    public class postsController : ControllerBase
    {
        private readonly UserContext _context;

        public postsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<post>>> Getposts()
        {
            return await _context.posts.Include(i=>i.User).ToListAsync();

        }

        // GET: api/posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<post>> Getpost(int id)
        {
            //var post = await _context.posts.FindAsync(id);

            var post = await _context.posts
                        .Include(i => i.User)
                        .FirstOrDefaultAsync(i => i.post_id == id);

            if (post == null)
            {
                return NotFound();
            }

            System.Diagnostics.Debug.WriteLine(post.User.user_id);
            System.Diagnostics.Debug.WriteLine(post.User.user_name);

            return post;
        }

        // PUT: api/posts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpost(int id, post post)
        {
            if (id != post.post_id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!postExists(id))
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

        // POST: api/posts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<post>> Postpost(post post)
        {
            _context.posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getpost", new { id = post.post_id }, post);
        }

        // DELETE: api/posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<post>> Deletepost(int id)
        {
            var post = await _context.posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.posts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool postExists(int id)
        {
            return _context.posts.Any(e => e.post_id == id);
        }
    }
}

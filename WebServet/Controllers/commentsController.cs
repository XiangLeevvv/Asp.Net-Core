using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServet.Models;
using CommentAndLike;
using localLib;

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

        [HttpGet("queryComment/{id}")]
        public async Task<ActionResult> queryComment(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("You must submit username and password");
            }

            var query = from comment in _context.comment
                        where comment.original_id == id
                        select new commentInfo
                        {
                            comment_id = comment.comment_id,
                            comment_content = comment.content,
                            comment_create_time = comment.time,
                            comment_message_id = comment.original_id,
                            comment_sender_id = comment.actor_id
                        };

            var num = await query.CountAsync();
            commentRes res;

            if (num > 0)
            {
                List<commentInfo> commentList = new List<commentInfo>();
                foreach(var comm in query)
                {
                    commentList.Add(comm);
                }
                res = new commentRes() { message = "success", count = num, data = commentList };
                return Ok(ResultToJson.toJson(res));
            }

            res = new commentRes() { message = "success", count = num, data = null };
            return Ok(ResultToJson.toJson(res));

        }

        [HttpGet("countComment/{id}")]
        public async Task<ActionResult> countComment(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("You must submit username and password");
            }

            var query = from comment in _context.comment
                        where comment.original_id == id
                        select comment;

            var num = await query.CountAsync();

            commentRes res = new commentRes() { message = "success", count = num, data = null };
            return Ok(ResultToJson.toJson(res));

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> queryComment([FromBody] newComment newComment)
        {
            comment new_comm = new comment()
            {
                original_id = newComment.message_id,
                actor_id = newComment.userID,
                time = DateTime.Now,
                content = newComment.comment_content,
                at_id = newComment.sender_id
            };

            _context.comment.Add(new_comm);
            await _context.SaveChangesAsync();

            commentRes res = new commentRes() { message = "success" };
            return Ok(ResultToJson.toJson(res));

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

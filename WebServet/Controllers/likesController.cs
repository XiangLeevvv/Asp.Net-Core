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
using MySqlX.XDevAPI.Common;

namespace WebServet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class likesController : ControllerBase
    {
        private readonly UserContext _context;

        public likesController(UserContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("countLike")]
        public async Task<ActionResult> countLike([FromBody] likeInfo likeInfo)
        {
            if (string.IsNullOrEmpty(likeInfo.userID.ToString()) || string.IsNullOrEmpty(likeInfo.messageID.ToString()))
            {
                return BadRequest("You must submit username and password");
            }

            var query = from like in _context.likes
                        where like.post_id == likeInfo.messageID
                        select like;

            var num = await query.CountAsync();

            likeRes res = new likeRes() { message = "success", Count = num };
            return Ok(ResultToJson.toJson(res));
        }

        [HttpPost]
        [Route("isLike")]
        public async Task<ActionResult> isLike([FromBody] likeInfo likeInfo)
        {
            if (string.IsNullOrEmpty(likeInfo.userID.ToString()) || string.IsNullOrEmpty(likeInfo.messageID.ToString()))
            {
                return BadRequest("You must submit username and password");
            }

            likeRes res;

            var query = from like in _context.likes
                        where like.post_id == likeInfo.messageID && like.user_id == likeInfo.userID
                        select like;

            var result = await query.AnyAsync();

            if(result == false)
            {
                res = new likeRes() { message = "not"};
                return Ok(ResultToJson.toJson(res));
            }

            res = new likeRes() { message = "like" };
            return Ok(ResultToJson.toJson(res));
        }

        [HttpPost]
        [Route("iLike")]
        public async Task<ActionResult> iLike([FromBody] likeInfo likeInfo)
        {
            if (string.IsNullOrEmpty(likeInfo.userID.ToString()) || string.IsNullOrEmpty(likeInfo.messageID.ToString()))
            {
                return BadRequest("You must submit username and password");
            }

            likeRes res;

            likes new_like = new likes() { user_id = likeInfo.userID, post_id = likeInfo.messageID };
            _context.likes.Add(new_like);
            await _context.SaveChangesAsync();
            res = new likeRes() { message = "success" };
            return Ok(ResultToJson.toJson(res));
        }

        [HttpPost]
        [Route("cancel")]
        public async Task<ActionResult> cancel([FromBody] likeInfo likeInfo)
        {
            if (string.IsNullOrEmpty(likeInfo.userID.ToString()) || string.IsNullOrEmpty(likeInfo.messageID.ToString()))
            {
                return BadRequest("You must submit username and password");
            }

            var query = from like in _context.likes
                        where like.post_id == likeInfo.messageID && like.user_id == likeInfo.userID
                        select like;

            var result = await query.FirstAsync();
            _context.likes.Remove(result);
            await _context.SaveChangesAsync();

            likeRes res = new likeRes() { message = "success" };
            return Ok(ResultToJson.toJson(res));
        }

        // GET: api/likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<likes>>> Getlikes()
        {
            return await _context.likes.ToListAsync();
        }

        // GET: api/likes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<likes>> Getlikes(int id)
        {
            var likes = await _context.likes.FindAsync(id);

            if (likes == null)
            {
                return NotFound();
            }

            return likes;
        }

        // PUT: api/likes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putlikes(int id, likes likes)
        {
            if (id != likes.user_id)
            {
                return BadRequest();
            }

            _context.Entry(likes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!likesExists(id))
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

        // POST: api/likes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<likes>> Postlikes(likes likes)
        {
            _context.likes.Add(likes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (likesExists(likes.user_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getlikes", new { id = likes.user_id }, likes);
        }

        // DELETE: api/likes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<likes>> Deletelikes(int id)
        {
            var likes = await _context.likes.FindAsync(id);
            if (likes == null)
            {
                return NotFound();
            }

            _context.likes.Remove(likes);
            await _context.SaveChangesAsync();

            return likes;
        }

        private bool likesExists(int id)
        {
            return _context.likes.Any(e => e.user_id == id);
        }
    }
}

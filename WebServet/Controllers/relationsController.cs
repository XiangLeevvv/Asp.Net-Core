using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServet.Models;
using localLib;
using ClassLibrary;
using ClassLibraryPost;

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

        public class FollowResponse
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<UserPublicInfo> data { get; set; }
        }

        [HttpPost("follower/{user_id}")]
        public async Task<ActionResult> Follower(int user_id, BaseQueryInfo baseQueryInfo)
        {
            FollowResponse response = new FollowResponse();
            var query1 =
                from us in _context.user
                join re in _context.relation
                on us.user_id equals re.actor_id into new_table
                from ne in new_table
                where ne.object_id == user_id
                select new UserPublicInfo
                {
                    userId = us.user_id,
                    nickname = us.user_name,
                    portrait = us.portrait
                };
            var result1 = await query1.AnyAsync();

            if (result1 == false)
            {
                response = new FollowResponse() { code = 200, message = "fail", data = null };
                return Ok(ResultToJson.toJson(response));
            }

            int sum = query1.ToList().Count;

            //处理分页问题
            if (sum < baseQueryInfo.startFrom || sum == 0)
            {
                response = new FollowResponse() { code = 200, message = "success", data = null };
                return Ok(ResultToJson.toJson(response));
            }
            if (sum < baseQueryInfo.limitation)
            {
                response = new FollowResponse() { code = 200, message = "success", data = query1.ToList() };
                return Ok(ResultToJson.toJson(response));
            }
            if (sum < baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
            {
                List<UserPublicInfo> tmp = new List<UserPublicInfo>();
                int i = baseQueryInfo.startFrom;
                foreach (var element in query1)
                {
                    i++;
                    tmp.Add(element);
                    if (i >= sum)
                        break;
                }
                response = new FollowResponse() { code = 200, message = "success", data = tmp };
                return Ok(ResultToJson.toJson(response));
            }
            List<UserPublicInfo> tmpReturn = new List<UserPublicInfo>();
            int j = baseQueryInfo.startFrom;
            foreach (var element in query1)
            {
                j++;
                tmpReturn.Add(element);
                if (j >= baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
                    break;
            }
            response = new FollowResponse() { code = 200, message = "success", data = tmpReturn };
            return Ok(ResultToJson.toJson(response));
        }


        [HttpPost("follow/{user_id}")]
        public async Task<ActionResult> Follow(int user_id, BaseQueryInfo baseQueryInfo)
        {
            FollowResponse response = new FollowResponse();
            var query1 =
                from us in _context.user
                join re in _context.relation
                on us.user_id equals re.object_id into new_table
                from ne in new_table
                where ne.actor_id == user_id
                select new UserPublicInfo
                {
                    userId = us.user_id,
                    nickname = us.user_name,
                    portrait = us.portrait
                };
            var result1 = await query1.AnyAsync();

            if (result1 == false)
            {
                response = new FollowResponse() { code = 200, message = "fail", data = null };
                return Ok(ResultToJson.toJson(response));
            }

            int sum = query1.ToList().Count;

            //处理分页问题
            if (sum < baseQueryInfo.startFrom || sum == 0)
            {
                response = new FollowResponse() { code = 200, message = "success", data = null };
                return Ok(ResultToJson.toJson(response));
            }
            if (sum < baseQueryInfo.limitation)
            {
                response = new FollowResponse() { code = 200, message = "success", data = query1.ToList() };
                return Ok(ResultToJson.toJson(response));
            }
            if (sum < baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
            {
                List<UserPublicInfo> tmp = new List<UserPublicInfo>();
                int i = baseQueryInfo.startFrom;
                foreach (var element in query1)
                {
                    i++;
                    tmp.Add(element);
                    if (i >= sum)
                        break;
                }
                response = new FollowResponse() { code = 200, message = "success", data = tmp };
                return Ok(ResultToJson.toJson(response));
            }
            List<UserPublicInfo> tmpReturn = new List<UserPublicInfo>();
            int j = baseQueryInfo.startFrom;
            foreach (var element in query1)
            {
                j++;
                tmpReturn.Add(element);
                if (j >= baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
                    break;
            }
            response = new FollowResponse() { code = 200, message = "success", data = tmpReturn };
            return Ok(ResultToJson.toJson(response));
        }

        public class IfFollowingResponse
        {
            public int code { get; set; }
            public string message { get; set; }
            public IfFollowing data { get; set; }
        }

        public class IfFollowing
        {
            public bool if_following { get; set; }
        }

        public class UserIdInfo
        {
            public int userID { get; set; }
        }

        [HttpPost("if_following_by_me/{be_followed_id}")]
        public async Task<ActionResult> If_following_by_me(int be_followed_id, UserIdInfo userIdInfo)
        {
            IfFollowingResponse response = new IfFollowingResponse();
            var query =
                from re in _context.relation
                where re.actor_id == userIdInfo.userID && re.object_id == be_followed_id
                select re;
            var result = await query.AnyAsync();
            if (query.Count() == 0)
                response = new IfFollowingResponse() { code = 200, message = "success", data = new IfFollowing() { if_following = false } };
            else response = new IfFollowingResponse() { code = 200, message = "success", data = new IfFollowing() { if_following = true } };
            return Ok(ResultToJson.toJson(response));

        }

        [HttpPost("followSb/{user_id}")]
        public async Task<ActionResult> FollowSb(int user_id, UserIdInfo userIdInfo)
        {
            IfFollowingResponse response = new IfFollowingResponse();
            relation re = new relation() { actor_id = userIdInfo.userID, object_id = user_id };

            response = new IfFollowingResponse() { code = 200, message = "fail", data = null };
            var query =
                from r in _context.relation
                where r.actor_id == userIdInfo.userID && r.object_id == user_id
                select r;
            var result = await query.AnyAsync();
            if (query.Count() == 0)
            {
                _context.relation.Add(re);
                await _context.SaveChangesAsync();
            }


            else response.message = "success";
            return Ok(ResultToJson.toJson(response));
        }

        [HttpPost("cancelFollowingTo/{user_id}")]
        public async Task<ActionResult> CancelFollowingTo(int user_id, UserIdInfo userIdInfo)
        {
            IfFollowingResponse response = new IfFollowingResponse();
            relation re = new relation() { actor_id = userIdInfo.userID, object_id = user_id };

            response = new IfFollowingResponse() { code = 200, message = "fail", data = null };
            var query =
                from r in _context.relation
                where r.actor_id == userIdInfo.userID && r.object_id == user_id
                select r;
            var result = await query.AnyAsync();

            if (query.Count() != 0)
            {
                var tmp = await _context.relation.FindAsync(query.First().relation_id);
                _context.relation.Remove(tmp);
                await _context.SaveChangesAsync();
            }
            else response.message = "success";
            return Ok(ResultToJson.toJson(response));
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

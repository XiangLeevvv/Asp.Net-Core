using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServet.Models;
using localLib;
using ClassLibraryPost;
using COMLib;
using CLRDll;

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

        //public class QueryMessageInfo
        //{
        //    public int message_sender_user_id { get; set; }
        //    public int message_id { get; set; }
        //    public DateTime message_create_time { get; set; }
        //    public string message_content { get; set; }
        //}

        //public class QueryMessageResponse
        //{
        //    public int code { get; set; }
        //    public string message { get; set; }
        //    public List<QueryMessageInfo> data { get; set; }
        //}

        //public class QueryNewestMessageResponse
        //{
        //    public int code { get; set; }
        //    public string message { get; set; }
        //    public List<QueryMessageInfo> data { get; set; }
        //}

        //public class BaseQueryInfo
        //{
        //    public int startFrom { get; set; }
        //    public int limitation { get; set; }
        //    public int userID { get; set; }
        //}

        //public class ContainsInfo
        //{
        //    public int startFrom { get; set; }
        //    public int limitation { get; set; }
        //    public string info { get; set; }
        //}


        [HttpPost]
        [Route("queryFollowMessage")]
        public async Task<ActionResult> PostQueryFollowMessage([FromBody] BaseQueryInfo baseQueryInfo)
        {
            QueryMessageResponse response = new QueryMessageResponse();

            var query1 =
                from po in _context.posts
                join re in _context.relation
                on po.user_id equals re.object_id into new_table
                from ne in new_table
                where ne.actor_id == baseQueryInfo.userID
                orderby po.time descending
                select new QueryMessageInfo
                {
                    message_sender_user_id = ne.object_id,
                    message_content = po.content,
                    message_create_time = po.time,
                    message_id = po.post_id
                };
            var result1 = await query1.AnyAsync();

            if (result1 == false)
            {
                response = new QueryMessageResponse() { code = 200, message = "fail", data = null };
                return Ok(ResultToJson.toJson(response));
            }

            int sum = query1.ToList().Count;

            //处理分页问题
            if (sum < baseQueryInfo.startFrom || sum == 0)
            {
                response = new QueryMessageResponse() { code = 200, message = "success", data = null };
                return Ok(ResultToJson.toJson(response));
            }
            if (sum < baseQueryInfo.limitation)
            {
                response = new QueryMessageResponse() { code = 200, message = "success", data = query1.ToList() };
                return Ok(ResultToJson.toJson(response));
            }
            if (sum < baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
            {
                List<QueryMessageInfo> tmp = new List<QueryMessageInfo>();
                int i = baseQueryInfo.startFrom;
                foreach (var element in query1)
                {
                    i++;
                    tmp.Add(element);
                    if (i >= sum)
                        break;
                }
                response = new QueryMessageResponse() { code = 200, message = "success", data = tmp };
                return Ok(ResultToJson.toJson(response));
            }
            List<QueryMessageInfo> tmpReturn = new List<QueryMessageInfo>();
            int j = baseQueryInfo.startFrom;
            foreach (var element in query1)
            {
                j++;
                tmpReturn.Add(element);
                if (j >= baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
                    break;
            }
            response = new QueryMessageResponse() { code = 200, message = "success", data = tmpReturn };
            return Ok(ResultToJson.toJson(response));

        }

        [HttpPost]
        [Route("queryNewestMessage")]
        public async Task<ActionResult> QueryNewestMessage([FromBody] BaseQueryInfo baseQueryInfo)
        {
            QueryNewestMessageResponse response = new QueryNewestMessageResponse();
            var query1 =
                from po in _context.posts
                orderby po.time descending
                select new QueryMessageInfo
                {
                    message_sender_user_id = po.user_id,
                    message_content = po.content,
                    message_create_time = po.time,
                    message_id = po.post_id
                };
            var result1 = await query1.AnyAsync();
            if (result1 == false)
            {
                response = new QueryNewestMessageResponse() { code = 200, message = "fail", data = null };
                return Ok(ResultToJson.toJson(response));
            }
            int sum = query1.ToList().Count;

            //处理分页问题
            if (sum < baseQueryInfo.startFrom || sum == 0)
            {
                response = new QueryNewestMessageResponse() { code = 200, message = "success", data = null };
                return Ok(ResultToJson.toJson(response));
            }

            if (sum < baseQueryInfo.limitation)
            {
                response = new QueryNewestMessageResponse() { code = 200, message = "success", data = query1.ToList() };
                return Ok(ResultToJson.toJson(response));
            }
            if (sum < baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
            {
                List<QueryMessageInfo> tmp = new List<QueryMessageInfo>();
                int i = baseQueryInfo.startFrom;
                foreach (var element in query1)
                {
                    i++;
                    tmp.Add(element);
                    if (i >= sum)
                        break;
                }
                response = new QueryNewestMessageResponse() { code = 200, message = "success", data = tmp };
                return Ok(ResultToJson.toJson(response));
            }
            List<QueryMessageInfo> tmpReturn = new List<QueryMessageInfo>();
            int j = baseQueryInfo.startFrom;
            foreach (var element in query1)
            {
                j++;
                tmpReturn.Add(element);
                if (j >= baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
                    break;
            }
            response = new QueryNewestMessageResponse() { code = 200, message = "success", data = tmpReturn };
            return Ok(ResultToJson.toJson(response));

        }

        [HttpPost("queryMessage/{user_id}")]
        public async Task<ActionResult> QueryMessage(int user_id, BaseQueryInfo baseQueryInfo)
        {
            QueryNewestMessageResponse response = new QueryNewestMessageResponse();

            var query1 =
                from po in _context.posts
                where po.user_id == user_id
                orderby po.time descending
                select new QueryMessageInfo
                {
                    message_sender_user_id = po.user_id,
                    message_content = po.content,
                    message_create_time = po.time,
                    message_id = po.post_id
                };
            var result1 = await query1.AnyAsync();
            if (result1 == false)
            {
                response = new QueryNewestMessageResponse() { code = 200, message = "fail", data = null };
                return Ok(ResultToJson.toJson(response));
            }
            int sum = query1.ToList().Count;

            //处理分页问题
            if (sum < baseQueryInfo.startFrom || sum == 0)
            {
                response = new QueryNewestMessageResponse() { code = 200, message = "success", data = null };
                return Ok(ResultToJson.toJson(response));
            }

            if (sum < baseQueryInfo.limitation)
            {
                response = new QueryNewestMessageResponse() { code = 200, message = "success", data = query1.ToList() };
                return Ok(ResultToJson.toJson(response));
            }
            if (sum < baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
            {
                List<QueryMessageInfo> tmp = new List<QueryMessageInfo>();
                int i = baseQueryInfo.startFrom;
                foreach (var element in query1)
                {
                    i++;
                    tmp.Add(element);
                    if (i >= sum)
                        break;
                }
                response = new QueryNewestMessageResponse() { code = 200, message = "success", data = tmp };
                return Ok(ResultToJson.toJson(response));
            }
            List<QueryMessageInfo> tmpReturn = new List<QueryMessageInfo>();
            int j = baseQueryInfo.startFrom;
            foreach (var element in query1)
            {
                j++;
                tmpReturn.Add(element);
                if (j >= baseQueryInfo.limitation + baseQueryInfo.startFrom - 1)
                    break;
            }
            response = new QueryNewestMessageResponse() { code = 200, message = "success", data = tmpReturn };
            return Ok(ResultToJson.toJson(response));
        }

        [HttpPost]
        [Route("queryMessagesContains")]
        public async Task<ActionResult> QueryMessagesContains([FromBody] ContainsInfo containsInfo)
        {
            //COM组件使用
            MyComClass myComClass = new MyComClass();

            QueryNewestMessageResponse response = new QueryNewestMessageResponse();
            var query1 =
                from po in _context.posts
                where po.content.Contains(containsInfo.info)
                select new QueryMessageInfo
                {
                    message_sender_user_id = po.user_id,
                    message_content = po.content,
                    message_create_time = po.time,
                    message_id = po.post_id
                };
            var result1 = await query1.AnyAsync();
            if (result1 == false)
            {
                response = new QueryNewestMessageResponse() { code = 200, message = "fail", data = null };
                return Ok(ResultToJson.toJson(response));
            }
            int sum = query1.ToList().Count;

            //处理分页问题
            if (sum < containsInfo.startFrom || sum == 0)
            {
                response = new QueryNewestMessageResponse() { code = 200, message = "success", data = null };
                return Ok(ResultToJson.toJson(response));
            }

            if (sum < containsInfo.limitation)
            {
                response = new QueryNewestMessageResponse() { code = 200, message = "success", data = query1.ToList() };
                return Ok(ResultToJson.toJson(response));
            }
            if (sum < myComClass.Add(containsInfo.limitation, containsInfo.startFrom) - 1)
            {
                List<QueryMessageInfo> tmp = new List<QueryMessageInfo>();
                //int i = containsInfo.startFrom;
                //CLR组件使用
                test t1 = new test(containsInfo.startFrom);
                foreach (var element in query1)
                {
                    t1.Increase();
                    tmp.Add(element);
                    if (t1.GetCount() >= sum)
                        break;
                }
                response = new QueryNewestMessageResponse() { code = 200, message = "success", data = tmp };
                return Ok(ResultToJson.toJson(response));
            }
            List<QueryMessageInfo> tmpReturn = new List<QueryMessageInfo>();
            //int j = containsInfo.startFrom;
            //CLR组件使用
            test t2 = new test(containsInfo.startFrom);
            foreach (var element in query1)
            {
                t2.Increase();
                tmpReturn.Add(element);
                if (t2.GetCount() >= myComClass.Add(containsInfo.limitation, containsInfo.startFrom) - 1)
                    break;
            }
            response = new QueryNewestMessageResponse() { code = 200, message = "success", data = tmpReturn };
            return Ok(ResultToJson.toJson(response));
        }

        [HttpPost]
        [Route("send")]
        public async Task<ActionResult> createPost([FromBody] Message message)
        {
            if (string.IsNullOrEmpty(message.message_content) || string.IsNullOrEmpty(message.userID.ToString()))
            {
                return BadRequest("You must submit username and password");
            }

            messageRes res;

            post new_post = new post() { user_id = message.userID, content = message.message_content, delete_flag = 0, time = DateTime.Now };
            _context.posts.Add(new_post);
            await _context.SaveChangesAsync();
            res = new messageRes() { message = "success" };
            return Ok(ResultToJson.toJson(res));

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

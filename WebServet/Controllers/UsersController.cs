using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServet.Models;
using COMLib;
using localLib;
using CLRDll;
using ClassLibrary;


namespace WebServet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getuser()
        {
            return await _context.user.ToListAsync();
        }


        public class UserPublicResponse
        {
            public int code { get; set; }
            public string message { get; set; }
            public UserPublicInfo data { get; set; }
        }


        [HttpGet("getUserPublicInfo/{user_id}")]
        public async Task<IActionResult> GetUserPublicInfo(int user_id)
        {
            UserPublicInfo userPublicInfo = new UserPublicInfo();
            UserPublicResponse response;
            userPublicInfo.userId = user_id;

            var query =
                from use in _context.user
                where use.user_id == userPublicInfo.userId
                select use;
            var result = await query.AnyAsync();

            if (result == false)
            {
                response = new UserPublicResponse() { code = 200, message = "fail", data = null };
                return Ok(ResultToJson.toJson(response));
            }
            userPublicInfo.nickname = query.First().user_name;
            userPublicInfo.portrait = query.First().portrait;

            var query1 =
                from po in _context.relation
                where po.actor_id == userPublicInfo.userId
                select po;
            var result1 = await query1.AnyAsync();
            int follows_num = 0;
            foreach (var item in query1) { follows_num += 1; }
            userPublicInfo.follows_num = follows_num;

            var query2 =
                from po in _context.relation
                where po.object_id == userPublicInfo.userId
                select po;
            var result2 = await query2.AnyAsync();
            int followers_num = 0;
            foreach (var item in query2) { followers_num += 1; }
            userPublicInfo.followers_num = followers_num;

            response = new UserPublicResponse() { code = 200, message = "success", data = userPublicInfo };
            return Ok(ResultToJson.toJson(response));
        }


        [HttpPost]
        [Route("signIn")]
        public async Task<IActionResult> SignIn([FromBody]LoginInfo loginInfo)
        {
            LoginResponse response;

            MyComClass f = new MyComClass();
            System.Diagnostics.Debug.WriteLine(f.Add(1,2));

            test t = new test();
            System.Diagnostics.Debug.WriteLine(t.GetCount());


            if (string.IsNullOrEmpty(loginInfo.name) || string.IsNullOrEmpty(loginInfo.password))
            {
                response = new LoginResponse() { code = 200, message = "error", data = null };
                return Ok(ResultToJson.toJson(response));
            }

            var query =
                from use in _context.user
                where use.user_name == loginInfo.name && use.password == loginInfo.password
                select use;

            var result = await query.AnyAsync();

            if (result == false)
            {
                response = new LoginResponse() { code = 200, message = "wrong username or password", data = null };
                return Ok(ResultToJson.toJson(response));
            }

            User user = query.First();
            LoginInfo loginData = new LoginInfo() { user_id = user.user_id, name = user.user_name, password = user.password };
            response = new LoginResponse() { code = 200, message = "login success", data = loginData };
            return Ok(ResultToJson.toJson(response));
        }


        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> SignUp([FromBody]LoginInfo loginInfo)
        {
            if (string.IsNullOrEmpty(loginInfo.name) || string.IsNullOrEmpty(loginInfo.password))
            {
                return BadRequest("You must submit username and password");
            }

            LoginResponse response;

            var query =
                from use in _context.user
                where use.user_name == loginInfo.name && use.password == loginInfo.password
                select use;

            var result = await query.AnyAsync();

            if (result == false)
            {
                User new_user = new User() { user_name = loginInfo.name, password = loginInfo.password, portrait = "null" };
                _context.user.Add(new_user);
                await _context.SaveChangesAsync();
                response = new LoginResponse() { code = 200, message = "success", data = null };
                return Ok(ResultToJson.toJson(response));
            }

            response = new LoginResponse() { code = 200, message = "your name is used", data = null };
            return Ok(ResultToJson.toJson(response));
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.user_id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.user.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.user_id }, user);
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.user.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.user.Any(e => e.user_id == id);
        }
    }
}

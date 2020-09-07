using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using GameServer.Database;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
//using GameServer.Patterns;
using Patterns.Adapter;
using Patterns.ChainOfResponsability;

namespace GameServer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View("this in index");
        //}

        public UserController(DatabaseContext context)
        {
            _context = context;
        }


        // GET api/player
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return _context.User.ToList();
        }

        //GET api/user/5
        [HttpGet("{id}", Name = "getUser")]
        public ActionResult<User> GetById(long id)
        {
            User u = _context.User.Find(id);
            if (u == null)
            {
                return NotFound("player not found");
            }
            return u;
        }

        [HttpGet("game/{userId}", Name = "GetPlayerGame")]
        public ActionResult<Game> GetPlayerGame(long userId)
        {
            return _context.Game
                .Where(g => g.JoinerId == userId || g.HostId == userId)
                .FirstOrDefault();
        }

        // POST api/users/login
        [HttpPost("login")]
        //public string Create(Player player)
        public string Login([FromBody]JObject data)
        {
            User user = _context.User
                .Where(u => u.Username == data["username"].ToString() && u.Password == data["password"].ToString())
                .FirstOrDefault();
            if (user == null)
            {
                return JsonConvert.SerializeObject(new Object());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;

                LogHandler h1 = new ConsoleLogHandler();
                LogHandler h2 = new DatabaseLogHandler();
                LogHandler h3 = new FileLogHandler();
                h1.SetSuccessor(h2);
                h2.SetSuccessor(h3);
                Tuple<string, string>[] logs = {Tuple.Create(DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt") + " " + user.Username + " logged in", "console"),
                                                Tuple.Create("Got login request from user ID: " + user.Id, "database"),
                                                Tuple.Create("User ID: " + user.Id + "User username: " + user.Username, "file")
                };
                foreach(Tuple<string, string> log in logs)
                {
                    h1.HandleLog(log.Item1, log.Item2);

                }

                Console.ResetColor();
                return JsonConvert.SerializeObject(user);
            }
        }

        // POST api/users/login
        [HttpPost("register")]
        //public string Create(Player player)
        public IActionResult Register([FromBody]JObject data)
        {

            User user = _context.User
                .Where(u => u.Username == data["username"].ToString())
                .FirstOrDefault();
            //Jei toks neegzistuoja
            if (user == null)
            {
                User newUser = new User(data["username"].ToString(), data["password"].ToString());
                _context.Add(newUser);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        //    // PUT api/values/5
        //    [HttpPut("{id}")]
        //    public IActionResult Update(long id, [FromBody] Player p)
        //    {
        //        var pp = _context.Players.Find(id);
        //        if (pp == null)
        //        {
        //            return NotFound();
        //        }

        //        //pp.Name = p.Name;
        //       // pp.PosX = p.PosX;
        //        ///pp.PosY = p.PosY;
        //       // pp.Score = p.Score;

        //        _context.Players.Update(pp);
        //        _context.SaveChanges();

        //        return Ok(); //NoContent();
        //    }

        //    [HttpPatch]
        //    public IActionResult PartialUpdate([FromBody] Coordinates request)
        //    {
        //        var player = _context.Players.Find(request.Id);
        //        if (player == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //        //    player.PosX = request.PosX;
        //        //    player.PosY = request.PosY;

        //            _context.Players.Update(player);
        //            _context.SaveChanges();
        //        }
        //        return Ok();
        //        //return CreatedAtRoute("GetPlayer", new { id = player.Id }, player);
        //    }

        //    // DELETE api/values/5
        //    /*[HttpDelete("{id}")]
        //    public void Delete(int id)
        //    {
        //    }*/
        //    [HttpDelete("{id}")]
        //    public IActionResult Delete(long id)
        //    {
        //        var todo = _context.Players.Find(id);
        //        if (todo == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Players.Remove(todo);
        //        _context.SaveChanges();
        //        return NoContent();
        //    }

    }
}





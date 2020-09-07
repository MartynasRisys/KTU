using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using GameServer.Database;
using Newtonsoft.Json.Linq;
using GameServer.Patterns.Observer;
using Microsoft.AspNetCore.SignalR;
using GameServer.Patterns.Observer.Hubs;

namespace GameServer.Controllers
{
    [Route("api/gamerooms")]
    [ApiController]
    public class GameRoomController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly GameRoomObserver _observer;

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View("this in index");
        //}

        public GameRoomController(DatabaseContext context, IHubContext<GameRoomHub> hubContext)
        {
            _context = context;
            _observer = new GameRoomObserver(hubContext);
        }


        //GET api/player
        [HttpGet]
        public ActionResult<IEnumerable<GameRoom>> GetAll()
        {
            return _context.GameRoom.ToList();
        }

        // GET api/gamerooms/5
        [HttpGet("{id}", Name = "GetGameroom")]
        public ActionResult<GameRoom> GetById(long id)
        {
            GameRoom g = _context.GameRoom.Find(id);
            if (g == null)
            {
                return NotFound("Gameroom not found");
            }
            return g;
        }

        // POST api/gamerooms/create
        [HttpPost("create")]
        public async Task<ActionResult<GameRoom>> Create([FromBody] JObject data)
        {
            User userHost = _context.User
                 .Where(u => u.Id == Int32.Parse(data["userHostId"].ToString()))
                 .FirstOrDefault();
            //Jei toks neegzistuoja
            if (userHost != null)
            {
                GameRoom newGameRoom = new GameRoom(data["roomName"].ToString(), 
                                                    userHost.Id,
                                                    Int32.Parse(data["startingGold"].ToString()),
                                                    (GameRoom.MapSizeEnum)Int32.Parse(data["mapSize"].ToString()));
                _context.GameRoom.Add(newGameRoom);
                _context.SaveChanges();
                await _observer.NotifyGameRoomCreated(newGameRoom);

                return Ok(newGameRoom);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/gamerooms/create
        [HttpPost("exit")]
        public async Task<ActionResult> Exit([FromBody] JObject data)
        {
            GameRoom gameRoom = _context.GameRoom
                 .Where(g => g.Id == Int32.Parse(data["gameRoomId"].ToString()))
                 .FirstOrDefault();
            //Jei toks neegzistuoja
            if (gameRoom != null)
            {
                if (gameRoom.UserHostId == Int32.Parse(data["userId"].ToString()))
                {
                    gameRoom.UserHostId = 0;
                }
                if (gameRoom.UserJoinerId == Int32.Parse(data["userId"].ToString()))
                {
                    gameRoom.UserJoinerId = 0;
                }

                User user = _context.User
                    .Where(u => u.Id == Int32.Parse(data["userId"].ToString()))
                    .FirstOrDefault();

                //Jei isejo host'as padaryti joineri host'u
                if (gameRoom.UserHostId == 0 && gameRoom.UserJoinerId != 0)
                {
                    gameRoom.UserHostId = gameRoom.UserJoinerId;
                    gameRoom.UserJoinerId = 0;
                    _context.Update(gameRoom);
                    await _observer.NotifyGameRoomHostLeft(gameRoom, user);
                }

                if (gameRoom.UserHostId != 0 && gameRoom.UserJoinerId == 0)
                {
                    gameRoom.UserJoinerId = 0;
                    _context.Update(gameRoom);
                    await _observer.NotifyGameRoomLeft(gameRoom, user);
                }

                //Jei isejo paskutinis zaidejas, istrint game room
                else if (gameRoom.UserHostId == 0 && gameRoom.UserJoinerId == 0)
                {
                    await _observer.NotifyGameRoomDeleted(gameRoom);
                    _context.Remove(gameRoom);
                }
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/gamerooms/join
        [HttpPost("join")]
        public async Task<ActionResult<GameRoom>> Join([FromBody] JObject data)
        {
            GameRoom gameRoom = _context.GameRoom
                 .Where(g => g.Id == Int32.Parse(data["gameRoomId"].ToString()))
                 .FirstOrDefault();
            //Jei toks egzistuoja
            if (gameRoom != null)
            {
                if (gameRoom.UserJoinerId == 0)
                {
                    User user = _context.User
                        .Where(g => g.Id == Int32.Parse(data["userId"].ToString()))
                        .FirstOrDefault();
                    gameRoom.UserJoinerId = user.Id;
                    await _observer.NotifyGameRoomJoined(gameRoom, user);

                    _context.SaveChanges();

                    return Ok(gameRoom);
                }
                else
                {
                    return BadRequest();

                }
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





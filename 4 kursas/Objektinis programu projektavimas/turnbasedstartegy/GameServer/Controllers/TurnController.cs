using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using GameServer.Database;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Patterns.Adapter;
using Microsoft.AspNetCore.SignalR;
using GameServer.Patterns.Observer.Hubs;
using GameServer.Patterns.Observer;

namespace GameServer.Controllers
{
    [Route("api/turns")]
    [ApiController]
    public class TurnController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly GameObserver _observer;

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View("this in index");
        //}

        public TurnController(DatabaseContext context, IHubContext<GameHub> hubContext)
        {
            _context = context;
            _observer = new GameObserver(hubContext);
        }


        // GET api/player
        [HttpGet]
        public ActionResult<IEnumerable<Turn>> GetAll()
        {
            return _context.Turn.ToList();
        }

        //GET api/turns/5
        [HttpGet("{id}", Name = "getTurn")]
        public ActionResult<Turn> GetById(long id)
        {
            Turn t = _context.Turn.Find(id);
            if (t == null)
            {
                return NotFound("player not found");
            }
            return t;
        }

        [HttpGet("game/{gameId}/latestTurn", Name = "GetGameLatestTurn")]
        public ActionResult<Turn> getGameLatestTurns(long gameId)
        {
            return _context.Turn
                .Where(t => t.GameId == gameId)
                .OrderByDescending(t => t.Number)
                .FirstOrDefault();
                
        }

        [HttpGet("game/{gameId}/endJoiner", Name = "EndGameLatestTurnJoinerTurn")]
        public async Task<ActionResult> endJoinerTurn(long gameId)
        {
            Turn currentTurn = _context.Turn
                .Where(t => t.GameId == gameId)
                .OrderByDescending(t => t.Number)
                .FirstOrDefault();

            currentTurn.PlayerJoinerEnded = true;
            _context.Update(currentTurn);
            if (currentTurn.PlayerHostEnded == true)
            {
                Turn newTurn = new Turn(currentTurn.Number + 1, gameId);
                _context.Add(newTurn);
                await _observer.NotifyTurnUpdated(newTurn);
            }
            else
            {
                await _observer.NotifyTurnUpdated(currentTurn);
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("game/{gameId}/endHost", Name = "EndGameLatestTurnHostTurn")]
        public async Task<ActionResult> endHostTurn(long gameId)
        {
            Turn currentTurn = _context.Turn
                .Where(t => t.GameId == gameId)
                .OrderByDescending(t => t.Number)
                .FirstOrDefault();

            currentTurn.PlayerHostEnded = true;
            _context.Update(currentTurn);
            if (currentTurn.Number == 0 && currentTurn.PlayerJoinerEnded == true)
            {
                Turn newTurn = new Turn(currentTurn.Number + 1, gameId);
                _context.Add(newTurn);
                await _observer.NotifyTurnUpdated(newTurn);
            }
            else
            {
                await _observer.NotifyTurnUpdated(currentTurn);

            }
            _context.SaveChanges();
            return Ok();

        }

        //// POST api/users/login
        //[HttpPost("login")]
        ////public string Create(Player player)
        //public string Login([FromBody]JObject data)
        //{
        //    User user = _context.User
        //        .Where(u => u.Username == data["username"].ToString() && u.Password == data["password"].ToString())
        //        .FirstOrDefault();
        //    if (user == null)
        //    {
        //        return JsonConvert.SerializeObject(new Object());
        //    }
        //    else
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        //Logger.getInstance.logMessage(DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt") + " " + user.Username + " logged in");

        //        ConsoleLogger consoleLogger = ConsoleLogger.getInstance;
        //        DataBaseLogger dataBaseLogger = DataBaseLogger.getInstance;
        //        FileLogger fileLogger = FileLogger.getInstance;
        //        Logger dataBaseLoggerAdapter = new DataBaseLoggerAdapter(dataBaseLogger);             
        //        Logger fileLoggerAdapter = new FileLoggerAdapter(fileLogger);

        //        consoleLogger.logMessage(DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt") + " " + user.Username + " logged in");
        //        dataBaseLoggerAdapter.logMessage(DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt") + " " + user.Username + " logged in");           
        //        fileLoggerAdapter.logMessage(DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt") + " " + user.Username + " logged in");

        //        Console.ResetColor();
        //        return JsonConvert.SerializeObject(user);
        //    }
        //}

        //// POST api/users/login
        //[HttpPost("register")]
        ////public string Create(Player player)
        //public IActionResult Register([FromBody]JObject data)
        //{

        //    User user = _context.User
        //        .Where(u => u.Username == data["username"].ToString())
        //        .FirstOrDefault();
        //    //Jei toks neegzistuoja
        //    if (user == null)
        //    {
        //        User newUser = new User(data["username"].ToString(), data["password"].ToString());
        //        _context.Add(newUser);
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}


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





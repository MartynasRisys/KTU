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
    [Route("api/turn_action")]
    [ApiController]
    public class TurnActionController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly GameObserver _observer;

        public TurnActionController(DatabaseContext context, IHubContext<GameHub> hubContext)
        {
            _context = context;
            _observer = new GameObserver(hubContext);
        }


        // GET api/player
        [HttpGet]
        public ActionResult<IEnumerable<TurnAction>> GetAll()
        {
            return _context.TurnAction.ToList();
        }

        //GET api/turns/5
        [HttpGet("{id}", Name = "getTurnAction")]
        public ActionResult<TurnAction> GetById(long id)
        {
            TurnAction ta = _context.TurnAction.Find(id);
            if (ta == null)
            {
                return NotFound("turn action not found");
            }
            return ta;
        }

        // POST api/users/login
        [HttpPost("create")]
        public async Task<IActionResult> CreateTurnAction([FromBody]JObject data)
        {
            int typeIndex = Int16.Parse(data["typeIndex"].ToString());
            long turnId = Int64.Parse(data["turnId"].ToString());
            long gameId = Int64.Parse(data["gameId"].ToString());
            long playerId = Int64.Parse(data["playerId"].ToString());
            int x1 = Int16.Parse(data["x1"].ToString());
            int y1 = Int16.Parse(data["y1"].ToString());
            int x2 = Int16.Parse(data["x2"].ToString());
            int y2 = Int16.Parse(data["y2"].ToString());

            TurnAction action = new TurnAction(turnId, playerId, gameId, typeIndex, x1, y1, x2, y2);
            _context.TurnAction.Add(action);
            await _observer.NotifyTurnActionCreated(action);
            _context.SaveChanges();
            return Ok();

        }

    }
}





using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Views;
using GameServer.Database;
using Newtonsoft.Json.Linq;
using GameServer.Patterns.Builder;
using Microsoft.AspNetCore.SignalR;
using GameServer.Patterns.Observer.Hubs;
using GameServer.Patterns.Observer;

namespace GameServer.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly GameRoomObserver _observer;

        /*public IActionResult Index()
        {
            return View();
        }*/

        public GameController(DatabaseContext context, IHubContext<GameRoomHub> hubContext)
        {
            _context = context;
            _observer = new GameRoomObserver(hubContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Game>> GetAll()
        {
            return _context.Game.ToList();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Game>> Create([FromBody] JObject data)
        {
            //Create map
            MapBuilder mapBuilder;
            MapGenerator mapGenerator = new MapGenerator();
            long gameRoomId = Int64.Parse(data["gameRoomId"].ToString());
            long hostId = Int64.Parse(data["hostId"].ToString());
            long joinerId = Int64.Parse(data["joinerId"].ToString());
            GameRoom g = _context.GameRoom.Find(gameRoomId);
            if (g == null)
            {
                return NotFound("Gameroom not found");
            }
            
            if (g.MapSize == GameRoom.MapSizeEnum.Small)
            {
                mapBuilder = new SmallMapBuilder();
            }
            else if (g.MapSize == GameRoom.MapSizeEnum.Medium)
            {
                mapBuilder = new MediumMapBuilder();
            }
            else
            {
                mapBuilder = new LargeMapBuilder();
            }
            mapGenerator.Construct(mapBuilder);

            int mapSize = mapBuilder.Map.MapMatrix.GetLength(0);
            Map mapToSave = new Map(mapSize);

            _context.Map.Add(mapToSave);
            _context.SaveChanges();

            //Create game
            Game newGame = new Game(mapToSave.Id, hostId, joinerId);

            _context.Game.Add(newGame);
            _context.SaveChanges();

            List<Obstacle> obstacles = new List<Obstacle>();
            for (int k = 0; k < mapSize; k++)
            {
                for (int l = 0; l < mapSize; l++)
                {
                    IMapUnitView unit = mapBuilder.Map.MapMatrix[k, l];
                    if (unit != null && unit.Type == MapUnitTypeEnum.Obstacle)
                    {
                        obstacles.Add(new Obstacle(mapToSave.Id, k, l));
                    }
                }
            }

            _context.Obstacle.AddRange(obstacles.ToArray());
            _context.SaveChanges();
            await _observer.NotifyGameStarted(g, newGame);

            Turn startTurn = new Turn(0, newGame.Id);

            _context.Turn.Add(startTurn);
            _context.SaveChanges();


            return Ok(newGame);
        }
    }
}
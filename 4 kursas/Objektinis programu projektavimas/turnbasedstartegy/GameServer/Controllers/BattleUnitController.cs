using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Database;
using GameServer.Patterns.Factory;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Views;
using Models;

namespace GameServer.Controllers
{
    [Route("api/battle_units")]
    [ApiController]
    public class BattleUnitController : ControllerBase
    {
        private readonly DatabaseContext _context;

        /*public IActionResult Index()
        {
            return View();
        }*/

        public BattleUnitController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BattleUnit>> GetAll()
        {
            return _context.BattleUnit.ToList();
        }

        [HttpGet("map/{mapId}", Name = "GetGameBattleUnits")]
        public ActionResult<IEnumerable<BattleUnit>> GetMapBattleUnits(long mapId)
        {
            return _context.BattleUnit
                .Where(bu => bu.MapId == mapId)
                .ToList();
        }

        [HttpPost("create")]
        public ActionResult<BattleUnit> Create([FromBody] JObject data)
        {

            Factory factory = new BattleUnitFactory();
            int typeIndex = Int16.Parse(data["selectedBattleUnitTypeIndex"].ToString());
            long mapId = Int64.Parse(data["mapId"].ToString());
            long playerId = Int64.Parse(data["playerId"].ToString());
            int x = Int16.Parse(data["x"].ToString());
            int y = Int16.Parse(data["y"].ToString());
            bool specialAbilityUsed = Boolean.Parse(data["specialAbilityUsed"].ToString());
            BattleUnitView battleUnit = factory.createBattleUnit(typeIndex, mapId, playerId, x, y);

            BattleUnit newBattleUnit = new BattleUnit(mapId, playerId, typeIndex, x, y, battleUnit.DamageVsSquare,
                battleUnit.DamageVsTriangle, battleUnit.DamageVsCircle, specialAbilityUsed);
            _context.BattleUnit.Add(newBattleUnit);
            _context.SaveChanges();
            return Ok(newBattleUnit);
        }
    }
}
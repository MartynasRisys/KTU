using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using GameServer.Database;
using Newtonsoft.Json.Linq;

namespace GameServer.Controllers
{
    [Route("api/maps")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly DatabaseContext _context;

        /*public IActionResult Index()
        {
            return View();
        }*/

        public MapController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Map>> GetAll()
        {
            return _context.Map.ToList();
        }

        //GET api/maps/5
        [HttpGet("{id}", Name = "getMap")]
        public ActionResult<Map> GetById(long id)
        {
            Map m = _context.Map.Find(id);
            if (m == null)
            {
                return NotFound("map not found");
            }
            return m;
        }

        //GET api/maps/5/obstacles
        [HttpGet("{mapId}/obstacles", Name = "getMapObstacles")]
        public ActionResult<List<Obstacle>> GetMapObstacles(long mapId)
        {
            return _context.Obstacle
                .Where(o => o.MapId == mapId)
                .ToList();
        }
    }
}
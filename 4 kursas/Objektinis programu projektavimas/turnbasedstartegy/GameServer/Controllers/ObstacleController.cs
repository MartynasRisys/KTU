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
    [Route("api/obstacles")]
    [ApiController]
    public class ObstacleController : ControllerBase
    {
        private readonly DatabaseContext _context;

        /*public IActionResult Index()
        {
            return View();
        }*/

        public ObstacleController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Obstacle>> GetAll()
        {
            return _context.Obstacle.ToList();
        }

    }
}
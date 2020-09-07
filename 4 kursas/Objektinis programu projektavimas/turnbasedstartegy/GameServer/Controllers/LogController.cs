using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;

namespace GameServer.Controllers
{
    [Route("api/logs")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public LogController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Log>> GetAll()
        {
            return _context.Log.ToList();
        }

        // POST api/log
        [HttpPost]
        public IActionResult Create([FromBody] JObject data)
        {
            Log newLog = new Log(Int64.Parse(data["userId"].ToString()), data["username"].ToString(), data["message"].ToString());
            _context.Add(newLog);
            _context.SaveChanges();
            return Ok();
        }

    }
}
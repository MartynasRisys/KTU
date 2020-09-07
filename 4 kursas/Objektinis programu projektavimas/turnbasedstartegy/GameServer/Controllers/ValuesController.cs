using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameServer.Database;
using Models;

namespace GameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        private void initiateDatabaseData(DatabaseContext context)
        {
            if (_context.User.Count() == 0)
            {
                int Qty = 1;
                User testUser1 = new User { Username = "q", Password = "q" };
                User testUser2 = new User { Username = "w", Password = "w" };
                _context.User.Add(testUser1);
                _context.User.Add(testUser2);
                for (int i = 0; i < 10; i++)
                {
                    Qty++;
                    User user = new User { Username = "User-" + Qty, Password = "password" };
                    _context.User.Add(user);
                }

            }
            
            Turn testTurn1 = new Turn(2, 10);
            Turn testTurn2 = new Turn(0, 10);
            Turn testTurn3 = new Turn(5, 10);
            Turn testTurn4 = new Turn(1, 10);
            Turn testTurn5 = new Turn(4, 10);
            Turn testTurn6 = new Turn(3, 10);
            _context.Turn.Add(testTurn1);
            _context.Turn.Add(testTurn2);
            _context.Turn.Add(testTurn3);
            _context.Turn.Add(testTurn4);
            _context.Turn.Add(testTurn5);
            _context.Turn.Add(testTurn6);



            if (_context.GameRoom.Count() == 0)
            {
                int Qty = 1;
                for (int i = 0; i < 10; i++)
                {
                    Qty++;
                    GameRoom gameroom = new GameRoom { Name = "gameroom Nr. " + Qty };
                    _context.GameRoom.Add(gameroom);
                }

            }
            _context.SaveChanges();
        }

        public ValuesController(DatabaseContext context)
        {
            _context = context;
            initiateDatabaseData(_context);


        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

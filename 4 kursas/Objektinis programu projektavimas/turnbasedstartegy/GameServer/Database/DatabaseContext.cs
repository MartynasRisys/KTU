using Microsoft.EntityFrameworkCore;
using System;
using Models;
namespace GameServer.Database
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<GameRoom> GameRoom { get; set; }
        public DbSet<Obstacle> Obstacle { get; set; }
        public DbSet<Map> Map { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<BattleUnit> BattleUnit { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Turn> Turn { get; set; }
        public DbSet<TurnAction> TurnAction { get; set; }
    }
}

using System;
namespace Models
{
    public class Game
    {
        public long Id { get; set; }
        public long MapId { get; set; }
        public long HostId { get; set; }
        public long JoinerId { get; set; }

        public Game()
        {
        }

        public Game(long mapId)
        {
            MapId = mapId;
            HostId = 0;
            JoinerId = 0;
        }

        public Game(long mapId, long hostId, long joinerId)
        {
            MapId = mapId;
            HostId = hostId;
            JoinerId = joinerId;
        }
    }
}

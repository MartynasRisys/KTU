using System;

namespace Models
{
    public class GameRoom
    {
        public enum MapSizeEnum { Small = 0, Medium = 1, Large = 2 };

        public long Id { get; set; }
        public string Name { get; set; }
        public long UserHostId { get; set; }
        public long UserJoinerId { get; set; }
        public int StartingGold { get; set; }
        public MapSizeEnum MapSize { get; set; }

        public GameRoom()
        {
        }

        public GameRoom(string name, long hostId, int startingGold, MapSizeEnum mapSize)
        {
            Name = name;
            UserHostId = hostId;
            UserJoinerId = 0;
            StartingGold = startingGold;
            MapSize = mapSize;
        }

        public override string ToString()
        {
            return "Name: " + Name;
        }
    }
}

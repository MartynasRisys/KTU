using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Obstacle
    {
        public long Id { get; set; }
        public long MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Obstacle()
        {

        }

        public Obstacle(long mapId, int x, int y)
        {
            MapId = mapId;
            X = x;
            Y = y;
        }
    }
}

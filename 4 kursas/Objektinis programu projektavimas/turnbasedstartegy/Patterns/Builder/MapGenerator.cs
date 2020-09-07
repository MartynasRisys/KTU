using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Patterns.Builder
{
    public class MapGenerator
    {
            // Builder uses a complex series of steps
        public void Construct(MapBuilder mapBuilder)
        {
            mapBuilder.BuildMapDimensions();
            mapBuilder.BuildObstacles();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Views;

namespace GameServer.Patterns.Builder
{
    public abstract class MapBuilder
    {
        protected MapView map;
        public abstract void BuildMapDimensions();
        public abstract void BuildObstacles();

        public MapView Map
        {
            get { return map; }
        }
    }
}

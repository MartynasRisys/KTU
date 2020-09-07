using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Views;

namespace GameServer.Patterns.Builder
{
    public class LargeMapBuilder : MapBuilder
    {
        public LargeMapBuilder()
        {
            map = new MapView();
        }

        public override void BuildMapDimensions()
        {
            map.setDimensions(75);
        }

        public override void BuildObstacles()
        {
            List<ObstacleView> obstacles = new List<ObstacleView>();
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                int obstacleX = rand.Next(0, 75);
                int obstacleY = rand.Next(0, 75);

                map.addObstacle(new ObstacleView(obstacleX, obstacleY));

            }
        }
    }
}

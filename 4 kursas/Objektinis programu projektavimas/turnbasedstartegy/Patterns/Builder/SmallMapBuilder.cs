using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Views;

namespace GameServer.Patterns.Builder
{
    public class SmallMapBuilder : MapBuilder
    {
        public SmallMapBuilder()
        {
            map = new MapView();
        }

        public override void BuildMapDimensions()
        {
            map.setDimensions(25);
        }

        public override void BuildObstacles()
        {
            List<ObstacleView> obstacles = new List<ObstacleView>();
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int obstacleX = rand.Next(0, 25);
                int obstacleY = rand.Next(0, 25);

                map.addObstacle(new ObstacleView(obstacleX, obstacleY));

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Views;

namespace GameServer.Patterns.Builder
{
    public class MediumMapBuilder : MapBuilder
    {
        public MediumMapBuilder()
        {
            map = new MapView();
        }

        public override void BuildMapDimensions()
        {
            map.setDimensions(50);
        }

        public override void BuildObstacles()
        {
            List<ObstacleView> obstacles = new List<ObstacleView>();
            Random rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                int obstacleX = rand.Next(0, 50);
                int obstacleY = rand.Next(0, 50);

                map.addObstacle(new ObstacleView(obstacleX, obstacleY));

            }
        }
    }
}

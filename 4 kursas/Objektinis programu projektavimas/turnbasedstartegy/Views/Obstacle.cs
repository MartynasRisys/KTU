using System;

namespace Views
{
    public class ObstacleView : IMapUnitView
    {
        public int X { get; set; }
        public int Y { get; set; }
        public MapUnitTypeEnum Type { get; set; }

        public ObstacleView()
        {
            Type = MapUnitTypeEnum.Obstacle;
        }

        public ObstacleView(int x, int y)
        {
            Type = MapUnitTypeEnum.Obstacle;
            X = x;
            Y = y;
        }
    }
}

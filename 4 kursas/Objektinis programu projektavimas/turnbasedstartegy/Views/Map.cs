using System;
using System.Collections.Generic;
using System.Drawing;

namespace Views
{
    public class MapView
    {
        public IMapUnitView[,] MapMatrix { get; set; }
        public Point selectedField { get; set; }
        public Point selectedFieldAdditional { get; set; }
        public bool usingSecondSelect { get; set; }

        public MapView()
        {
            selectedField = new Point(-1, -1);
        }

        public MapView(int size)
        {
            MapMatrix = new IMapUnitView[size, size];
            selectedField = new Point(-1, -1);
            selectedFieldAdditional = new Point(-1, -1);
        }

        public void setDimensions (int size)
        {
            MapMatrix = new IMapUnitView[size, size];
        }

        public void setSelectedField(Point point)
        {
            selectedField = point;
        }

        public void resetSelectedField()
        {
            selectedField = new Point(-1, -1);
        }

        public void setSelectedFieldAdditional(Point point)
        {
            selectedFieldAdditional = point;
        }

        public void resetSelectedFieldAdditional()
        {
            selectedFieldAdditional = new Point(-1, -1);
        }

        public int getSize()
        {
            return MapMatrix.GetLength(0);
            
        }

        public IMapUnitView getMapUnit(Point point)
        {
            return MapMatrix[point.X, point.Y];
        }

        public void setMapUnit (Point point, IMapUnitView unit)
        {
            MapMatrix[point.X, point.Y] = unit;
        }

        public IMapUnitView getSelectedMapUnit()
        {
                return MapMatrix[selectedField.X, selectedField.Y];
        }

        public IMapUnitView getSelectedAdditionalMapUnit()
        {
            return MapMatrix[selectedFieldAdditional.X, selectedFieldAdditional.Y];
        }

        public void addObstacle(ObstacleView obs)
        {
            MapMatrix[obs.X, obs.Y] = obs;
        }

        public void addBattleUnit(BattleUnitView battleUnit)
        {
            MapMatrix[battleUnit.X, battleUnit.Y] = battleUnit;
        }

        public void addObstacles(List<ObstacleView> obstacles)
        {
            foreach(ObstacleView obs in obstacles)
            {
                MapMatrix[obs.X, obs.Y] = obs;
            }
        }

    }
}

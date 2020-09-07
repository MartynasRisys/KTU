using System;
using System.Collections.Generic;
using System.Text;

namespace Views
{
    public enum MapUnitTypeEnum { Obstacle = 0, BattleUnit = 1 };

    public interface IMapUnitView
    {
        int X { get; set; }
        int Y { get; set; }
        MapUnitTypeEnum Type { get; set; }

    }
}

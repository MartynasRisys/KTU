using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Views;

namespace GameServer.Patterns.Factory
{
    public class BattleUnitFactory : Factory
    {
        public override BattleUnitView createBattleUnit(int index, long mapId, long playerId, int x, int y)
        {
            switch (index)
            {
                case 0:
                    return new SquareView(playerId, x, y);
                case 1:
                    return new TriangleView(playerId, x, y);
                case 2:
                    return new CircleView(playerId, x, y);
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Views;

namespace GameServer.Patterns.Factory
{
    public abstract class Factory
    {
        public abstract BattleUnitView createBattleUnit(int index, long mapId, long playerId, int x, int y);
    }
}

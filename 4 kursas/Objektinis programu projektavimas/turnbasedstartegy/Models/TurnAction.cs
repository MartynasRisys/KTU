using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public enum TurnActionTypesEnum { None = -1, Move = 0, Attack = 1 };
    public enum States { Waiting = 0, Moved = 1, UsedAction = 2, EndedTurn = 3 };

    public class TurnAction
    {

        public long Id { get; set; }
        public long TurnId { get; set; }
        public long GameId { get; set; }
        public long PlayerId { get; set; }
        public TurnActionTypesEnum TurnActionType { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public TurnAction()
        {
        }

        public TurnAction(long turnId, long playerId, long gameId, int typeIndex, int x1, int y1, int x2, int y2)
        {
            TurnId = turnId;
            PlayerId = playerId;
            GameId = gameId;
            switch (typeIndex)
            {
                case 0:
                    TurnActionType = TurnActionTypesEnum.Move;
                    break;
                case 1:
                    TurnActionType = TurnActionTypesEnum.Attack;
                    break;
            }
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
    }
}

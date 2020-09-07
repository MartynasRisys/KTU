using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Models;

namespace Patterns.ItteratorPattern
{
    public class GameRoomAggregate : Aggregate
    {
        private List<GameRoom> _items = new List<GameRoom>();

        public GameRoomAggregate(GameRoom[] array)
        {
            _items = new List<GameRoom>(array);
        }

        public override Iterator CreateIterator()
        {
            return new GameRoomIterator(this);
        }

        // Gets item count

        public int Count
        {
            get { return _items.Count; }
        }

        // Indexer

        public GameRoom this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }

}

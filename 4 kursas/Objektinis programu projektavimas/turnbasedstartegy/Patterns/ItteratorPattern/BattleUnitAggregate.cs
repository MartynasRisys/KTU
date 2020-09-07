using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Models;

namespace Patterns.ItteratorPattern
{
    public class BattleUnitAggregate : Aggregate
    {
        private List<BattleUnit> _items = new List<BattleUnit>();

        public BattleUnitAggregate(BattleUnit[] array)
        {
            _items = new List<BattleUnit>(array);
        }

        public override Iterator CreateIterator()
        {
            return new BattleUnitIterator(this);
        }

        // Gets item count

        public int Count
        {
            get { return _items.Count; }
        }

        // Indexer

        public BattleUnit this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }

}

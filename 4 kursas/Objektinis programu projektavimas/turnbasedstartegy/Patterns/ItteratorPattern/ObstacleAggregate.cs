using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Models;

namespace Patterns.ItteratorPattern
{
    public class ObstacleAggregate : Aggregate
    {
        private List<Obstacle> _items = new List<Obstacle>();

        public ObstacleAggregate(Obstacle[] array)
        {
            _items = new List<Obstacle>(array);
        }

        public override Iterator CreateIterator()
        {
            return new ObstacleIterator(this);
        }

        // Gets item count

        public int Count
        {
            get { return _items.Count; }
        }

        // Indexer

        public Obstacle this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }

}

using System;
using System.Drawing;
namespace Models
{
    public class Map
    {
        public long Id { get; set; }
        public int Size { get; set; }

        public Map()
        {
        }

        public Map(int size)
        {
            Size = size;
        }
    }
}

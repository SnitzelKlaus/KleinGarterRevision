using System;
using System.Collections.Generic;
using System.Text;

namespace KleinGarterRevision
{
    public class GameObject
    {
        public GameObject(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}

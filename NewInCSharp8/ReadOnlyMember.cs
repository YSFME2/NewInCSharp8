using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewInCSharp8
{
    struct Numbers
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int z;
        public int Z
        {
            readonly get
            {
                Change();
                return z;
            }
            set { z = value; }
        }
        public readonly int GetSum()
        {
            Change();
            return X + Y;
        }
        public void Change()
        {
            X = 0;
            Y = 0;
        }
    }
}

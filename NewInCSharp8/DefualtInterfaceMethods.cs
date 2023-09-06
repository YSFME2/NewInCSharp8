using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewInCSharp8
{
    interface ICar
    {
        string Name { get; }
        int TopSpeed => 130;
    }

    class CarA : ICar
    {
        public string Name { get; set; }
    }

    class CarB : ICar
    {
        public string Name { get; set; }
        public int TopSpeed => 150;
    }
}

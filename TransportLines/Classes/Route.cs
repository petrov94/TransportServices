using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportLines.Classes
{
    class Route
    {
        public int ID { get; set; }
        public int ID_Company { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public System.DateTime Timeof { get; set; }
        public byte Freespaces { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}

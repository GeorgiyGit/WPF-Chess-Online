using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.HelpElems
{
    [DataContract]
    public struct PointInt
    {
        [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }

        public PointInt(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}

using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.GameElems
{
    [DataContract]
    public class Step
    {
        [DataMember]
        public PieceType Type { get; }

        [DataMember]
        public PieceColors Color { get; }

        [DataMember]
        public PointInt CurrentPoint { get; }

        [DataMember]
        public PointInt NextPoint { get; }

        public Step(PieceType type, PieceColors color,PointInt currentPoint, PointInt nextPoint)
        {
            Type = type;
            Color = color;
            CurrentPoint = currentPoint;
            NextPoint = nextPoint;
        }
    }
}

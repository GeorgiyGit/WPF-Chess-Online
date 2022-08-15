using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.GameElems
{
    public class RoqueMsg
    {

        [DataMember]
        public Step Step { get; set; }
        public RoqueMsg(Step step)
        {
            Step = step;
        }
    }
}

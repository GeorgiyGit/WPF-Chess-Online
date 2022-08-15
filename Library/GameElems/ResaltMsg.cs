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
    public enum ResaltType
    {
        AttackMove,
        Roque,
        LastPawn
    }

    [DataContract]
    public class ResaltMsg
    {
        [DataMember]
        public bool CanMove { get; }

        [DataMember]
        public Step Step { get; }

        [DataMember]
        public GameState GameState { get; }

        [DataMember]
        public ResaltType Type { get; }
        public ResaltMsg(bool canMove, Step step,GameState gameState, ResaltType type)
        {
            CanMove = canMove;
            GameState = gameState;
            Step = step;
            Type = type;
        }
    }
}

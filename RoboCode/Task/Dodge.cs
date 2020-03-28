using System;
using System.Collections.Generic;
using System.Text;

namespace GHRoboRepo
{
    public class Dodge : BTNode
    {
        public Dodge(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {

            return BTNodeStatus.Succes;
        }
    }
}

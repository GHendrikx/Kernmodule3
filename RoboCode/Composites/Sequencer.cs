using System;
using System.Collections.Generic;
using System.Text;
using GHRoboRepo;
using Robocode;

namespace GHRoboRepo
{

    public class Sequencer : BTNode
    {
        private BTNode[] inputNodes;
        public Sequencer(BlackBoard blackBoard, params BTNode[] input)
        {
            this.blackBoard = blackBoard;
            inputNodes = input;
        }

        public override BTNodeStatus Tick()
        {
            foreach(BTNode node in inputNodes)
            {
                BTNodeStatus status = node.Tick();

                switch (status)
                {
                    case BTNodeStatus.Failed:
                    case BTNodeStatus.Running:
                        return status;
                    case BTNodeStatus.Succes:
                        continue;      
                }
            }
            return BTNodeStatus.Succes;
        }
    }
}

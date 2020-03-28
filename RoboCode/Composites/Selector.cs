using System;
using System.Collections.Generic;
using System.Text;
using GHRoboRepo;
using Robocode;

namespace GHRoboRepo
{
    /// <summary>
    /// The Selector runs all input nodes and returns success if an input nodes returns success.
    /// selector returned succes en dan is die klaar.
    /// </summary>
    public class Selector : BTNode
    {
        private BTNode[] inputNodes;
        public Selector(BlackBoard blackBoard, params BTNode[] input)
        {
            this.blackBoard = blackBoard;
            inputNodes = input;
        }

        public override BTNodeStatus Tick()
        {
            foreach (BTNode node in inputNodes)
            {
                BTNodeStatus status = node.Tick();
                switch (status)
                {
                    case BTNodeStatus.Failed:
                        continue;
                    case BTNodeStatus.Running:
                    case BTNodeStatus.Succes:
                        return status;
                }
            }
            return BTNodeStatus.Succes;
        }
    }
}

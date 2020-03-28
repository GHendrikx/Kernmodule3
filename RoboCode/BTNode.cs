using System;
using System.Collections.Generic;
using System.Text;

namespace GHRoboRepo
{
    public enum BTNodeStatus
    {
        Failed,
        Running,
        Succes
    }

    public abstract class BTNode
    {
        /// <summary>
        /// Basic information the BT need.
        /// </summary>
        protected BlackBoard blackBoard;

        /// <summary>
        /// Implement in the BT using for the Behaviour of the robot.
        /// When this isn't imported the BT Node wouldn't execute at all.
        /// the BT will freeze at that moment.
        /// </summary>
        /// <returns>
        /// Succes: Go to the next BTNode in the Sequencer
        /// Running: BTNode still running check the next frame.
        /// Failed: BTNode is failed and the BT stops running.
        /// </returns>
        public abstract BTNodeStatus Tick(); 

    }
}

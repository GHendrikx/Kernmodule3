using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GHRoboRepo
{
    public class Turn : BTNode
    {
        public Turn(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            blackBoard.Robot.SetColors(Color.Blue, Color.Blue, Color.Blue);

            if (blackBoard.LastScannedRobotEvent != null)
            {
                double rotation = blackBoard.LastScannedRobotEvent.Bearing;
                if (rotation <= 180)
                    blackBoard.Robot.SetTurnRight(rotation);
                else
                    blackBoard.Robot.SetTurnLeft(360 - rotation);

                return BTNodeStatus.Succes;

            }
            return BTNodeStatus.Running;
        }
    }
}

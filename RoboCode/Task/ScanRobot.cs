using System;
using System.Drawing;

namespace GHRoboRepo
{
    public class ScanRobot : BTNode
    {
        public ScanRobot(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;

        }

        public override BTNodeStatus Tick()
        {

            blackBoard.ScanDegree = 360;
            blackBoard.Robot.SetAllColors(Color.White);

            if (blackBoard.LastScannedRobotEvent != null)
            {
                double enemyAngle = blackBoard.Robot.Heading + blackBoard.LastScannedRobotEvent.Bearing;

                blackBoard.ScanDegree = enemyAngle - blackBoard.Robot.RadarHeading;
            }

            //blackBoard.LastScannedRobotEvent = null;
            blackBoard.Robot.TurnRadarRight(blackBoard.ScanDegree);
             
            return BTNodeStatus.Succes;
        }


    }
}

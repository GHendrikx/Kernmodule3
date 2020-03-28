using System;
using System.Drawing;

namespace GHRoboRepo
{
    public class ScanRobot : BTNode
    {
        private float scanDegrees;

        public ScanRobot(BlackBoard blackBoard, float scanDegrees)
        {
            this.blackBoard = blackBoard;
            this.scanDegrees = scanDegrees;
        }

        public override BTNodeStatus Tick()
        {

            blackBoard.scanDegree = 360;
            blackBoard.Robot.SetAllColors(Color.White);

            if (blackBoard.LastScannedRobotEvent != null)
            {
                double enemyAngle = blackBoard.Robot.Heading + blackBoard.LastScannedRobotEvent.Bearing;

                blackBoard.scanDegree = enemyAngle - blackBoard.Robot.RadarHeading;
            }

            //blackBoard.LastScannedRobotEvent = null;
            blackBoard.Robot.TurnRadarRight(blackBoard.scanDegree);
             
            return BTNodeStatus.Succes;
        }


    }
}

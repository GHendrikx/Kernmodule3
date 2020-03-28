using GHRoboRepo;
using System.Drawing;
using Robocode;

namespace GHRoboRepo
{
    class MoveAhead : BTNode
    {
        public MoveAhead(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            ScannedRobotEvent e = blackBoard.LastScannedRobotEvent;
            blackBoard.Robot.SetColors(Color.Red, Color.Red, Color.Red);

            if (e != null)
            {
                double distance = e.Distance;

                double distanceToDrive = 50;

                if (distance > 200)
                {
                    double i = blackBoard.Robot.Velocity;

                    if (i < 8)
                        i++;
                    else
                        i = 0;

                    blackBoard.Robot.MaxVelocity = i;
                    blackBoard.Robot.SetAhead(distanceToDrive);
                    blackBoard.Robot.Execute();
                }
                else
                    blackBoard.Robot.SetAhead(-distanceToDrive);
                
                return BTNodeStatus.Succes;
            }

            return BTNodeStatus.Succes;
        }
    }
}

using Robocode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GHRoboRepo
{
    public class TurnGunTowardsLastScannedRobot : BTNode
    {
        public TurnGunTowardsLastScannedRobot(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            blackBoard.Robot.SetColors(Color.Yellow, Color.Yellow, Color.Yellow);

            if (blackBoard.LastScannedRobotEvent != null)
            {

                // acceleration is change in velocity devided by time
                double velocity = blackBoard.LastScannedRobotEvent.Velocity;
                double rotationoOfEnemy = blackBoard.LastScannedRobotEvent.Heading;

                //Check where to aim when the other robot is in motion predicting
                //and calculate your own movement
                double xPos = blackBoard.Robot.X;
                double yPos = blackBoard.Robot.Y;

                //enemy Pos
                double xEnemyPos = blackBoard.EnemyPositions[0];
                double yEnemyPos = blackBoard.EnemyPositions[1];

                //enemy velocity in x and y
                double xSpeed = blackBoard.LastScannedRobotEvent.Velocity * Math.Cos(blackBoard.LastScannedRobotEvent.BearingRadians);
                double ySpeed = blackBoard.LastScannedRobotEvent.Velocity * Math.Sin(blackBoard.LastScannedRobotEvent.BearingRadians);


                //Target Position
                double xTarget = xEnemyPos;
                double yTarget = yEnemyPos;

                double bulletTravelTime = 0;
                double bulletTravelDistance = blackBoard.LastScannedRobotEvent.Distance;


                blackBoard.FirePower = 1;

                blackBoard.CanShoot = false;
                CalculateBulletTravelTime(ref bulletTravelDistance, ref bulletTravelTime, ref xTarget, ref yTarget, ref xPos, ref yPos, ref xEnemyPos, ref yEnemyPos, ref xSpeed, ref ySpeed);
                
                //calculate Precision of the Bullet
                if(!blackBoard.TargetPrediction)
                    for (int i = 1; i < 7; i++)
                    {
                        if (bulletTravelTime < 25 && bulletTravelTime > 0)
                        {
                            blackBoard.FirePower = i;
                            CalculateBulletTravelTime(ref bulletTravelDistance, ref bulletTravelTime, ref xTarget, ref yTarget, ref xPos, ref yPos, ref xEnemyPos, ref yEnemyPos, ref xSpeed, ref ySpeed);
                        }
                        else
                        {
                            blackBoard.FirePower -= 1;
                            CalculateBulletTravelTime(ref bulletTravelDistance, ref bulletTravelTime, ref xTarget, ref yTarget, ref xPos, ref yPos, ref xEnemyPos, ref yEnemyPos, ref xSpeed, ref ySpeed);
                            if(bulletTravelTime < 25 && bulletTravelTime > 0)
                                blackBoard.CanShoot = true;
                            break;
                        }
                    }

                //calculate the targetbearing with targetprediction
                double targetBearing = ((360 / (Math.PI * 2)) *
                    ((Math.PI * 0.5f) - Math.Asin((yTarget - yPos) / (bulletTravelDistance)) - blackBoard.Robot.HeadingRadians));



                if (xEnemyPos < xPos)
                {
                    targetBearing *= -1;
                    targetBearing -= 2 * blackBoard.Robot.Heading;
                }

                //calculate Rotation
                double rotation = (targetBearing -
                    (blackBoard.Robot.GunHeading - blackBoard.Robot.Heading));

                if (rotation < -180)
                    rotation += 360;
                else if (rotation >= 180)
                    rotation -= 360;

                if (rotation >= 0)
                {
                    blackBoard.Robot.TurnGunRight(rotation);
                }
                else
                {
                    if (Double.IsNaN(rotation))
                        rotation = 0;

                    blackBoard.Robot.TurnGunLeft(-rotation);
                }

                return BTNodeStatus.Succes;
            }
            else
                return BTNodeStatus.Running;
        }

        public void CalculateBulletTravelTime(ref double bulletTravelDistance, ref double bulletTravelTime, ref double xTarget, ref double yTarget, ref double xPos, ref double yPos, ref double xEnemyPos, ref double yEnemyPos, ref double xSpeed, ref double ySpeed)
        {
            for (int i = 0; i < 10; i++)
            {
                bulletTravelDistance = Math.Sqrt(Math.Pow(xTarget - xPos, 2) + Math.Pow(yTarget - yPos, 2));
                bulletTravelTime = bulletTravelDistance / (20 - 3 * blackBoard.FirePower);

                xTarget = xEnemyPos + xSpeed * bulletTravelTime;
                yTarget = yEnemyPos + ySpeed * bulletTravelTime;
            }
        }

        public int GetDistance()
        {
            if (blackBoard.LastScannedRobotEvent != null)
            {
                double distance = blackBoard.LastScannedRobotEvent.Distance;
                int firePower = 1;

                for (int i = 800; i >= 0; i -= 200)
                {
                    if (distance >= i)
                        return firePower;

                    firePower++;
                }
                return firePower;
            }
            return 0;
        }
    }
}
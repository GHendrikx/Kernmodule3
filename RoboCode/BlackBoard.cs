using Robocode;

namespace GHRoboRepo
{
    public class BlackBoard
    {
        public AdvancedRobot Robot;
        public ScannedRobotEvent LastScannedRobotEvent;
        public double scanDegree;
        public double enemyHeading;
        public int firePower;
        public bool targetPrediction;
        public bool canShoot;
        //x and y position of the enemy
        public double[] enemyPositions;
    }
}
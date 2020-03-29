using Robocode;

namespace GHRoboRepo
{
    public class BlackBoard
    {
        public AdvancedRobot Robot;
        public ScannedRobotEvent LastScannedRobotEvent;
        public double ScanDegree;
        public double EnemyHeading;
        public int FirePower;
        public bool TargetPrediction;
        public bool CanShoot;
        //x and y position of the enemy
        public double[] EnemyPositions;
    }
}
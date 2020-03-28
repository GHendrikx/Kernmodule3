using System;
using System.Collections.Generic;
using System.Drawing;
using Robocode;
using Robocode.Util;

namespace GHRoboRepo
{
    public class SUSTAIN : AdvancedRobot
    {
        public BTNode ScanTree;
        public BTNode ShootTree;
        public BTNode DriveTree;

        public List<Sequencer> sequencer;
        public BlackBoard blackBoard = new BlackBoard();

        public override void Run()
        {
            blackBoard.Robot = this;
            IsAdjustGunForRobotTurn = true;
            IsAdjustRadarForGunTurn = true;

            ScanTree = new Sequencer(blackBoard, new ScanRobot(blackBoard, 360));

            ShootTree =
                new Sequencer(blackBoard,
                new TurnGunTowardsLastScannedRobot(blackBoard),
                new Shoot(blackBoard)
                );

            DriveTree =
                new Sequencer(blackBoard,
                new Turn(blackBoard),
                new MoveAhead(blackBoard));

            while (true)
            {
                ScanTree.Tick();
                ShootTree.Tick();
                blackBoard.targetPrediction = !blackBoard.targetPrediction;
                DriveTree.Tick();
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            base.OnScannedRobot(evnt);
            blackBoard.LastScannedRobotEvent = evnt;
            GetPositionOfEnemy(evnt);

        }


        public void GetPositionOfEnemy(ScannedRobotEvent e)
        {

            double angle = ConvertToRadians(blackBoard.Robot.Heading + e.Bearing % 360);
            double scannedX = (blackBoard.Robot.X + Math.Sin(angle) * e.Distance);
            double scannedY = (blackBoard.Robot.Y + Math.Cos(angle) * e.Distance);

            double[] positions = new double[2] { scannedX, scannedY };

            blackBoard.enemyPositions = positions;
        }

        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public override void OnWin(WinEvent evnt)
        {
            base.OnWin(evnt);
            blackBoard.Robot.SetAllColors(Color.OrangeRed);
        }

    }
}

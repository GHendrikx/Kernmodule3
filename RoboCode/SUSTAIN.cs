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

        public List<Sequencer> Sequencer;
        public BlackBoard BlackBoard = new BlackBoard();

        public override void Run()
        {
            BlackBoard.Robot = this;
            IsAdjustGunForRobotTurn = true;
            IsAdjustRadarForGunTurn = true;

            ScanTree = new Sequencer(BlackBoard, new ScanRobot(BlackBoard));

            ShootTree =
                new Sequencer(BlackBoard,
                new TurnGunTowardsLastScannedRobot(BlackBoard),
                new Shoot(BlackBoard)
                );

            DriveTree =
                new Sequencer(BlackBoard,
                new Turn(BlackBoard),
                new MoveAhead(BlackBoard));

            while (true)
            {
                ScanTree.Tick();
                ShootTree.Tick();
                BlackBoard.TargetPrediction = !BlackBoard.TargetPrediction;
                DriveTree.Tick();
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            base.OnScannedRobot(evnt);
            BlackBoard.LastScannedRobotEvent = evnt;
            GetPositionOfEnemy(evnt);

        }


        public void GetPositionOfEnemy(ScannedRobotEvent e)
        {

            double angle = ConvertToRadians(BlackBoard.Robot.Heading + e.Bearing % 360);
            double scannedX = (BlackBoard.Robot.X + Math.Sin(angle) * e.Distance);
            double scannedY = (BlackBoard.Robot.Y + Math.Cos(angle) * e.Distance);

            double[] positions = new double[2] { scannedX, scannedY };

            BlackBoard.EnemyPositions = positions;
        }

        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public override void OnWin(WinEvent evnt)
        {
            base.OnWin(evnt);
            BlackBoard.Robot.SetAllColors(Color.OrangeRed);
        }

    }
}

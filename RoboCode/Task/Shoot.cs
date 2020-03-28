using Robocode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GHRoboRepo
{
    public class Shoot : BTNode
    {
        private float power;
        public Shoot(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            blackBoard.Robot.SetColors(Color.Black, Color.Black, Color.Black);
            if (blackBoard.canShoot)
                blackBoard.Robot.Fire(blackBoard.firePower);
            return BTNodeStatus.Succes;
        }

        
    }
}

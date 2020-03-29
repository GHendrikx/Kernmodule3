using Robocode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GHRoboRepo
{
    public class Shoot : BTNode
    {
        public Shoot(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            blackBoard.Robot.SetColors(Color.Black, Color.Black, Color.Black);
            if (blackBoard.CanShoot)
                blackBoard.Robot.Fire(blackBoard.FirePower);
            return BTNodeStatus.Succes;
        }

        
    }
}

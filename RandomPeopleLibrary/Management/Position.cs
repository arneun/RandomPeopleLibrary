using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.Management
{
    public class Position
    {

        public Position(double posX, double posY) {
            PosX = posX;
            PosY = posY;
        }

        public double PosX { get; }
        public double PosY { get; }
    }
}

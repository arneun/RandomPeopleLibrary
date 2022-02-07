using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.Example
{
    public class Position
    {

        public Position(float posX, float posY) {
            PosX = posX;
            PosY = posY;
        }

        public float PosX { get; }
        public float PosY { get; }
    }
}

using RandomPeopleLibrary.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomPeopleLibrary.Example
{
    public class Target : IExampleTarget
    {
        public Position ObjectPosition { get; }

        public Target(double posX, double posY) 
        {
            this.ObjectPosition = new Position(posX, posY);
        }

    }
}

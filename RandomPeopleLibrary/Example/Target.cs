﻿using RandomPeopleLibrary.Management;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomPeopleLibrary.Example
{
    public class Target : ITarget
    {
        public Position ObjectPosition { get; }

        public Target(float posX, float posY) 
        {
            this.ObjectPosition = new Position(posX, posY);
        }

        public float GetActivityTime()
        {
            /// always 10 for example purposes
            return 10;
        }
    }
}

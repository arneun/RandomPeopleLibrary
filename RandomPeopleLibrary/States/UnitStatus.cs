using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomPeopleLibrary.States
{
    public class UnitStatus
    {
        public UnitState state;

        public ITarget target;

        public double TimeLeft;

    }

    public enum UnitState 
    {
        Moving,
        Busy,
        Thinking,
        Leaving
    }
}

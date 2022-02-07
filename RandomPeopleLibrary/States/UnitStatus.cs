using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomPeopleLibrary.States
{
    public class UnitStatus<Target, Need> where Target : ITarget where Need : Enum
    {
        public UnitState state;

        public Target target;

        public float TimeLeft;

        public Dictionary<Need, float> Needs;

    }

    public enum UnitState 
    {
        Moving,
        Busy,
        Thinking,
        Leaving
    }
}

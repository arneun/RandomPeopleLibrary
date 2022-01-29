using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomPeopleLibrary.States
{
    public class UnitStatus<T> where T : ITarget
    {
        public UnitState state;

        public T target;

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

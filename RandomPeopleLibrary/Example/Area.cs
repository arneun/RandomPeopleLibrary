using RandomPeopleLibrary.Example;
using RandomPeopleLibrary.Management;
using RandomPeopleLibrary.NPC;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomPeopleLibrary.Example
{
    public class Area : IArea<ITarget, NeedSatisfier, Need>
    {
        private ITarget[] availibleTargets;
        
        private IUnit<ITarget, NeedSatisfier, Need>[] simulatedUnits;

        public Area(List<ITarget> targets, Random random)
        {
            this.availibleTargets = targets.ToArray();
            this.simulatedUnits = new IUnit<ITarget, NeedSatisfier, Need>[0];
        }

        public Area(List<ITarget> targets, Random random, int unitCount)
        {
            this.availibleTargets = targets.ToArray();
            this.simulatedUnits =  Enumerable.Range(1, unitCount).Select(x => new Unit(this, 1)).ToArray();
        }

        public IList<ITarget> PointsOfInterest => availibleTargets;
    }
}

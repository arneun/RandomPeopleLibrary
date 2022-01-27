using RandomPeopleLibrary.NPC;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomPeopleLibrary.Management
{
    public class Area : IArea
    {
        private ITarget[] availibleTargets;
        
        private IUnit[] simulatedUnits;

        public Area(List<ITarget> targets, Random random)
        {
            this.availibleTargets = targets.ToArray();
            this.simulatedUnits = new IUnit[0];
        }

        public Area(List<ITarget> targets, Random random, int unitCount)
        {
            this.availibleTargets = targets.ToArray();
            this.simulatedUnits =  Enumerable.Range(1, unitCount).Select(x => new Unit(this, 1)).ToArray();
        }

        public IList<ITarget> PointsOfInterest => availibleTargets;
    }
}

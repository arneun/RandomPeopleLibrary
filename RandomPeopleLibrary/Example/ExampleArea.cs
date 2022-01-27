using RandomPeopleLibrary.Management;
using RandomPeopleLibrary.NPC;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.Example
{
    public class ExampleArea : IExampleArea
    {
        private IExampleTarget[] availibleTargets;

        private Unit[] simulatedUnits;

        public ExampleArea(List<IExampleTarget> targets, Random random)
        {
            this.availibleTargets = targets.ToArray();
            this.simulatedUnits = new Unit[0];
        }

        public ExampleArea(List<IExampleTarget> targets, Random random, int unitCount)
        {
            this.availibleTargets = targets.ToArray();
            this.simulatedUnits = Enumerable.Range(1, unitCount).Select(x => new Unit(this, 1)).ToArray();
        }

        public IList<IExampleTarget> PointsOfInterest => throw new NotImplementedException();
    }
}

using RandomPeopleLibrary.NPC.Needs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.Example
{
    public class NeedSatisfier : INeedSatisfier<Need>
    {
        public float GetSatisfactionRate()
        {
            throw new NotImplementedException();
        }
    }
}

using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.NPC.Needs
{
    public interface INeedSatisfier<Need> where Need : Enum
    {
        float GetSatisfactionRate();
    }
}

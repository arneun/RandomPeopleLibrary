using RandomPeopleLibrary.NPC.Needs;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomPeopleLibrary.Management
{
    public interface IArea<Target, NeedSatisfier, Need> 
        where Target : ITarget 
        where Need : Enum 
        where NeedSatisfier : INeedSatisfier<Need> 
    {
        IList<Target> PointsOfInterest { get; } 
    }
}

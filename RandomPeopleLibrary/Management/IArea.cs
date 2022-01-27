using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomPeopleLibrary.Management
{
    public interface IArea 
    {
        IList<ITarget> PointsOfInterest { get; } 
    }
}

using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomPeopleLibrary.Management
{
    public interface IArea<T> where T : ITarget 
    {
        IList<T> PointsOfInterest { get; } 
    }
}

using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.Management
{
    public interface IBasicArea<T> where T : ITarget
    {
        IList<T> PointsOfInterest { get; }
    }
}

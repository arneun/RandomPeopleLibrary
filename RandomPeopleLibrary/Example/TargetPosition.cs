using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.Example
{
    internal static class TargetPosition
    {
        public static Position GetPosition(this ITarget target) 
        {
            var targetcast = target as Target;
            return targetcast.ObjectPosition;
        } 
    }
}

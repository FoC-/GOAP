using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    public class StateComaparer : IEqualityComparer<State>
    {
        public bool Equals(State left, State right)
        {
            var leftParameters = left.GetAll().ToList();
            var rightParameters = right.GetAll().ToList();

            if (!leftParameters.Any() && !rightParameters.Any()) return true;
            return leftParameters.Count() == rightParameters.Count() && leftParameters.All(l => rightParameters.Any(r => l.Equals(r)));
        }

        public int GetHashCode(State obj)
        {
            var hash = 127;

            foreach (var pair in obj.GetAll().OrderBy(x => x.Id))
            {
                hash ^= pair.Id.GetHashCode();
                hash ^= pair.Count.GetHashCode();
            }

            return hash;
        }
    }
}
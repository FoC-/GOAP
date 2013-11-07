using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    public class StateComaparer : IEqualityComparer<IEnumerable<Parameter>>
    {
        public bool Equals(IEnumerable<Parameter> left, IEnumerable<Parameter> right)
        {

            if (!left.Any() && !right.Any()) return true;
            return left.Count() == right.Count() && left.All(l => right.Any(r => l.Equals(r)));
        }

        public int GetHashCode(IEnumerable<Parameter> obj)
        {
            var hash = 127;

            foreach (var pair in obj)
            {
                hash ^= pair.Name.GetHashCode();
                hash ^= pair.Count.GetHashCode();
            }

            return hash;
        }

        public double Distance(IEnumerable<Parameter> source, IEnumerable<Parameter> destination)
        {
            var score = 0.0;
            var requiredParameters = destination.Where(x => x.IsRequiredForGoal);
            foreach (var requiredParameter in requiredParameters)
            {
                var existingParameter = source.SingleOrDefault(p => p.Name == requiredParameter.Name);
                if (existingParameter == null) continue;
                if (existingParameter.IsRequiredExectCount && existingParameter.Count == requiredParameter.Count)
                {
                    score += 1;
                }
                else
                {
                    score += (double)requiredParameter.Count / existingParameter.Count;
                }
            }
            return score / destination.Count(x => x.IsRequiredForGoal);
        }
    }
}
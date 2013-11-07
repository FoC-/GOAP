using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    public class StateComaparer : IEqualityComparer<IEnumerable<Parameter>>
    {
        public bool Equals(IEnumerable<Parameter> left, IEnumerable<Parameter> right)
        {
            if (!left.Any() && !right.Any()) return true;
            return left.Count() == right.Count() && left.All(l => right.Any(r => l.Name == r.Name && l.Count == r.Count));
        }

        public int GetHashCode(IEnumerable<Parameter> state)
        {
            var parameters = state.OrderBy(x => x.Name).ToList();
            var hash = 127;
            foreach (var parameter in parameters)
            {
                hash ^= parameter.ToString().GetHashCode();
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
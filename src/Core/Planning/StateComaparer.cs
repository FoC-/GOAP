using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    public class StateComaparer : IEqualityComparer<Dictionary<string, int>>
    {
        public bool Equals(Dictionary<string, int> x, Dictionary<string, int> y)
        {
            return x.Count == y.Count && !x.Except(y).Any();
        }

        public int GetHashCode(Dictionary<string, int> state)
        {
            var hash = 127;
            foreach (var parameter in state)
            {
                hash ^= parameter.Key.GetHashCode();
                hash ^= parameter.Value.GetHashCode();
            }
            return hash;
        }

        public double Distance(Dictionary<string, int> x, Dictionary<string, int> y)
        {
            var score = 0.0;
            foreach (var requiredParameter in y)
            {
                int existingParameter = 0;
                x.TryGetValue(requiredParameter.Key, out existingParameter);
                if (existingParameter == 0) continue;
                if (existingParameter == requiredParameter.Value)
                {
                    score += 1;
                }
                else
                {
                    score += (double)requiredParameter.Value / existingParameter;
                }
            }
            return score / y.Count;
        }
    }
}
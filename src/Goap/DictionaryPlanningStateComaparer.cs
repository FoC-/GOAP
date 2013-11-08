using System.Linq;
using GOAP.Planning;

namespace GOAP
{
    public class DictionaryPlanningStateComaparer : IPlanningStateComparer<DictionaryState>
    {
        public bool Equals(DictionaryState x, DictionaryState y)
        {
            return x.Count == y.Count && !x.Except(y).Any();
        }

        public int GetHashCode(DictionaryState state)
        {
            var hash = 127;
            foreach (var parameter in state)
            {
                hash ^= parameter.Key.GetHashCode();
                hash ^= parameter.Value.GetHashCode();
            }
            return hash;
        }

        public double Distance(DictionaryState x, DictionaryState y)
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
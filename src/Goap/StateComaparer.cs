using System;
using System.Linq;
using GOAP.Planning;

namespace GOAP
{
    public class StateComaparer : IPlanningStateComparer<State>
    {
        public bool Equals(State x, State y)
        {
            return x.Count == y.Count && !x.Except(y).Any();
        }

        public int GetHashCode(State state)
        {
            var hash = 127;
            foreach (var parameter in state)
            {
                hash ^= parameter.Key.GetHashCode();
                hash ^= parameter.Value.GetHashCode();
            }
            return hash;
        }

        public double Distance(State state1, State state2)
        {
            var score = 0.0;
            foreach (var state2param in state2)
            {
                var state1Value = 0;
                var state2Value = state2param.Value;
                state1.TryGetValue(state2param.Key, out state1Value);
                score += Math.Abs(state1Value - state2Value);
            }
            return score / Math.Max(state1.Count, state2.Count);
        }
    }
}
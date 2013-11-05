using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    public class State<T> : ICloneable, IEquatable<State<T>>
    {
        private readonly Dictionary<T, int> parameters = new Dictionary<T, int>();
        private PlanningAction<T> createdBy;

        public void Add(Dictionary<T, int> items)
        {
            foreach (var item in items)
            {
                if (parameters.ContainsKey(item.Key))
                    parameters[item.Key] = parameters[item.Key] + item.Value;
                else
                    parameters.Add(item.Key, item.Value);
            }
        }

        public void Remove(Dictionary<T, int> items)
        {
            foreach (var item in items)
            {
                if (parameters.ContainsKey(item.Key))
                    parameters[item.Key] = parameters[item.Key] - item.Value;
            }
        }

        public bool Has(T item, int count)
        {
            return parameters.ContainsKey(item) && parameters[item] == count;
        }

        public int Count(T item)
        {
            return parameters.ContainsKey(item) ? parameters[item] : 0;
        }

        public int ParametersCount()
        {
            return parameters.Count;
        }

        public double Distance(State<T> destination)
        {
            double score = 0.0;
            foreach (var parameter in parameters)
            {
                double needed = destination.Count(parameter.Key);
                double exist = parameter.Value;
                if (needed > exist) needed = exist;
                score += needed / exist;
            }
            return score / parameters.Count;
        }

        public void CreatedBy(PlanningAction<T> planningAction)
        {
            createdBy = planningAction;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(State<T>) && Equals((State<T>)obj);
        }

        public bool Equals(State<T> other)
        {
            if (this.ParametersCount() != other.ParametersCount()) return false;
            return parameters.All(p => other.Count(p.Key) == p.Value);
        }
        public override int GetHashCode()
        {
            int iHash = 127;

            foreach (var pair in parameters)
            {
                iHash ^= pair.Key.GetHashCode();
                iHash ^= pair.Value.GetHashCode();
            }

            return iHash;
        }

        public object Clone()
        {
            var newState = new State<T>();
            newState.Add(parameters);
            return newState;
        }
    }
}

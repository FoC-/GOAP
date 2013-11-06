﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    public class State : ICloneable, IEquatable<State>
    {
        private readonly Dictionary<string, Parameter> parameters = new Dictionary<string, Parameter>();

        public string CreatedBy { get; set; }

        public IEnumerable<Parameter> GetAll()
        {
            return parameters.Values;
        }

        public Parameter Get(string id)
        {
            Parameter value;
            parameters.TryGetValue(id, out value);
            return value;
        }

        public void Save(Parameter parameter)
        {
            Parameter value;
            if (parameters.TryGetValue(parameter.Id, out value))
            {
                parameters.Remove(parameter.Id);
            }
            parameters.Add(parameter.Id, parameter);
        }

        public void Remove(string id)
        {
            parameters.Remove(id);
        }

        public double Distance(State destination)
        {
            var score = 0.0;
            var requiredParameters = destination.GetAll().Where(x => x.IsRequiredForGoal);
            foreach (var requiredParameter in requiredParameters)
            {
                var existingParameter = Get(requiredParameter.Id);
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
            return score / destination.GetAll().Count(x => x.IsRequiredForGoal);
        }

        public object Clone()
        {
            var newState = new State();
            foreach (var parameter in GetAll())
            {
                newState.Save(parameter);
            }
            return newState;
        }

        public bool Equals(State other)
        {
            var otherParameters = other.GetAll().Where(x => x.IsRequiredForGoal);
            if (!otherParameters.Any()) return false;
            foreach (var otherParameter in otherParameters)
            {
                var parameter = Get(otherParameter.Id);
                if (parameter == null) return false;
                if (parameter.IsRequiredExectCount && parameter.Count != otherParameter.Count) return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(State) && Equals((State)obj);
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
    }

    public class Parameter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsRequiredForGoal { get; set; }
        //Todo: Make it enum less more equal
        public bool IsRequiredExectCount { get; set; }
        public int Count { get; set; }
    }
}

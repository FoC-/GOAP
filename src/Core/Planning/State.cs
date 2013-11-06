using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    [Serializable]
    public class State
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
    }
}

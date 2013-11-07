using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    public class PlanningAction
    {
        public string Name { get; private set; }

        private readonly Func<Dictionary<string, int>, bool> validator;
        private readonly Action<Dictionary<string, int>> executor;

        public PlanningAction(string name, Func<Dictionary<string, int>, bool> validator, Action<Dictionary<string, int>> executor)
        {
            Name = name;
            this.validator = validator;
            this.executor = executor;
        }

        public bool CanExecute(Dictionary<string, int> state)
        {
            return validator(state);
        }

        public Dictionary<string, int> Execute(Dictionary<string, int> state)
        {
            var newState = state.ToDictionary(x => x.Key, x => x.Value);
            executor(newState);
            return newState;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
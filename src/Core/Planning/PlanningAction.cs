using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    public class PlanningAction
    {
        public string Name { get; private set; }

        private readonly Func<IEnumerable<Parameter>, bool> validator;
        private readonly Action<IEnumerable<Parameter>> executor;

        public PlanningAction(string name, Func<IEnumerable<Parameter>, bool> validator, Action<IEnumerable<Parameter>> executor)
        {
            Name = name;
            this.validator = validator;
            this.executor = executor;
        }

        public bool CanExecute(IEnumerable<Parameter> state)
        {
            return validator(state);
        }

        public IEnumerable<Parameter> Execute(IEnumerable<Parameter> state)
        {
            var newState = state.Select(o => o.ShallowCopy()).ToList();
            executor(newState);
            return newState;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
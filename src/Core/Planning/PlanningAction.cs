using System;
using Core.Extensions;

namespace Core.Planning
{
    public class PlanningAction
    {
        public string Name { get; private set; }

        private readonly Func<State, bool> validator;
        private readonly Action<State> executor;
        public bool IsMultiExecutable { get; set; }

        public PlanningAction(string name, Func<State, bool> validator, Action<State> executor)
        {
            Name = name;
            this.validator = validator;
            this.executor = executor;
        }

        public bool CanExecute(State state)
        {
            return validator(state);
        }

        public State Execute(State state)
        {
            var newState = state.DeepClone();
            newState.CreatedBy = Name;
            do
            {
                executor(newState);
            } while (IsMultiExecutable && CanExecute(newState));
            return newState;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
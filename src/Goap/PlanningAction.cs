using System;
using GOAP.Planning;

namespace GOAP
{
    public class PlanningAction<T> : IPlanningAction<T> where T : ICloneable
    {
        public string Name { get; private set; }

        private readonly Predicate<T> validator;
        private readonly Action<T> executor;

        public PlanningAction(string name, Predicate<T> validator, Action<T> executor)
        {
            Name = name;
            this.validator = validator;
            this.executor = executor;
        }

        public bool CanExecute(T state)
        {
            return validator(state);
        }

        public T Execute(T state)
        {
            var newState = (T)state.Clone();
            executor(newState);
            return newState;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
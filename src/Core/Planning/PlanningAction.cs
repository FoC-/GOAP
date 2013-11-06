using System;

namespace Core.Planning
{
    public class PlanningAction<T> where T : class, IPlaningState
    {
        public string Name { get; private set; }

        private readonly Func<T, bool> validator;
        private readonly Action<T> executor;
        public bool IsMultiExecutable { get; set; }

        public PlanningAction(string name, Func<T, bool> validator, Action<T> executor)
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
            var newState = state.Clone() as T;
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Planning
{
    public class PlanningAction<T> : ICloneable
    {
        public string Name { get; private set; }
        public bool IsMultiproducer { get; private set; }

        private readonly Dictionary<T, int> requires = new Dictionary<T, int>();
        private readonly Dictionary<T, int> consumes = new Dictionary<T, int>();
        private readonly Dictionary<T, int> produces = new Dictionary<T, int>();
        private readonly List<Func<State<T>, bool>> prejudicates = new List<Func<State<T>, bool>>();
        private readonly List<Action<State<T>>> postActions = new List<Action<State<T>>>();

        public PlanningAction(string name)
        {
            Name = name;
        }

        public PlanningAction<T> MultiProducer(bool isMultiproducer)
        {
            IsMultiproducer = isMultiproducer;
            return this;
        }
        public PlanningAction<T> Requires(T item)
        {
            requires.Add(item, 0);
            return this;
        }
        public PlanningAction<T> Requires(T item, int quantity)
        {
            if (requires.ContainsKey(item))
                requires[item] = quantity;
            else
                requires.Add(item, quantity);
            return this;
        }
        public PlanningAction<T> Consumes(T item)
        {
            return Consumes(item, 1);
        }
        public PlanningAction<T> Consumes(T item, int quantity)
        {
            if (consumes.ContainsKey(item))
                consumes[item] = quantity;
            else
                consumes.Add(item, quantity);
            return this;
        }
        public PlanningAction<T> Produces(T item)
        {
            return Produces(item, 1);
        }
        public PlanningAction<T> Produces(T item, int quantity)
        {
            if (produces.ContainsKey(item))
                produces[item] = quantity;
            else
                produces.Add(item, quantity);
            return this;
        }
        public PlanningAction<T> AddPrejudicate(Func<State<T>, bool> prejudicate)
        {
            prejudicates.Add(prejudicate);
            return this;
        }
        public PlanningAction<T> AddPostAction(Action<State<T>> postAction)
        {
            postActions.Add(postAction);
            return this;
        }

        public bool CanExecute(State<T> state)
        {
            if (requires.Any(i => i.Value > state.Count(i.Key))) return false;
            if (consumes.Any(i => i.Value > state.Count(i.Key))) return false;
            if (prejudicates.Any(func => !func(state))) return false;
            return true;
        }
        public void Execute(State<T> state)
        {
            if (!CanExecute(state)) return;
            do
            {
                state.Remove(consumes);
                state.Add(produces);
                state.CreatedBy(this);
            } while (IsMultiproducer && CanExecute(state));
            postActions.ForEach(pa => pa(state));
        }
        public State<T> Migrate(State<T> state)
        {
            var newState = state.Clone() as State<T>;
            Execute(newState);
            return newState;
        }

        public object Clone()
        {
            var clone = new PlanningAction<T>(Name);
            clone.MultiProducer(IsMultiproducer);
            foreach (var require in requires)
                clone.Requires(require.Key, require.Value);
            foreach (var consume in consumes)
                clone.Consumes(consume.Key, consume.Value);
            foreach (var produce in produces)
                clone.Produces(produce.Key, produce.Value);
            foreach (var prejudicate in prejudicates)
                clone.AddPrejudicate(prejudicate);
            foreach (var postAction in postActions)
                clone.AddPostAction(postAction);
            return clone;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
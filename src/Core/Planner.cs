using System.Collections.Generic;
using System.Linq;
using Core.Graph;
using Core.Planning;
using Core.PrioritizedCollections;

namespace Core
{
    public class Planner<T>
    {
        private readonly Method method;
        private readonly IEnumerable<IPlanningAction<T>> planningActions;
        private readonly IStateComparer<T> stateComparer;

        public Planner(Method method, IEnumerable<IPlanningAction<T>> planningActions, IStateComparer<T> stateComparer)
        {
            this.method = method;
            this.planningActions = planningActions;
            this.stateComparer = stateComparer;
        }

        public IEnumerable<T> MakePlan(T initialState, T goalState)
        {
            var visitedStates = new HashSet<T>(stateComparer);
            var unvisitedStates = UnvisitedStates<Path<T>>();
            unvisitedStates.Add(0, new Path<T>(initialState));
            while (unvisitedStates.HasElements)
            {
                var path = unvisitedStates.Get();
                if (visitedStates.Contains(path.Node)) continue;
                if (stateComparer.Equals(path.Node, goalState)) return path.Reverse();

                visitedStates.Add(path.Node);

                var plans = planningActions
                    .Where(action => action.CanExecute(path.Node))
                    .Select(action => action.Execute(path.Node))
                    .Select(state => path.AddChild(state, stateComparer.Distance(state, path.Node)));

                foreach (var plan in plans)
                {
                    unvisitedStates.Add(plan.Cost + stateComparer.Distance(plan.Node, goalState), plan);
                }
            }
            return Enumerable.Empty<T>();
        }

        private IPrioritized<double, S> UnvisitedStates<S>()
        {
            IPrioritized<double, S> prioritized = null;
            switch (method)
            {
                case Method.BreadthFirst:
                    prioritized = new PrioritizedQueue<double, S>();
                    break;
                case Method.DepthFirst:
                    prioritized = new PrioritizedStack<double, S>();
                    break;
            }
            return prioritized;
        }
    }
}
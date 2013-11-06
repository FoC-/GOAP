using System.Collections.Generic;
using System.Linq;
using Core.Graph;
using Core.Planning;
using Core.PrioritizedCollections;

namespace Core
{
    public static class Planner
    {
        public static IEnumerable<S> MakePlan<S, T>(S initialState, S goalState, IEnumerable<PlanningAction<S>> planningActions, Method method) where S : State<T>
        {
            var visitedStates = new HashSet<S>();
            var unvisitedStates = UnvisitedStates<Path<S>>(method);
            unvisitedStates.Add(0, new Path<S>(initialState));
            while (unvisitedStates.HasElements)
            {
                var path = unvisitedStates.Get();
                if (visitedStates.Contains(path.Node)) continue;
                if (path.Node.Equals(goalState)) return path;

                visitedStates.Add(path.Node);

                var plans = planningActions
                    .Where(action => action.CanExecute(path.Node))
                    .Select(action => action.Execute(path.Node))
                    .Select(state => path.AddChild(state, state.Distance(path.Node)));

                foreach (var plan in plans)
                {
                    unvisitedStates.Add(plan.Cost + plan.Node.Distance(goalState), plan);
                }
            }
            return null;
        }

        private static IPrioritized<double, S> UnvisitedStates<S>(Method method)
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
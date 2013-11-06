using System.Collections.Generic;
using System.Linq;
using Core.Graph;
using Core.Planning;
using Core.PrioritizedCollections;

namespace Core
{
    public static class Planner
    {
        public static IEnumerable<State> MakePlan(State initialState, State goalState, IEnumerable<PlanningAction> planningActions, Method method)
        {
            var visitedStates = new HashSet<State>();
            var unvisitedStates = UnvisitedStates<Path<State>>(method);
            unvisitedStates.Add(0, new Path<State>(initialState));
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

        private static IPrioritized<double, T> UnvisitedStates<T>(Method method)
        {
            IPrioritized<double, T> prioritized = null;
            switch (method)
            {
                case Method.BreadthFirst:
                    prioritized = new PrioritizedQueue<double, T>();
                    break;
                case Method.DepthFirst:
                    prioritized = new PrioritizedStack<double, T>();
                    break;
            }
            return prioritized;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Core.Graph;
using Core.Planning;
using Core.PrioritizedCollections;

namespace Core
{
    public class Planner
    {
        private readonly Method method;
        private readonly IEnumerable<PlanningAction> planningActions;
        private static readonly StateComaparer Comparer = new StateComaparer();

        public Planner(Method method, IEnumerable<PlanningAction> planningActions)
        {
            this.method = method;
            this.planningActions = planningActions;
        }

        public IEnumerable<Dictionary<string, int>> MakePlan(Dictionary<string, int> initialState, Dictionary<string, int> goalState)
        {
            var visitedStates = new HashSet<Dictionary<string, int>>(Comparer);
            var unvisitedStates = UnvisitedStates<Path<Dictionary<string, int>>>();
            unvisitedStates.Add(0, new Path<Dictionary<string, int>>(initialState));
            while (unvisitedStates.HasElements)
            {
                var path = unvisitedStates.Get();
                if (visitedStates.Contains(path.Node)) continue;
                if (Comparer.Equals(path.Node, goalState)) return path.Reverse();

                visitedStates.Add(path.Node);

                var plans = planningActions
                    .Where(action => action.CanExecute(path.Node))
                    .Select(action => action.Execute(path.Node))
                    .Select(state => path.AddChild(state, Comparer.Distance(state, path.Node)));

                foreach (var plan in plans)
                {
                    unvisitedStates.Add(plan.Cost + Comparer.Distance(plan.Node, goalState), plan);
                }
            }
            return Enumerable.Empty<Dictionary<string, int>>();
        }

        private IPrioritized<double, T> UnvisitedStates<T>()
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
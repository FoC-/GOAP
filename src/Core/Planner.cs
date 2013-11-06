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

        public IEnumerable<State> MakePlan(State initialState, State goalState)
        {
            var visitedStates = new HashSet<State>(Comparer);
            var unvisitedStates = UnvisitedStates<Path<State>>();
            unvisitedStates.Add(0, new Path<State>(initialState));
            while (unvisitedStates.HasElements)
            {
                var path = unvisitedStates.Get();
                if (visitedStates.Contains(path.Node, Comparer)) continue;
                if (Comparer.Equals(path.Node, goalState)) return path.Reverse();

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
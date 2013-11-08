using System.Collections.Generic;
using System.Linq;
using GOAP.Graph;
using GOAP.Planning;
using GOAP.PrioritizedCollections;

namespace GOAP
{
    public class Planner<T> : IPlanner<T>
    {
        private readonly PlanningMethod planningMethod;
        private readonly IEnumerable<IPlanningAction<T>> planningActions;
        private readonly IPlanningStateComparer<T> planningStateComparer;

        public Planner(PlanningMethod planningMethod, IEnumerable<IPlanningAction<T>> planningActions, IPlanningStateComparer<T> planningStateComparer)
        {
            this.planningMethod = planningMethod;
            this.planningActions = planningActions;
            this.planningStateComparer = planningStateComparer;
        }

        public IEnumerable<IPlanningAction<T>> MakePlan(T initialState, T goalState)
        {
            var visitedStates = new HashSet<T>(planningStateComparer) { initialState };
            var unvisitedPathes = UnvisitedPathes<Path<IPlanningAction<T>>>();

            var initialPossibleActions = planningActions
                .Where(action => action.CanExecute(initialState))
                .Select(action => new Path<IPlanningAction<T>>(action));

            foreach (var action in initialPossibleActions)
            {
                unvisitedPathes.Add(0, action);
            }

            while (unvisitedPathes.HasElements)
            {
                var path = unvisitedPathes.Get();

                var reachedByPath = path.Reverse().Aggregate(initialState, (current, action) => action.Execute(current));
                if (visitedStates.Contains(reachedByPath)) continue;
                if (planningStateComparer.Equals(reachedByPath, goalState)) return path.Reverse();

                visitedStates.Add(reachedByPath);

                var plans = planningActions
                    .Where(action => action.CanExecute(reachedByPath))
                    .Select(action => path.AddChild(action, planningStateComparer.Distance(action.Execute(goalState), goalState)));

                foreach (var plan in plans)
                {
                    unvisitedPathes.Add(plan.Cost + planningStateComparer.Distance(plan.Node.Execute(reachedByPath), goalState), plan);
                }
            }
            return Enumerable.Empty<IPlanningAction<T>>();
        }

        private IPrioritized<double, S> UnvisitedPathes<S>()
        {
            IPrioritized<double, S> prioritized = null;
            switch (planningMethod)
            {
                case PlanningMethod.BreadthFirst:
                    prioritized = new PrioritizedQueue<double, S>();
                    break;
                case PlanningMethod.DepthFirst:
                    prioritized = new PrioritizedStack<double, S>();
                    break;
            }
            return prioritized;
        }
    }
}
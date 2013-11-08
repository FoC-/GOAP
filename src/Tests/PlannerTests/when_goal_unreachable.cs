using System.Collections.Generic;
using GOAP;
using Machine.Specifications;

namespace Tests.PlannerTests
{
    [Subject(typeof(Planner<>))]
    class when_goal_unreachable : BaseContext
    {
        Establish context = () =>
        {
            planner = CreatePlanner();

            initialState = new State
            {
                {"1" , 3},
                {"2" , 6},
            };
            goalState = new State
            {
                {"1" , 5},
                {"2" , 5},
            };
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_be_empty = () =>
            plan.ShouldBeEmpty();

        private static Planner<State> planner;
        private static State initialState;
        private static State goalState;
        private static IEnumerable<Dictionary<string, int>> plan;
    }
}
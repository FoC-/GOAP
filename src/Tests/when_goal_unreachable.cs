using System.Collections.Generic;
using Core;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(Planner))]
    class when_goal_unreachable : BaseContext
    {
        Establish context = () =>
        {
            planner = CreatePlanner();

            initialState = new Dictionary<string, int>
            {
                {"1" , 3},
                {"2" , 6},
            };
            goalState = new Dictionary<string, int>
            {
                {"1" , 5},
                {"2" , 5},
            };
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_be_empty = () =>
            plan.ShouldBeEmpty();

        private static Planner planner;
        private static Dictionary<string, int> initialState;
        private static Dictionary<string, int> goalState;
        private static IEnumerable<Dictionary<string, int>> plan;
    }
}
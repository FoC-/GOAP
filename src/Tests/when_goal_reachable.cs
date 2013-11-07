using System.Collections.Generic;
using System.Linq;
using Core;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(Planner))]
    class when_goal_reachable : BaseContext
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
                {"2" , 4},
            };
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_return_plan = () =>
            plan.ShouldNotBeEmpty();

        It should_contain_3_steps = () =>
            plan.Count().ShouldEqual(3);

        private static Planner planner;
        private static Dictionary<string, int> initialState;
        private static Dictionary<string, int> goalState;
        private static IEnumerable<Dictionary<string, int>> plan;
    }
}

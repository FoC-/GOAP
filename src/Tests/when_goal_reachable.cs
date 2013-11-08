using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Examples;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(Planner<>))]
    class when_goal_reachable : BaseContext
    {
        Establish context = () =>
        {
            planner = CreatePlanner();

            initialState = new DictionaryState
            {
                {"1" , 3},
                {"2" , 6},
            };
            goalState = new DictionaryState
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

        private static Planner<DictionaryState> planner;
        private static DictionaryState initialState;
        private static DictionaryState goalState;
        private static IEnumerable<DictionaryState> plan;
    }
}

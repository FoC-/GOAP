using System.Collections.Generic;
using GOAP;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(Planner<>))]
    class when_goal_unreachable : BaseContext
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
                {"2" , 5},
            };
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_be_empty = () =>
            plan.ShouldBeEmpty();

        private static Planner<DictionaryState> planner;
        private static DictionaryState initialState;
        private static DictionaryState goalState;
        private static IEnumerable<Dictionary<string, int>> plan;
    }
}
using System.Collections.Generic;
using System.Linq;
using GOAP;
using GOAP.Planning;
using Machine.Specifications;

namespace Tests.PlannerTests
{
    [Subject(typeof(Planner<>))]
    class when_goal_reachable : BaseContext
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
                {"2" , 4},
            };
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_return_plan = () =>
            plan.ShouldNotBeEmpty();

        It should_contain_2_steps = () =>
            plan.Count().ShouldEqual(2);

        private static Planner<State> planner;
        private static State initialState;
        private static State goalState;
        private static IEnumerable<IPlanningAction<State>> plan;
    }
}

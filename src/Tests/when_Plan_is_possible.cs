using System.Collections.Generic;
using Core.Planing;
using Core.PlaningActions;
using Core.States;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(Planner))]
    class when_Plan_is_possible
    {
        Establish context = () =>
        {
            planningActions = new List<PlanningAction<string>>
            {
                new PlanningAction<string>("swap 1 with 2").Requires("1").Consumes("1", 1).Produces("2", 1),
                new PlanningAction<string>("swap 2 with 1").Requires("2").Consumes("2", 1).Produces("1", 1)
            };
            initialState = new State<string>();
            initialState.Add(new Dictionary<string, int> { { "1", 4 }, { "2", 5 } });
            goalState = new State<string>();
            goalState.Add(new Dictionary<string, int> { { "1", 5 }, { "2", 4 } });
        };

        Because of = () =>
            plan = Planner.MakePlan(initialState, goalState, planningActions, Method.DepthFirst);

        It should_return_plan = () =>
            plan.ShouldNotBeEmpty();

        private static State<string> initialState;
        private static State<string> goalState;
        private static List<PlanningAction<string>> planningActions;
        private static IEnumerable<IState<string>> plan;
    }
}

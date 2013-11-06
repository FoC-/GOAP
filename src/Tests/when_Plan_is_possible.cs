using System.Collections.Generic;
using Core;
using Core.Planning;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(Planner))]
    class when_Plan_is_possible
    {
        Establish context = () =>
        {
            planningActions = new List<PlanningAction<State<string>>>
            {
                new PlanningAction<State<string>>(
                    name: "swap 1 with 2",
                    validator: x => x.Count("1") > 1,
                    executor: x => {
                        x.Remove(new Dictionary<string, int>{ {"1" , 1 } });
                        x.Add(new Dictionary<string, int>{ { "2", 1 } });
                    }),
                new PlanningAction<State<string>>(
                    name:"swap 2 with 1",
                    validator:x => x.Count("2") > 1,
                    executor:x =>{
                        x.Remove(new Dictionary<string, int>{ { "2" , 1 } });
                        x.Add(new Dictionary<string, int>{ { "1", 1 } });
                    })
            };
            initialState = new State<string>();
            initialState.Add(new Dictionary<string, int> { { "1", 3 }, { "2", 6 } });
            goalState = new State<string>();
            goalState.Add(new Dictionary<string, int> { { "1", 5 }, { "2", 4 } });
        };

        Because of = () =>
            plan = Planner.MakePlan<State<string>, string>(initialState, goalState, planningActions, Method.DepthFirst);

        It should_return_plan = () =>
            plan.ShouldNotBeEmpty();

        private static State<string> initialState;
        private static State<string> goalState;
        private static List<PlanningAction<State<string>>> planningActions;
        private static IEnumerable<State<string>> plan;
    }
}

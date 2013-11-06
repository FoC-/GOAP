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
            planningActions = new List<PlanningAction<State>>
            {
                new PlanningAction<State>(
                    name: "swap 1 with 2",
                    validator: x => x.Get("1").Count > 1,
                    executor: x => {
                                var parameter1 = x.Get("1");
                                parameter1.Count -= 1;
                                x.Save(parameter1);
                                var parameter2 = x.Get("2");
                                parameter2.Count += 1;
                                x.Save(parameter2);
                    }),
                new PlanningAction<State>(
                    name:"swap 2 with 1",
                    validator: x => x.Get("2") != null && x.Get("1").Count > 1,
                    executor:x =>   {
                                var parameter1 = x.Get("1");
                                parameter1.Count += 1;
                                x.Save(parameter1);
                                var parameter2 = x.Get("2");
                                parameter2.Count -= 1;
                                x.Save(parameter2);
                    })
            };
            initialState = new State();
            initialState.Save(new Parameter { Id = "1", Count = 3, IsRequiredExectCount = true, IsRequiredForGoal = true });
            initialState.Save(new Parameter { Id = "2", Count = 6, IsRequiredExectCount = true, IsRequiredForGoal = true });
            goalState = new State();
            initialState.Save(new Parameter { Id = "1", Count = 5, IsRequiredExectCount = true, IsRequiredForGoal = true });
            initialState.Save(new Parameter { Id = "2", Count = 4, IsRequiredExectCount = true, IsRequiredForGoal = true });
        };

        Because of = () =>
            plan = Planner.MakePlan(initialState, goalState, planningActions, Method.DepthFirst);

        It should_return_plan = () =>
            plan.ShouldNotBeEmpty();

        private static State initialState;
        private static State goalState;
        private static List<PlanningAction<State>> planningActions;
        private static IEnumerable<State> plan;
    }
}

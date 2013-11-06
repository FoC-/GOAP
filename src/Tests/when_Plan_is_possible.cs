using System.Collections.Generic;
using System.Linq;
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
            var planningActions = new List<PlanningAction>
            {
                new PlanningAction(
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
                new PlanningAction(
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

            planner = new Planner(Method.DepthFirst, planningActions);
            initialState = new State();
            initialState.Save(new Parameter { Id = "1", Count = 3, IsRequiredExectCount = true, IsRequiredForGoal = true });
            initialState.Save(new Parameter { Id = "2", Count = 6, IsRequiredExectCount = true, IsRequiredForGoal = true });
            goalState = new State();
            goalState.Save(new Parameter { Id = "1", Count = 5, IsRequiredExectCount = true, IsRequiredForGoal = true });
            goalState.Save(new Parameter { Id = "2", Count = 4, IsRequiredExectCount = true, IsRequiredForGoal = true });
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_return_plan = () =>
            plan.ShouldNotBeEmpty();

        It should_contain_3_steps = () =>
            plan.Count().ShouldEqual(3);

        private static Planner planner;
        private static State initialState;
        private static State goalState;
        private static IEnumerable<State> plan;
    }
}

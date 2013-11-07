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
                    validator: x => x.Single(p => p.Name == "1").Count > 1,
                    executor: x => {
                                var parameter1 = x.Single(p => p.Name == "1");
                                parameter1.Count -= 1;
                                var parameter2 = x.Single(p => p.Name == "2");
                                parameter2.Count += 1;
                    }),
                new PlanningAction(
                    name:"swap 2 with 1",
                    validator: x => x.Single(p => p.Name == "2").Count > 1,
                    executor: x => {
                                var parameter1 = x.Single(p => p.Name == "1");
                                parameter1.Count += 1;
                                var parameter2 = x.Single(p => p.Name == "2");
                                parameter2.Count -= 1;
                    }),
            };

            planner = new Planner(Method.DepthFirst, planningActions);

            initialState = new[]
            {
                new Parameter {Name = "1", Count = 3, IsRequiredExectCount = true, IsRequiredForGoal = true},
                new Parameter {Name = "2", Count = 6, IsRequiredExectCount = true, IsRequiredForGoal = true}
            };
            goalState = new[]
            {
                new Parameter { Name = "1", Count = 5, IsRequiredExectCount = true, IsRequiredForGoal = true },
                new Parameter { Name = "2", Count = 4, IsRequiredExectCount = true, IsRequiredForGoal = true }
            };
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_return_plan = () =>
            plan.ShouldNotBeEmpty();

        It should_contain_3_steps = () =>
            plan.Count().ShouldEqual(3);

        private static Planner planner;
        private static IEnumerable<Parameter> initialState;
        private static IEnumerable<Parameter> goalState;
        private static IEnumerable<IEnumerable<Parameter>> plan;
    }
}

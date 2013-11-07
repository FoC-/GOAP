using System.Collections.Generic;
using Core;
using Core.Planning;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(Planner))]
    class when_goal_reachable : BaseContext
    {
        Establish context = () =>
        {
            planner = CreatePlanner();

            initialState = new[]
            {
                new Parameter {Name = "1", Count = 3, IsRequiredExectCount = true, IsRequiredForGoal = true},
                new Parameter {Name = "2", Count = 6, IsRequiredExectCount = true, IsRequiredForGoal = true}
            };
            goalState = new[]
            {
                new Parameter { Name = "1", Count = 5, IsRequiredExectCount = true, IsRequiredForGoal = true },
                new Parameter { Name = "2", Count = 5, IsRequiredExectCount = true, IsRequiredForGoal = true }
            };
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_return_null = () =>
            plan.ShouldBeNull();

        private static Planner planner;
        private static IEnumerable<Parameter> initialState;
        private static IEnumerable<Parameter> goalState;
        private static IEnumerable<IEnumerable<Parameter>> plan;
    }
}
using System.Collections.Generic;
using System.Linq;
using GOAP;
using GOAP.Planning;
using Machine.Specifications;

namespace Tests.PlannerTests
{
    [Subject(typeof(Planner<>))]
    internal class hanoi_tower_solver_test
    {
        //A1 1  B1 |  C1 |
        //A2 2  B2 |  C2 |
        //A3 3  B3 |  C3 |
        //  / \   / \   / \

        Establish context = () =>
        {
            initialState = new State
            {
                {"A1", 1},
                {"A2", 2},
                {"A3", 3},
            };
            goalState = new State
            {
                {"C1", 1},
                {"C2", 2},
                {"C3", 3},
            };
            var planningActions = new List<PlanningAction<State>>
            {
                new PlanningAction<State>(
                    name: "Move from A to B",
                    validator: state => Validate(state, "A", "B"),
                    executor: state => Move(state, "A", "B")),
                new PlanningAction<State>(
                    name: "Move from A to C",
                    validator: state => Validate(state, "A", "C"),
                    executor: state => Move(state, "A", "C")),
                new PlanningAction<State>(
                    name: "Move from B to A",
                    validator: state => Validate(state, "B", "A"),
                    executor: state => Move(state, "B", "A")),
                new PlanningAction<State>(
                    name: "Move from B to C",
                    validator: state => Validate(state, "B", "C"),
                    executor: state => Move(state, "B", "C")),
                new PlanningAction<State>(
                    name: "Move from C to A",
                    validator: state => Validate(state, "C", "A"),
                    executor: state => Move(state, "C", "A")),
                new PlanningAction<State>(
                    name: "Move from C to B",
                    validator: state => Validate(state, "C", "B"),
                    executor: state => Move(state, "C", "B")),
            };

            planner = new Planner<State>(PlanningMethod.DepthFirst, planningActions, new StateComaparer());
        };

        Because of = () =>
            plan = planner.MakePlan(initialState, goalState);

        It should_return_plan = () =>
            plan.ShouldNotBeEmpty();

        It should_be_7_actions_in_plan = () =>
            plan.Count().ShouldEqual(7);

        private static Planner<State> planner;
        private static IEnumerable<IPlanningAction<State>> plan;
        private static State initialState;
        private static State goalState;

        private static bool Validate(State x, string from, string to)
        {
            var onFromTop = 0;
            var trigerFrom = false;
            if (!trigerFrom) trigerFrom = x.TryGetValue(from + "1", out onFromTop);
            if (!trigerFrom) trigerFrom = x.TryGetValue(from + "2", out onFromTop);
            if (!trigerFrom) trigerFrom = x.TryGetValue(from + "3", out onFromTop);

            var onToTop = 0;
            var trigerTo = false;
            if (!trigerTo) trigerTo = x.TryGetValue(to + "1", out onToTop);
            if (!trigerTo) trigerTo = x.TryGetValue(to + "2", out onToTop);
            if (!trigerTo) trigerTo = x.TryGetValue(to + "3", out onToTop);
            return trigerFrom && (!trigerTo || onToTop > onFromTop);
        }

        private static void Move(State x, string from, string to)
        {
            var a = "";
            int onFromTop;
            var trigerFrom = false;
            if (!trigerFrom && x.TryGetValue(from + "1", out onFromTop))
            {
                a = from + "1";
                trigerFrom = true;
            }
            if (!trigerFrom && x.TryGetValue(from + "2", out onFromTop))
            {
                a = from + "2";
                trigerFrom = true;
            }
            if (!trigerFrom && x.TryGetValue(from + "3", out onFromTop))
            {
                a = from + "3";
                trigerFrom = true;
            }

            var b = "";
            int onToTop;
            var trigerTo = false;

            if (!trigerTo && x.TryGetValue(to + "1", out onToTop))
            {
                b = "";
                trigerTo = true;
            }
            if (!trigerTo && x.TryGetValue(to + "2", out onToTop))
            {
                b = to + "1";
                trigerTo = true;
            }
            if (!trigerTo && x.TryGetValue(to + "3", out onToTop))
            {
                b = to + "2";
                trigerTo = true;
            }
            if (!trigerTo)
            {
                b = to + "3";
            }

            x.Add(b, x[a]);
            x.Remove(a);
        }
    }
}
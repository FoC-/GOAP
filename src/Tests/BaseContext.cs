using System.Collections.Generic;
using GOAP;
using GOAP.Planning;

namespace Tests
{
    internal class BaseContext
    {
        protected static Planner<State> CreatePlanner()
        {
            var planningActions = new List<PlanningAction<State>>
                {
                    new PlanningAction<State>(
                        name: "swap 1 with 2",
                        validator: x => x["1"] > 1,
                        executor: x =>
                            {
                                x["1"] -= 1;
                                x["2"] += 1;
                            }),
                    new PlanningAction<State>(
                        name:"swap 2 with 1",
                        validator: x => x["2"] > 1,
                        executor: x => 
                            {
                                x["1"] += 1;
                                x["2"] -= 1;
                            }),
                };
            var stateComparer = new StateComaparer();

            return new Planner<State>(PlanningMethod.DepthFirst, planningActions, stateComparer);
        }
    }
}
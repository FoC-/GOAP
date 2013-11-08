using System.Collections.Generic;
using Core;
using Core.Planning;
using Tests.Examples;

namespace Tests
{
    internal class BaseContext
    {
        protected static Planner<DictionaryState> CreatePlanner()
        {
            var planningActions = new List<PlanningAction<DictionaryState>>
                {
                    new PlanningAction<DictionaryState>(
                        name: "swap 1 with 2",
                        validator: x => x["1"] > 1,
                        executor: x =>
                            {
                                x["1"] -= 1;
                                x["2"] += 1;
                            }),
                    new PlanningAction<DictionaryState>(
                        name:"swap 2 with 1",
                        validator: x => x["2"] > 1,
                        executor: x => 
                            {
                                x["1"] += 1;
                                x["2"] -= 1;
                            }),
                };
            var stateComparer = new DictionaryPlanningStateComaparer();

            return new Planner<DictionaryState>(PlanningMethod.DepthFirst, planningActions, stateComparer);
        }
    }
}
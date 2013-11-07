using System.Collections.Generic;
using Core;
using Core.Planning;

namespace Tests
{
    internal class BaseContext
    {
        protected static Planner<Dictionary<string, int>> CreatePlanner()
        {
            var planningActions = new List<PlanningAction<Dictionary<string, int>>>
                {
                    new PlanningAction<Dictionary<string, int>>(
                        name: "swap 1 with 2",
                        validator: x => x["1"] > 1,
                        executor: x =>
                            {
                                x["1"] -= 1;
                                x["2"] += 1;
                            }),
                    new PlanningAction<Dictionary<string, int>>(
                        name:"swap 2 with 1",
                        validator: x => x["2"] > 1,
                        executor: x => 
                            {
                                x["1"] += 1;
                                x["2"] -= 1;
                            }),
                };
            var stateComparer = new DictionaryStateComaparer();

            return new Planner<Dictionary<string, int>>(Method.DepthFirst, planningActions, stateComparer);
        }
    }
}
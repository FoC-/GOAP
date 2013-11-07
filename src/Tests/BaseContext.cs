using System.Collections.Generic;
using Core;
using Core.Planning;

namespace Tests
{
    internal class BaseContext
    {
        protected static Planner CreatePlanner()
        {
            var planningActions = new List<PlanningAction>
                {
                    new PlanningAction(
                        name: "swap 1 with 2",
                        validator: x => x["1"] > 1,
                        executor: x =>
                            {
                                x["1"] -= 1;
                                x["2"] += 1;
                            }),
                    new PlanningAction(
                        name:"swap 2 with 1",
                        validator: x => x["2"] > 1,
                        executor: x => 
                            {
                                x["1"] += 1;
                                x["2"] -= 1;
                            }),
                };

            return new Planner(Method.DepthFirst, planningActions);
        }
    }
}
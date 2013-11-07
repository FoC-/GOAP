using System.Collections.Generic;
using System.Linq;
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

            return new Planner(Method.DepthFirst, planningActions);
        }
    }
}
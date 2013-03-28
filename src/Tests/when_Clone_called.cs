using Core.PlaningActions;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(PlanningAction<string>))]
    public class when_Clone_called
    {
        Establish context = () =>
        {
            planningAction = new PlanningAction<string>("pa_origin");
            planningAction.Produces("asd", 45);
        };

        Because of = () =>
        {
            result = (PlanningAction<string>)planningAction.Clone();
            result.Consumes("dfsd");
            result.MultiProducer(true);
        };

        It should_return_new_instance = () =>
            result.ShouldNotBeTheSameAs(planningAction);

        private static PlanningAction<string> result;
        private static PlanningAction<string> planningAction;
    }
}
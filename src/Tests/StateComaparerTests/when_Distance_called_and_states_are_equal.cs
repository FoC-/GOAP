using GOAP;
using Machine.Specifications;

namespace Tests.StateComaparerTests
{
    [Subject(typeof(StateComaparer))]
    class when_Distance_called_and_states_are_equal
    {
        Establish context = () =>
            {
                source = new State{
                    {"param1", 10},
                    {"param2", 20},
                    {"param3", 30},
                };
                destination = new State{
                    {"param3", 30},
                    {"param2", 20},
                    {"param1", 10},
                };
            };
        Because of = () =>
            result = new StateComaparer().Distance(destination, source);

        It should_return_true = () =>
            result.ShouldBeCloseTo(0.0);

        private static double result;
        private static State source;
        private static State destination;
    }
}
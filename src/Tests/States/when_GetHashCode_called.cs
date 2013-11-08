using GOAP;
using Machine.Specifications;

namespace Tests.States
{
    [Subject(typeof(StateComaparer))]
    public class when_GetHashCode_called
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
            comparer = new StateComaparer();
        };
        Because of = () =>
            result = comparer.GetHashCode(source) == comparer.GetHashCode(destination);

        It should_return_true = () =>
            result.ShouldBeTrue();

        private static bool result;
        private static State source;
        private static State destination;
        private static StateComaparer comparer;
    }
}

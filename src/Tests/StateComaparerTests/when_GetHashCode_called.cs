using FluentAssertions;
using GOAP;
using Machine.Specifications;

namespace Tests.StateComaparerTests
{
    [Subject(typeof(StateComaparer))]
    class when_GetHashCode_called
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
            result = new StateComaparer().GetHashCode(source) == new StateComaparer().GetHashCode(destination);

        It should_return_true = () =>
            result.Should().BeTrue();

        private static bool result;
        private static State source;
        private static State destination;
    }
}

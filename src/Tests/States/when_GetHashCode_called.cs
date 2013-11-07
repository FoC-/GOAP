using System.Collections.Generic;
using Core.Planning;
using Machine.Specifications;

namespace Tests.States
{
    [Subject(typeof(IEnumerable<Parameter>))]
    public class when_GetHashCode_called
    {
        Establish context = () =>
        {
            source = new[]{
                new Parameter { Name = "param1", Count = 10, IsRequiredExectCount = true, IsRequiredForGoal = true },
                new Parameter { Name = "param2", Count = 20, IsRequiredExectCount = true, IsRequiredForGoal = true },
                new Parameter { Name = "param3", Count = 30, IsRequiredExectCount = true, IsRequiredForGoal = true },
            };
            destination = new[]{
                new Parameter { Name = "param3", Count = 30, IsRequiredExectCount = true, IsRequiredForGoal = true },
                new Parameter { Name = "param2", Count = 20, IsRequiredExectCount = true, IsRequiredForGoal = true },
                new Parameter { Name = "param1", Count = 10, IsRequiredExectCount = true, IsRequiredForGoal = true },
            };
            comparer = new StateComaparer();
        };
        Because of = () =>
            result = comparer.GetHashCode(source) == comparer.GetHashCode(destination);

        It should_return_true = () =>
            result.ShouldBeTrue();

        private static bool result;
        private static IEnumerable<Parameter> source;
        private static IEnumerable<Parameter> destination;
        private static StateComaparer comparer;
    }
}

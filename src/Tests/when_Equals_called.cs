using System.Collections.Generic;
using Core.Planning;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(State))]
    public class when_Equals_called
    {
        Establish context = () =>
        {
            source = new State();
            source.Save(new Parameter { Id = "param1", Count = 10, IsRequiredExectCount = true, IsRequiredForGoal = true });
            source.Save(new Parameter { Id = "param2", Count = 20, IsRequiredExectCount = true, IsRequiredForGoal = true });
            source.Save(new Parameter { Id = "param3", Count = 30, IsRequiredExectCount = true, IsRequiredForGoal = true });

            destination = new State();
            destination.Save(new Parameter { Id = "param3", Count = 30, IsRequiredExectCount = true, IsRequiredForGoal = true });
            destination.Save(new Parameter { Id = "param2", Count = 20, IsRequiredExectCount = true, IsRequiredForGoal = true });
            destination.Save(new Parameter { Id = "param1", Count = 10, IsRequiredExectCount = true, IsRequiredForGoal = true });
        };
        Because of = () =>
            result = source.Equals(destination);

        It should_return_true = () =>
            result.ShouldBeTrue();

        private static bool result;
        private static State source;
        private static State destination;
    }
}

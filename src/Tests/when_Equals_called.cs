using System.Collections.Generic;
using Core.States;
using Machine.Specifications;

namespace Tests
{
    [Subject(typeof(State<string>))]
    public class when_Equals_called
    {
        Establish context = () =>
        {
            source = new State<string>();
            source.Add(new Dictionary<string, int> { { "param1", 10 }, { "param2", 20 }, { "param3", 30 } });

            destination = new State<string>();
            destination.Add(new Dictionary<string, int> { { "param1", 10 }, { "param2", 20 }, { "param3", 30 } });
        };
        Because of = () =>
            result = source.Equals(destination);

        It should_return_true = () =>
            result.ShouldBeTrue();

        private static bool result;
        private static State<string> source;
        private static State<string> destination;
    }
}

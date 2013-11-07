using System.Collections.Generic;
using Core.Planning;
using Machine.Specifications;

namespace Tests.States
{
    [Subject(typeof(DictionaryStateComaparer))]
    public class when_GetHashCode_called
    {
        Establish context = () =>
        {
            source = new Dictionary<string, int>{
                {"param1", 10},
                {"param2", 20},
                {"param3", 30},
            };
            destination = new Dictionary<string, int>{
                {"param3", 30},
                {"param2", 20},
                {"param1", 10},
            };
            comparer = new DictionaryStateComaparer();
        };
        Because of = () =>
            result = comparer.GetHashCode(source) == comparer.GetHashCode(destination);

        It should_return_true = () =>
            result.ShouldBeTrue();

        private static bool result;
        private static Dictionary<string, int> source;
        private static Dictionary<string, int> destination;
        private static DictionaryStateComaparer comparer;
    }
}

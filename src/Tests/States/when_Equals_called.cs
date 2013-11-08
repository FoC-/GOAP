using Core.Examples;
using Machine.Specifications;

namespace Tests.States
{
    [Subject(typeof(DictionaryStateComaparer))]
    public class when_Equals_called
    {
        Establish context = () =>
        {
            source = new DictionaryState{
                {"param1", 10},
                {"param2", 20},
                {"param3", 30},
            };
            destination = new DictionaryState{
                {"param3", 30},
                {"param2", 20},
                {"param1", 10},
            };
            comparer = new DictionaryStateComaparer();
        };
        Because of = () =>
            result = comparer.Equals(source, destination);

        It should_return_true = () =>
            result.ShouldBeTrue();

        private static bool result;
        private static DictionaryState source;
        private static DictionaryState destination;
        private static DictionaryStateComaparer comparer;
    }
}

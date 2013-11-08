using GOAP;
using Machine.Specifications;

namespace Tests.StateComaparerTests
{
    [Subject(typeof(StateComaparer))]
    class when_Distance_called
    {
        Establish context = () =>
            {
                state1 = new State{
                    {"param1", 10},
                    {"param2", 20},
                    {"param3", 30},
                };
                state2 = new State{
                    {"param1", 10},
                    {"param2", 15},
                    {"param3", 30},
                };
                state3 = new State{
                    {"param1", 10},
                    {"param2", 20},
                    {"param3", 20},
                };
                state4 = new State{
                    {"param1", 10},
                    {"param2", 20},
                };
            };

        Because of = () =>
            {
                distance1 = new StateComaparer().Distance(state1, state2);
                distance2 = new StateComaparer().Distance(state1, state3);
                distance3 = new StateComaparer().Distance(state1, state4);
            };

        It should_one_distance_be_less_then_another1 = () =>
            distance2.ShouldBeLessThan(distance1);

        It should_one_distance_be_less_then_another2 = () =>
            distance3.ShouldBeLessThan(distance1);


        private static double distance1;
        private static double distance2;
        private static double distance3;
        private static State state1;
        private static State state2;
        private static State state3;
        private static State state4;
    }
}
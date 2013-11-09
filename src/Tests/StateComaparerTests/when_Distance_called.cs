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
                state5 = new State{
                    {"param1", 10},
                    {"param4", 20},
                };
            };

        Because of = () =>
            {
                distance12 = new StateComaparer().Distance(state1, state2);
                distance13 = new StateComaparer().Distance(state1, state3);
                distance14 = new StateComaparer().Distance(state1, state4);
                distance15 = new StateComaparer().Distance(state1, state5);
            };

        It should_one_distance_be_less_then_another1 = () =>
            distance12.ShouldBeLessThan(distance13);

        It should_one_distance_be_less_then_another2 = () =>
            distance12.ShouldBeLessThan(distance14);  

        It should_one_distance_be_less_then_another3 = () =>
            distance12.ShouldBeLessThan(distance15);


        private static double distance12;
        private static double distance13;
        private static double distance14;
        private static double distance15;
        private static State state1;
        private static State state2;
        private static State state3;
        private static State state4;
        private static State state5;
    }
}
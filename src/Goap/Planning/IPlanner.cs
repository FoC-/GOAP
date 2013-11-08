using System.Collections.Generic;

namespace GOAP.Planning
{
    public interface IPlanner<T>
    {
        IEnumerable<T> MakePlan(T initialState, T goalState);
    }
}
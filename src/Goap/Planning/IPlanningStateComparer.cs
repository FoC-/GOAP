using System.Collections.Generic;

namespace GOAP.Planning
{
    public interface IPlanningStateComparer<in T> : IEqualityComparer<T>
    {
        double Distance(T state1, T state2);
    }
}
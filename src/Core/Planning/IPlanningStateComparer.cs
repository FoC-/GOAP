using System.Collections.Generic;

namespace Core.Planning
{
    public interface IPlanningStateComparer<in T> : IEqualityComparer<T>
    {
        double Distance(T x, T y);
    }
}
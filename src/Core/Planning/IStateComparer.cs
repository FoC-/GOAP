using System.Collections.Generic;

namespace Core.Planning
{
    public interface IStateComparer<T> : IEqualityComparer<T>
    {
        double Distance(T x, T y);
    }
}
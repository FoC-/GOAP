using System.Collections.Generic;

namespace Core.Planning
{
    public interface IStateComparer : IEqualityComparer<Dictionary<string, int>>
    {
        double Distance(Dictionary<string, int> x, Dictionary<string, int> y);
    }
}
using System.Collections.Generic;
using System.Linq;
using Core.Planning;

namespace Core.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<Parameter> ShallowCopy(this IEnumerable<Parameter> source)
        {
            return source.Select(o => o.ShallowCopy());
        }
    }
}

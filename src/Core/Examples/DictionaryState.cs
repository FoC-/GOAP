using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Examples
{
    public class DictionaryState : Dictionary<string, int>, ICloneable
    {
        public object Clone()
        {
            return (DictionaryState)(this.ToDictionary(p => p.Key, p => p.Value));
        }
    }
}
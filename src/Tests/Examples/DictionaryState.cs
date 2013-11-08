using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Examples
{
    public class DictionaryState : Dictionary<string, int>, ICloneable
    {
        public DictionaryState()
        {
        }

        public DictionaryState(IDictionary<string, int> dictionary)
            : base(dictionary)
        {
        }

        public object Clone()
        {
            return new DictionaryState(this.ToDictionary(p => p.Key, p => p.Value));
        }
    }
}
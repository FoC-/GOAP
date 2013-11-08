using System;
using System.Collections.Generic;
using System.Linq;

namespace GOAP
{
    public class State : Dictionary<string, int>, ICloneable
    {
        public State()
        {
        }

        public State(IDictionary<string, int> dictionary)
            : base(dictionary)
        {
        }

        public object Clone()
        {
            return new State(this.ToDictionary(p => p.Key, p => p.Value));
        }
    }
}
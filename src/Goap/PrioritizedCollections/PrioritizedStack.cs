using System.Collections.Generic;
using System.Linq;

namespace GOAP.PrioritizedCollections
{
    internal class PrioritizedStack<K, V> : IPrioritized<K, V>
    {
        private readonly SortedDictionary<K, Stack<V>> stacks = new SortedDictionary<K, Stack<V>>();

        public bool HasElements
        {
            get { return stacks.Any(); }
        }

        public void Add(K priority, V value)
        {
            Stack<V> stack;
            if (stacks.ContainsKey(priority))
            {
                stack = stacks[priority];
            }
            else
            {
                stack = new Stack<V>();
                stacks.Add(priority, stack);
            }
            stack.Push(value);
        }

        public V Get()
        {
            if (!HasElements)
                return default(V);

            var pair = stacks.First();
            var stack = pair.Value;
            var value = stack.Pop();
            if (stack.Count == 0)
                stacks.Remove(pair.Key);
            return value;
        }
    }
}
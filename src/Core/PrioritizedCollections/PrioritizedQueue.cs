using System.Collections.Generic;
using System.Linq;

namespace GOAP.PrioritizedCollections
{
    internal class PrioritizedQueue<K, V> : IPrioritized<K, V>
    {
        private readonly SortedDictionary<K, Queue<V>> queues = new SortedDictionary<K, Queue<V>>();

        public bool HasElements
        {
            get { return queues.Any(); }
        }

        public void Add(K priority, V value)
        {
            Queue<V> queue;
            if (queues.ContainsKey(priority))
            {
                queue = queues[priority];
            }
            else
            {
                queue = new Queue<V>();
                queues.Add(priority, queue);
            }
            queue.Enqueue(value);
        }

        public V Get()
        {
            if (!HasElements)
                return default(V);

            var pair = queues.First();
            var queue = pair.Value;
            var value = queue.Dequeue();
            if (queue.Count == 0)
                queues.Remove(pair.Key);
            return value;
        }
    }
}
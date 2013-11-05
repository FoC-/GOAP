using System.Collections;
using System.Collections.Generic;

namespace Core.Graph
{
    public class Path<T> : IEnumerable<T>
    {
        public T Node { get; private set; }
        public double Cost { get; private set; }
        public Path<T> PathToParentNode { get; private set; }

        public Path(T rootNode) : this(rootNode, null, 0) { }
        private Path(T node, Path<T> pathToParentNode, double cost)
        {
            Node = node;
            PathToParentNode = pathToParentNode;
            Cost = cost;
        }

        public Path<T> AddChild(T node, double cost)
        {
            return new Path<T>(node, this, Cost + cost);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var path = this; path != null; path = path.PathToParentNode)
            {
                yield return path.Node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
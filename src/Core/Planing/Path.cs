using System.Collections;
using System.Collections.Generic;

namespace Core.Planing
{
    public class Path<N> : IEnumerable<N>
    {
        public double PathCost { get; private set; }
        public N Node { get; private set; }
        public Path<N> PathToNode { get; private set; }

        public Path(N startNode) : this(startNode, null, 0) { }
        private Path(N node, Path<N> pathToNode, double pathCost)
        {
            Node = node;
            PathToNode = pathToNode;
            PathCost = pathCost;
        }

        public Path<N> Add(N node, double cost)
        {
            return new Path<N>(node, this, PathCost + cost);
        }

        public IEnumerator<N> GetEnumerator()
        {
            for (Path<N> path = this; path != null; path = path.PathToNode)
                yield return path.Node;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
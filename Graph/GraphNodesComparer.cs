using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class GraphNodesComparer : IComparer<GraphNode>
    {
        public int Compare(GraphNode x, GraphNode y)
        {
            return x.CompareTo(y);
        }
    }
}

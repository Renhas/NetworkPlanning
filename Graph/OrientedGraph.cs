using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class OrientedGraph
    {
        readonly public string ProductName;

        public GraphNode Root;
        delegate void SeekAction(GraphNode node);

        public OrientedGraph(string name) 
        {
            ProductName = name;
            Root = null;
        }
        public OrientedGraph(string name, GraphNode root) : this(name)
        {
            Root = root;
        }

        private void Seek(SeekAction action) 
        {
            if (Root == null) return;
            List<GraphNode> visited = new List<GraphNode>();
            Recursiv(Root);
            void Recursiv(GraphNode node)
            {
                action(node);
                visited.Add(node);
                
                if(node.Next.Any())
                foreach (var next in node.Next)
                {
                    if (visited.Contains(next)) continue;
                    Recursiv(next);
                }

                if(node.Prev.Any())
                foreach (var prev in node.Prev)
                {
                    if (visited.Contains(prev)) continue;
                    Recursiv(prev);
                }
            }
        }

        public int JobCount()
        {

            int result = 0;

            void Count(GraphNode node) 
            {
                if(node != null) result++;
            }
            Seek(Count);
            return result;
        }

        public GraphNode FindAt(int id) 
        {
            if (id < 1) throw new ArgumentException("Некорректный индекс");


            GraphNode result = null;
            void Compare(GraphNode node)
            {
                if (node.Id == id) result = node;
                if (result != null) return;
            }
            Seek(Compare);

            return result;

        }

        public override string ToString()
        {
            string result = $"Изделие {ProductName}\n";

            void StringNode(GraphNode node) 
            {
                result += node.ToString() + "\n";
            }
            Seek(StringNode);
            return result;
        }





    }
}

using Graph;
using Microsoft.SqlServer.Server;
using MyResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamespace
{
    public static class Algorythm
    {
        static List<GraphNode> Jobs;

        public static int Run(Company company)
        {
            if (company == null) throw new ArgumentNullException();
            Jobs = company.GetAllJobs();
            int tact;
            for (tact = 1; Jobs.Any(); tact++) 
            {
                company.Import();
                List<GraphNode> front = ConstructFront(tact);
                foreach (GraphNode node in front)
                {
                    node.Work(tact);
                    if (node.StartTime != 0) 
                    {
                        if (Jobs.Count == 1) tact = Jobs[0].EndTime;
                        Jobs.Remove(node);
                    }
                }
            }
            return tact - 1;
        }

        static List<GraphNode> ConstructFront(int tact) 
        {
            List<GraphNode> front = new List<GraphNode>();

            foreach (var node in Jobs) 
            {
                if (!CheckJob(node, tact)) continue;
                front.Add(node);
            }
            front.Sort(new GraphNodesTimeComparer());
            return front;
        }
        static bool CheckJob(GraphNode node, int tact) 
        {
            if (node.ResourceIntensity > node.Resource.Count) return false;
            if (node.EntryTime != null && node.EntryTime > tact) return false;
            if (node.StartTime != 0) return false;
            foreach (var prev in node.Prev)
            {
                if (prev.StartTime != 0 && prev.EndTime < tact) continue;
                return false;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using MyResources;

namespace CompanyNamespace
{
    public class Company
    {
        List<OrientedGraph> Products;
        List<Resource> Resources;
        public int ProductsCount { get { return Products.Count; } }
        public Company() 
        {
            Products = new List<OrientedGraph>();
            Resources = new List<Resource>();
        }

        public void Import() 
        {
            foreach (var res in Resources) res.Count += res.PerTact;
        }

        public void AddProduct(string name) 
        {
            Products.Add(new OrientedGraph(name));
        }

        public void AddProduct(OrientedGraph graph) 
        {
            Products.Add(graph);
        }

        public void AddResource(Resource resource) 
        {
            Resources.Add(resource);
        }

        public OrientedGraph GetProduct(int id) 
        {
            if(id < 0 || id >= Products.Count) throw new ArgumentOutOfRangeException($"id = {id}");
            return Products[id];
        }

        public OrientedGraph GetProduct(string name) 
        {

            foreach (OrientedGraph product in Products) 
            {
                if(product.ProductName == name) return product;
            }
            throw new ArgumentException($"Такого изделия не существует = {name}");
        }

        public Resource GetResource(int resId) 
        {
            foreach (Resource resource in Resources) 
            {
                if(resource.Id == resId) return resource;
            }
            return null;
        }

        public List<Resource> GetAllResources() 
        {
            return Resources;
        }

        public List<GraphNode> GetAllJobs()
        {
            List<GraphNode> allJobs = new List<GraphNode>();
            foreach (var graph in Products)
            {
                allJobs.AddRange(graph.ToSortedList());
            }
            return allJobs;
        }

        public int DirectiveCalculate() 
        {
            int result = 0;

            foreach (var node in GetAllJobs()) 
            {
                if (node.Directive != null) result = Math.Max(result, node.EndTime - (int)node.Directive);
            }
            return result; 
        }

        public override string ToString()
        {
            string result = "";
            if (Products.Any()) result = "Изделия:\n";
            foreach (var graph in Products) result += graph;
            if (result != "") result += "\n";
            if (Resources.Any()) result += "Ресурсы:\n";
            foreach (var resource in Resources) result += $"{resource}\n";
            return result;
        }
    }
}

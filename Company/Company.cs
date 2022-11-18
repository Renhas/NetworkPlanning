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
        public Company() 
        {
            Products = new List<OrientedGraph>();
            Resources = new List<Resource>();
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

        public OrientedGraph GetProduct(string name) 
        {

            foreach (OrientedGraph product in Products) 
            {
                if(product.ProductName == name) return product;
            }
            return null;
        }

        public Resource GetResource(int resId) 
        {
            foreach (Resource resource in Resources) 
            {
                if(resource.Id == resId) return resource;
            }
            return null;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

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

        public void Add(string name) 
        {
            Products.Add(new OrientedGraph(name));
        }

        public void Add(OrientedGraph graph) 
        {
            Products.Add(graph);
        }

        public OrientedGraph GetProduct(string name) 
        {

            foreach (OrientedGraph product in Products) 
            {
                if(product.ProductName == name) return product;
            }
            return null;
        }

        public override string ToString()
        {
            string result = "";
            foreach (var graph in Products) result += graph;
            return result;
        }
    }
}

using Graph;
using MyResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompanyNamespace
{
    public static class XMLReader
    {
        public static Company Read(string path) 
        {
            Company company = new Company();

            XDocument document = XDocument.Load(path);

            XElement root = document.Root;
            foreach (var element in root.Element("Resources").Elements("Resource")) 
            {
                company.AddResource(ReadResource(element));
            }
            foreach (XElement element in root.Element("Products").Elements("Product")) 
            {
                company.AddProduct(ReadProduct(element, company));
            }

            return company;
        }

        private static Resource ReadResource(XElement element) 
        {
            int id = int.Parse(element.Attribute("id").Value);
            int perTact = int.Parse(element.Value);
            return new Resource(id, perTact);
        }

        private static OrientedGraph ReadProduct(XElement product, Company company) 
        {
            OrientedGraph graph = new OrientedGraph(product.Attribute("name").Value);

            foreach (XElement element in product.Elements("Job")) 
            {

                ReadJob(element, product, graph, company);
            }
            return graph;
        }

        private static GraphNode ReadJob(XElement job, XElement product, OrientedGraph graph, Company company) 
        {
            int id = int.Parse(job.Attribute("id").Value);
            GraphNode node = graph.FindAt(id);
            if (node != null) return node;

            int resIntens = int.Parse(job.Element("ResIntens").Value);
            int workIntens = int.Parse(job.Element("Intensivity").Value);
            int? startTime = null;
            if (job.Element("StartTime") != null) startTime = int.Parse(job.Element("StartTime").Value);
            int? directive = null;
            if (job.Element("Directive") != null) directive = int.Parse(job.Element("Directive").Value);
            Resource resource = company.GetResource(int.Parse(job.Element("Resource").Value));
            node = new GraphNode(id, resIntens, workIntens, resource, startTime, directive);
            if(graph.Root == null) graph.Root = node;

            if (job.Element("Next") == null) return node;
            foreach (var next in job.Element("Next").Elements("id")) 
            {
                GraphNode nextNode = ReadJob(
                    product.Elements("Job").Where(el => el.Attribute("id").Value == next.Value).First(),
                    product, graph, company);
                node.Next.Add(nextNode);
                nextNode.Prev.Add(node);
            }
            return node;
        }
    }
}

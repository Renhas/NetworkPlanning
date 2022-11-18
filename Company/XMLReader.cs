using Graph;
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
            foreach (XElement element in root.Element("Products").Elements("Product")) 
            {
                company.Add(ReadProduct(element));
            }

            return company;
        }

        private static OrientedGraph ReadProduct(XElement product) 
        {
            OrientedGraph graph = new OrientedGraph(product.Attribute("name").Value);

            foreach (XElement element in product.Elements("Job")) 
            {

                if (graph.Root == null) graph.Root = ReadJob(element, product, graph);
                else ReadJob(element, product, graph);
            }
            return graph;
        }

        private static GraphNode ReadJob(XElement job, XElement product, OrientedGraph graph) 
        {
            int id = int.Parse(job.Attribute("id").Value);
            GraphNode node = graph.FindAt(id);
            if (node != null) return node;

            int resType = int.Parse(job.Element("ResType").Value);
            int resIntens = int.Parse(job.Element("ResIntens").Value);
            int workIntens = int.Parse(job.Element("Intensivity").Value);
            int? startTime = null;
            if (job.Element("StartTime") != null) startTime = int.Parse(job.Element("StartTime").Value);
            int? directive = null;
            if (job.Element("Directive") != null) directive = int.Parse(job.Element("Directive").Value);

            node = new GraphNode(id, resType, resIntens, workIntens, startTime, directive);

            if (job.Element("Next") == null) return node;
            foreach (var next in job.Element("Next").Elements("id")) 
            {
                GraphNode nextNode = ReadJob(product.Elements("Job").Where(el => el.Attribute("id").Value == next.Value).First(), product, graph);
                node.Next.Add(nextNode);
                nextNode.Prev.Add(node);
            }
            return node;
        }
    }
}

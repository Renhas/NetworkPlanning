using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class GraphNode
    {
        public List<GraphNode> Next;
        public List<GraphNode> Prev;

        readonly public int ResourceType;
        readonly public int ResourceIntensity;
        readonly public int WorkIntensity;

        readonly public int Id;
        readonly public int? StartTime;
        readonly public int? Directive;

        public GraphNode(int id, int resourceType, int resourceIntensity, int workIntensity, int? startTime = null, int? directive = null) 
        {
            if (id < 1 || resourceType < 1 || resourceIntensity < 1 || workIntensity < 1) throw new ArgumentException("Некорректные данные!");
            Id = id;
            ResourceType = resourceType;
            ResourceIntensity = resourceIntensity;
            WorkIntensity = workIntensity;
            if (startTime != null && startTime < 1) throw new ArgumentException("Некорректные данные!");
            StartTime = startTime;
            if (directive != null && directive < 1) throw new ArgumentException("Некорректные данные!");
            Directive = directive;
            Next = new List<GraphNode>();
            Prev = new List<GraphNode>();

        }

        public int CalculateTimeReserve() 
        {
            return LateEnd() - EarlyEnd();
        }

        public int EarlyStart()
        {
            if (Prev.Any())
            {
                int max = Prev[0].EarlyEnd();
                foreach (var node in Prev.Skip(1))
                {
                    max = Math.Max(node.EarlyEnd(), max);
                }
                return max;
            }
            else if (StartTime != null) return (int)StartTime;
            else throw new ArgumentException("Не задано время поступления в систему");
        }

        public int EarlyEnd() 
        {
            return EarlyStart() + (int)Math.Ceiling((float)ResourceIntensity / WorkIntensity) - 1;
        }

        public int LateStart() 
        {
            return LateEnd() - (int)Math.Ceiling((float)ResourceIntensity / WorkIntensity) + 1;
        }

        public int LateEnd() 
        {
            if (Next.Any())
            {
                int min = Next[0].LateStart();
                foreach (var node in Next.Skip(1))
                {
                    min = Math.Min(node.LateStart(), min);
                }
                return min;
            }
            else return EarlyEnd();
        }

        public override string ToString()
        {
            string next = "", prev = "", startTime = "", directive = "";
            if (Next.Any()) 
            {
                next = "Next = [";
                foreach (GraphNode node in Next) next += $"{node.Id}, ";
                next = next.Remove(next.Length - 2) + "], ";
            }
            if (Prev.Any()) 
            {
                prev = "Prev = [";
                foreach (GraphNode node in Prev) prev += $"{node.Id}, ";
                prev = prev.Remove(prev.Length - 2) + "], ";
            }
            if (StartTime != null) startTime = $", время поступления в систему = {StartTime}";
            if (Directive != null) directive = $", директивный срок = {Directive}";
            return $"{Id}: {next}{prev}" +
                $"тип ресурса = {ResourceType}, интенсивность = {WorkIntensity}, ресурсоёмкость = {ResourceIntensity}, " +
                $"резерв времени = {CalculateTimeReserve()}{startTime}{directive}";
        }
    }
}

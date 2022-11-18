using MyResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class GraphNode : IComparable<GraphNode>
    {
        public List<GraphNode> Next;
        public List<GraphNode> Prev;

        public int StartTime;
        public int EndTime;
        public bool Done { get; private set; }
        private int _remainingResource;
        public int RemainingResource 
        {
            get { return _remainingResource; }
            set 
            {
                if (value < 0 || value > ResourceIntensity) throw new ArgumentOutOfRangeException();
                if (value == 0) Done = true;
                else Done = false;
                _remainingResource = value;
                
            }
        }
        
        readonly public Resource Resource;
        readonly public int ResourceIntensity;
        readonly public int WorkIntensity;

        readonly public int Id;
        readonly public int? EntryTime;
        readonly public int? Directive;

        public GraphNode(int id, int resourceIntensity, int workIntensity, Resource resource, int? startTime = null, int? directive = null) 
        {
            if (id < 1 || resourceIntensity < 1 || workIntensity < 1) throw new ArgumentException("Некорректные данные!");
            Id = id;
            ResourceIntensity = resourceIntensity;
            WorkIntensity = workIntensity;
            if (startTime != null && startTime < 1) throw new ArgumentException("Некорректные данные!");
            EntryTime = startTime;
            if (directive != null && directive < 1) throw new ArgumentException("Некорректные данные!");
            Directive = directive;
            Next = new List<GraphNode>();
            Prev = new List<GraphNode>();
            Resource = resource;
            StartTime = EndTime = 0;
            RemainingResource = resourceIntensity;
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
            else if (EntryTime != null) return (int)EntryTime;
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
            if (EntryTime != null) startTime = $", время поступления в систему = {EntryTime}";
            if (Directive != null) directive = $", директивный срок = {Directive}";
            return $"{Id}: {next}{prev}" +
                $"ресурс = {Resource.Id}, интенсивность = {WorkIntensity}, ресурсоёмкость = {ResourceIntensity}, " +
                $"резерв времени = {CalculateTimeReserve()}{startTime}{directive}";
        }

        public int CompareTo(GraphNode other)
        {
            return Id.CompareTo(other.Id);
        }
    }
}

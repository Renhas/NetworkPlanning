using MyResources;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class GraphNode : IComparable<GraphNode>, ICloneable
    {
        public List<GraphNode> Next;
        public List<GraphNode> Prev;

        public int StartTime;
        public int EndTime 
        {
            get { return StartTime + WorkTime - 1; }   
        }
        public int WorkTime
        {
            get { return (int)Math.Ceiling((float)ResourceIntensity / WorkIntensity); }
        }

        private int? _reserve;
        public int TimeReserve 
        {
            get 
            {
                if (_reserve == null) _reserve = CalculateTimeReserve();
                return _reserve.Value;
            }
        }
        
        readonly public Resource Resource;
        readonly public int ResourceIntensity;
        readonly public int WorkIntensity;


        readonly public int Id;
        readonly public int? EntryTime;
        readonly public int? Directive;

        public GraphNode(int id, int resourceIntensity, int workIntensity, 
            Resource resource, int? entryTime = null, int? directive = null) 
        {
            if (id < 1 || resourceIntensity < 1 || workIntensity < 1)
                throw new ArgumentException("Некорректные данные!");
            Id = id;
            ResourceIntensity = resourceIntensity;
            WorkIntensity = workIntensity;
            if (entryTime != null && entryTime < 1 ||
                directive != null && directive < 1)
                throw new ArgumentException("Некорректные данные!");
            EntryTime = entryTime;
            Directive = directive;
            Next = new List<GraphNode>();
            Prev = new List<GraphNode>();
            Resource = resource;
            StartTime = 0;

        }

        public void ResetReserve() => _reserve = null;
        public void Work(int tact) 
        {
            if (Resource.Count < ResourceIntensity) return;
            StartTime = tact;
            Resource.Count -= ResourceIntensity;
        }
        private int CalculateTimeReserve() 
        {
            return LateEnd() - EarlyEnd();
        }

        private int EarlyStart()
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

        private int EarlyEnd() 
        {
            return EarlyStart() + WorkTime - 1;
        }

        private int LateStart() 
        {
            return LateEnd() - WorkTime + 1;
        }

        private int LateEnd() 
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
                $"ресурс = {Resource.Id}, интенсивность = {WorkIntensity}, " +
                $"ресурсоёмкость = {ResourceIntensity}, " +
                $"время работы = {WorkTime}, " +
                $"резерв времени = {TimeReserve}{startTime}{directive}";
        }

        public int CompareTo(GraphNode other)
        {
            return Id.CompareTo(other.Id);
        }

        public object Clone() 
        {
            GraphNode newNode = new GraphNode(Id, ResourceIntensity, WorkIntensity, Resource, EntryTime, Directive);
            foreach (var next in Next) newNode.Next.Add(next);
            foreach (var prev in Prev) newNode.Prev.Add(prev);
            newNode.StartTime = StartTime;
            newNode._reserve = _reserve;
            return newNode;
        }
    }
}

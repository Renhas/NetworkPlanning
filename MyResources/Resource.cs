using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResources
{
    public class Resource
    {
        public readonly int Id;
        public readonly int PerTact;
        private int count;
        public int Count {
            get { return count; }
            set 
            {
                if (value < 0 || value > count + PerTact) throw new ArgumentOutOfRangeException("Некорректное значение");
                count = value;
            }
        }

        public Resource(int id, int perTact)
        {
            if (perTact < 1 || id < 1) throw new ArgumentException("Недопустимые значения");
            Id = id;
            PerTact = perTact;
            count = 0;
        }

        public override string ToString()
        {
            return $"Ресурс #{Id}: {PerTact} за один такт, {Count} сейчас есть";
        }
    }
}

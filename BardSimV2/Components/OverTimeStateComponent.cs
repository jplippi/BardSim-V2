using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class OverTimeStateComponent : Component
    {
        private List<DoT> dotList = new List<DoT>();
        private decimal offset;

        public List<DoT> DotList { get => dotList; }
        public decimal Offset { get => offset; }

        public OverTimeStateComponent(Entity parent) : base(parent)
        {
            offset = (new Random().Next(0, 300) * 10).MilliToSeconds();
        }
    }
}

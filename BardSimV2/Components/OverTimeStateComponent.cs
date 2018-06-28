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

        public List<DoT> DotList { get => dotList; set => dotList = value; }
        public decimal Offset { get => offset; set => offset = value; }

        public OverTimeStateComponent(Entity parent) : base(parent)
        {
            offset = (new Random().Next(0, 300) * 10).MilliToSeconds();
        }
    }
}

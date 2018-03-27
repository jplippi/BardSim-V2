using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class DotEffectComponent : Component
    {
        private DotName name;
        private decimal duration;
        private int potency;

        public DotName Name { get => name; }
        public decimal Duration { get => duration; }
        public int Potency { get => potency; }

        public DotEffectComponent(Entity parent, DotName name, decimal duration, int potency) : base(parent)
        {
            this.name = name;
            this.duration = duration;
            this.potency = potency;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class GenericStatusEffectComponent : Component
    {
        private StatusName name;
        private decimal duration;

        public StatusName Name { get => name; }
        public decimal Duration { get => duration; }

        public GenericStatusEffectComponent(Entity parent, StatusName name, decimal duration) : base(parent)
        {
            this.name = name;
            this.duration = duration;
        }
    }
}

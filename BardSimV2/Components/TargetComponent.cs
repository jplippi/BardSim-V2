using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class TargetComponent : Component
    {
        private Entity target;

        public Entity Target { get => target; }

        public TargetComponent(Entity parent) : base (parent)
        {
            target = null;
        }

        public TargetComponent(Entity parent, Entity target) : base(parent)
        {
            this.target = target;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    abstract class Component
    {
        protected Entity parent;

        public Entity Parent { get => parent; }

        public Component(Entity parent)
        {
            this.parent = parent;
        }

    }
}

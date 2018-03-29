using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class UsesEnablerComponent : Component
    {
        private StatusName name;
        
        public StatusName Name { get => name; }

        public UsesEnablerComponent(Entity parent, StatusName name) : base(parent)
        {
            this.name = name;
        }
    }
}

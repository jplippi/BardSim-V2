using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Entity
    {
        private ImmutableArray<Component> components;
        
        public ImmutableArray<Component> Components { get => components; }

        public Entity(ImmutableArray<Component> components)
        {
            this.components = components;
        }
    }
}

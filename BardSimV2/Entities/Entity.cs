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
        private List<Component> components;
        
        public List<Component> Components { get => components; }

        public Entity()
        {
        }

        public void AddComponents(List<Component> components)
        {
            this.components = components;
        }
    }
}

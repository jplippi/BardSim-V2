using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class KeyMappingComponent : Component
    {
        // Technically, they would be hotbar slots, but since it's just a simulation, they're straight up skill commands.
        private List<KeyBind> keyBinds;
        public List<KeyBind> KeyBinds { get => keyBinds; }

        public KeyMappingComponent(Entity parent, List<KeyBind> keyBinds) : base(parent)
        {
            this.keyBinds = keyBinds;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{ 
    class UseConditionComponent : Component
    {
        private Func<ModifierStateComponent, Component, bool> function;

        public Func<ModifierStateComponent, Component, bool> Function { get => function; }

        public UseConditionComponent(Entity parent, Func<ModifierStateComponent, Component, bool> function) : base(parent)
        {
            this.function = function;
        }
    }
}

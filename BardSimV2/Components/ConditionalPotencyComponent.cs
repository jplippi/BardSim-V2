using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class ConditionalPotencyComponent : Component
    {
        private Func<Entity, OverTimeStateComponent, Component, int> function;

        public Func<Entity, OverTimeStateComponent, Component, int> Function { get => function; }

        public ConditionalPotencyComponent(Entity parent, Func<Entity, OverTimeStateComponent, Component, int> function) : base(parent)
        {
            this.function = function;
        }
    }
}

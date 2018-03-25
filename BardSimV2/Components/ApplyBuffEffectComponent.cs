using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class ApplyBuffEffectComponent : Component
    {
        private AttributeType type;
        private ActorType actor;
        private decimal duration;
        private decimal modifier;

        public AttributeType Type { get => type; }
        public ActorType Actor { get => actor; }
        public decimal Duration { get => duration; }
        public decimal Modifier { get => modifier; }

        public ApplyBuffEffectComponent(Entity parent, AttributeType type, ActorType actor, decimal duration, decimal modifier) : base(parent)
        {
            this.type = type;
            this.actor = actor;
            this.duration = duration;
            this.modifier = modifier;
        }
    }
}

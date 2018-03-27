using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class StatusEffectComponent : Component
    {
        private AttributeType type;
        private StatusName name;
        private ActorType actor;
        private decimal duration;
        private decimal modifier;

        public AttributeType Type { get => type; }
        public StatusName Name { get => name; }
        public ActorType Actor { get => actor; }
        public decimal Duration { get => duration; }
        public decimal Modifier { get => modifier; }

        public StatusEffectComponent(Entity parent, AttributeType type, StatusName name, ActorType actor, decimal duration, decimal modifier) : base(parent)
        {
            this.type = type;
            this.name = name;
            this.actor = actor;
            this.duration = duration;
            this.modifier = modifier;
        }
    }
}

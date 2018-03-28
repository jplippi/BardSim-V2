using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Buff
    {
        private Entity buffTarget;
        private Entity userSource;
        private StatusName name;
        private AttributeType type;
        private decimal duration;
        private decimal start;
        private decimal modifier;
        private bool isActive;

        public Entity BuffTarget { get => buffTarget; }
        public Entity UserSource { get => userSource; }
        public StatusName Name { get => name; }
        public AttributeType Type { get => type; }
        public decimal Duration { get => duration; }
        public decimal Modifier { get => modifier; }
        public decimal Start { get => start; set => start = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public Buff(Entity buffTarget, Entity userSource, StatusName name, AttributeType type, decimal duration, decimal start, decimal modifier, bool isActive)
        {
            this.buffTarget = buffTarget;
            this.userSource = userSource;
            this.name = name;
            this.type = type;
            this.duration = duration;
            this.start = start;
            this.modifier = modifier;
            this.isActive = isActive;
        }
    }
}

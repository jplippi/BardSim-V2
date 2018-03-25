using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Buff
    {
        private Entity skillSource;
        private Entity userSource;
        private AttributeType type;
        private decimal duration;
        private ulong start;
        private decimal modifier;
        private bool isActive;

        public Entity SkillSource { get => skillSource; }
        public Entity UserSource { get => userSource; }
        public AttributeType Type { get => type; }
        public decimal Duration { get => duration; }
        public decimal Modifier { get => modifier; }
        public ulong Start { get => start; set => start = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public Buff(Entity skillSource, Entity userSource, AttributeType type, decimal duration, ulong start, decimal modifier, bool isActive)
        {
            this.skillSource = skillSource;
            this.userSource = userSource;
            this.type = type;
            this.duration = duration;
            this.start = start;
            this.modifier = modifier;
            this.isActive = isActive;
        }
    }
}

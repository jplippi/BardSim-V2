using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class SkillBaseComponent : Component
    {
        private SkillName name;
        private SkillType type;
        private Cooldown cooldown;

        public SkillName Name { get => name; }
        public SkillType Type { get => type; }
        public Cooldown Cooldown { get => cooldown; }

        public SkillBaseComponent(Entity parent, SkillName name, SkillType type, Cooldown cooldown) : base(parent)
        {
            this.name = name;
            this.type = type;
            this.cooldown = cooldown;
        }
    }
}

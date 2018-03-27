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

        public SkillName Name { get => name; }
        public SkillType Type { get => type; }

        public SkillBaseComponent(Entity parent, SkillName name, SkillType type) : base(parent)
        {
            this.name = name;
            this.type = type;
        }
    }
}

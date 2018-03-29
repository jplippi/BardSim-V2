using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class RiverOfBloodComponent : Component
    {
        private List<Entity> skillList;

        public List<Entity> SkillList { get => skillList; }

        public RiverOfBloodComponent(Entity parent, List<Entity> skillList) : base(parent)
        {
            this.skillList = skillList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class DragoonComponent : Component
    {
        private Job job = Job.Dragoon;
        private List<Entity> skillList;

        public Job Job { get => job; }
        public List<Entity> SkillList { get => skillList; }

        public DragoonComponent(Entity parent, List<Entity> skillList) : base(parent)
        {
            this.skillList = skillList;
        }
    }
}

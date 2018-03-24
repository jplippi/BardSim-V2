using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class JobComponent : Component
    {
        private Jobs job;
        private List<Entity> skillList;

        public Jobs Job { get => job; }
        public List<Entity> SkillList { get => skillList; }

        public JobComponent(Entity parent, Jobs job, List<Entity> skillList) : base(parent)
        {
            this.job = job;
            this.skillList = skillList;
        }
    }
}

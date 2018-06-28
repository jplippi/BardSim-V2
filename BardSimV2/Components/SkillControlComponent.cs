using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class SkillControlComponent : Component
    {
        private Dictionary<SkillName, bool> skillControlDictionary;
        private List<SkillName> skillControlList;

        public Dictionary<SkillName, bool> SkillControlDictionary { get => skillControlDictionary; }
        public List<SkillName> SkillControlList { get => skillControlList; }

        public SkillControlComponent(Entity parent, Job job) : base(parent)
        {
            skillControlDictionary = new Dictionary<SkillName, bool>();
            skillControlList = JobSettings.brdSkillNames;

            if (job == Job.Bard)
            {
                foreach (SkillName s in skillControlList)
                {
                    skillControlDictionary[s] = false;
                }
            }
        }
    }
}

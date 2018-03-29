using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class BardComponent : Component
    {
        private Job job = Job.Bard;
        private List<Entity> skillList;

        // Bard-specific resources
        private int repertoire = 0;
        private SongName song = SongName.None;
        private decimal songDuration;
        private decimal songStart;

        public Job Job { get => job; }
        public List<Entity> SkillList { get => skillList; }
        public int Repertoire { get => repertoire; set => repertoire = value; }
        public SongName Song { get => song; set => song = value; }
        public decimal SongDuration { get => songDuration; set => songDuration = value; }
        public decimal SongStart { get => songStart; set => songStart = value; }

        public BardComponent(Entity parent, List<Entity> skillList) : base(parent)
        {
            this.skillList = skillList;
        }
    }
}

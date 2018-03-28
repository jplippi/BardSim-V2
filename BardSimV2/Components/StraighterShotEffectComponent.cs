using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class StraighterShotEffectComponent : Component
    {
        private StatusName name;
        private decimal duration;
        private decimal probability;

        public StatusName Name { get => name; }
        public decimal Duration { get => duration; }
        public decimal Probability { get => probability; }

        public StraighterShotEffectComponent(Entity parent, StatusName name, decimal duration, decimal probability) : base(parent)
        {
            this.name = name;
            this.duration = duration;
            this.probability = probability;
        }
    }
}

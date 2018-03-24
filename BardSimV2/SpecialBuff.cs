using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class SpecialBuff
    {
        protected SpecialBuffType attribute;
        protected decimal duration;
        protected decimal start;
        protected decimal modifier;

        public SpecialBuffType Attribute { get => attribute; }
        public decimal Duration { get => duration; }
        public decimal Modifier { get => modifier; }
        public decimal Start { get => start; }

        public SpecialBuff(SpecialBuffType attribute, decimal duration, decimal start, decimal modifier)
        {
            this.attribute = attribute;
            this.duration = duration;
            this.start = start;
            this.modifier = modifier;
        }
    }
}

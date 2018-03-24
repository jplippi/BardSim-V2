using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AttributeBuff
    {
        protected AttributeType attribute;
        protected decimal duration;
        protected decimal start;
        protected decimal modifier;

        public AttributeType Attribute { get => attribute; }
        public decimal Duration { get => duration; }
        public decimal Modifier { get => modifier; }
        public decimal Start { get => start; }

        public AttributeBuff(AttributeType attribute, decimal duration, decimal start, decimal modifier)
        {
            this.attribute = attribute;
            this.duration = duration;
            this.start = start;
            this.modifier = modifier;
        }
    }
}

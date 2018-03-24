using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class CooldownComponent : Component
    {
        private bool isOnGCD;
        private decimal recast;
        private decimal start;

        public decimal Recast
        {
            get
            {
                if (isOnGCD)
                {
                    var attComp = parent.Components.First(x => x.GetType() == typeof(AttributesComponent));
                    return attComp.GCD;
                }
                return recast;
            }
        }
        public decimal Start { get; set; }

        public CooldownComponent(Entity parent, bool isOnGCD, decimal recast, decimal start = 0) : base(parent)
        {
            this.isOnGCD = isOnGCD;
            this.recast = recast;
        }
    }
}

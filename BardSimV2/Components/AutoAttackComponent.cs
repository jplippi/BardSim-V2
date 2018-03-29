using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AutoAttackComponent : Component
    {
        private int potency;
        private decimal nextAuto;
        
        public int Potency { get => potency; }
        public decimal NextAuto { get => nextAuto; set => nextAuto = value; }


        public AutoAttackComponent(Entity parent, int potency, decimal start = 0) : base(parent)
        {
            this.potency = potency;
            this.nextAuto = start;
        }
    }
}

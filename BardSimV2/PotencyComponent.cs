using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class PotencyComponent : Component
    {
        private int amount;

        public int Amount { get => amount; }

        public PotencyComponent(Entity parent, int amount) : base(parent)
        {
            this.amount = amount;
        }
    }
}

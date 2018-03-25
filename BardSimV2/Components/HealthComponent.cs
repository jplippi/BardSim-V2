using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class HealthComponent : Component
    {
        private decimal amount;
        private decimal damageTaken = 0;

        public decimal Amount { get => amount; set => amount = value; }
        public decimal DamageTaken { get => damageTaken; set => damageTaken = value; }

        public HealthComponent(Entity parent, decimal amount) : base(parent)
        {
            this.amount = amount;
        }
    }
}

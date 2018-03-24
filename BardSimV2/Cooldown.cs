using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Cooldown
    {
        private decimal baseRecast;
        private decimal start;

        public decimal BaseRecast { get => baseRecast; }
        public decimal Start { get => start; set => start = value; }

        public Cooldown(decimal baseRecast, decimal start = 0)
        {
            this.baseRecast = baseRecast;
            this.start = start;
        }
    }
}

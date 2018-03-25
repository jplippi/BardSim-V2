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
        private ulong start;

        public decimal BaseRecast { get => baseRecast; }
        public ulong Start { get => start; set => start = value; }

        public Cooldown(decimal baseRecast, ulong start = 0)
        {
            this.baseRecast = baseRecast;
            this.start = start;
        }
    }
}

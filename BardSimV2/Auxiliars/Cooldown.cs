using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class CooldownComponent : Component
    {
        private decimal baseRecast;
        private List<Entity> sharedCooldownList;
        private ulong start;

        public decimal BaseRecast { get => baseRecast; }
        public List<Entity> SharedCooldownList { get => sharedCooldownList; }
        public ulong Start { get => start; set => start = value; }

        public CooldownComponent(Entity parent, decimal baseRecast, List<Entity> sharedCooldownList, ulong start = 0) : base (parent)
        {
            this.baseRecast = baseRecast;
            this.sharedCooldownList = sharedCooldownList;
            this.start = start;
        }

        public CooldownComponent(Entity parent, decimal baseRecast, ulong start = 0) : base(parent)
        {
            this.baseRecast = baseRecast;
            sharedCooldownList = new List<Entity> { parent };
            this.start = start;
        }
    }
}

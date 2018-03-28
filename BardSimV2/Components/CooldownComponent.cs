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
        private decimal start;
        private decimal usableAt;

        public decimal BaseRecast { get => baseRecast; }
        public List<Entity> SharedCooldownList { get => sharedCooldownList; }
        public decimal Start { get => start; set => start = value; }
        public decimal UsableAt { get => usableAt; set => usableAt = value; }

        public CooldownComponent(Entity parent, decimal baseRecast, List<Entity> sharedCooldownList, decimal start = 0, decimal usableAt = 0) : base (parent)
        {
            this.baseRecast = baseRecast;
            this.sharedCooldownList = sharedCooldownList;
            this.start = start;
            this.usableAt = usableAt;
        }

        public CooldownComponent(Entity parent, decimal baseRecast, decimal start = 0, decimal usableAt = 0) : base(parent)
        {
            this.baseRecast = baseRecast;
            sharedCooldownList = new List<Entity> { parent };
            this.start = start;
            this.usableAt = usableAt;
        }
    }
}

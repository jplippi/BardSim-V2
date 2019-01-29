using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class PartyComponent : Component
    {
        private List<Entity> members = new List<Entity>();

        public List<Entity> Members { get => members; }

        public PartyComponent(Entity parent, params Entity[] members) : base(parent)
        {
            this.members.Add(parent);
            this.members.AddRange(members);
        }

    }
}

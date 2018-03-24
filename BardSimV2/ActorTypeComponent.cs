using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class ActorTypeComponent : Component
    {
        private ActorType actorType;

        public ActorType ActorType { get => actorType; }

        public ActorTypeComponent(Entity parent, ActorType actorType) : base(parent)
        {
            this.parent = parent;
            this.actorType = actorType;
        }
    }

    enum ActorType { Player, Ally, Enemy};
}

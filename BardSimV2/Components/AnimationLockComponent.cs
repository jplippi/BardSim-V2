using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AnimationLockComponent : Component
    {
        private decimal start;

        public decimal Start { get => start; set => start = value; }

        public AnimationLockComponent(Entity parent) : base(parent)
        {
            // Assumes the player starts out of animation lock
            start = 0 - Constants.AnimationLock;
        }
    }
}

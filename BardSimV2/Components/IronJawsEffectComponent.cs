using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class IronJawsEffectComponent : Component
    {
        private List<DotEffectComponent> dotList;

        public List<DotEffectComponent> DotList { get => dotList; }

        public IronJawsEffectComponent(Entity parent, List<DotEffectComponent> dotList) : base(parent)
        {
            this.dotList = dotList;
        }
    }
}

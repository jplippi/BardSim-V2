using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class JobComponent : Component
    {
        private Jobs job;

        public Jobs Job { get => job; }

        public JobComponent(Entity parent, Jobs job) : base(parent)
        {
            this.job = job;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Enabler
    {
        private Entity user;
        private StatusName name;
        private decimal duration;
        private decimal start;

        public Entity User { get => user; }
        public StatusName Name { get => name; }
        public decimal Duration { get => duration; }
        public decimal Start { get => start; set => start = value; }

        public Enabler(Entity user, StatusName name, decimal duration, decimal start)
        {
            this.user = user;
            this.name = name;
            this.duration = duration;
            this.start = start;
        }
    }
}

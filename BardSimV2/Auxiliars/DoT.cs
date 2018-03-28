using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class DoT
    {
        private Entity dotTarget;
        private Entity userSource;
        private DotName name;
        private decimal potency;
        private decimal duration;
        private decimal start;
        private decimal lastTick;
        private Dictionary<DamageModifierType, decimal> usersModifiersDictionary;
        private Dictionary<AttributeType, decimal> usersChancesDictionary;
        private List<Buff> usersBuffList;
        private bool isActive;

        public Entity DotTarget { get => dotTarget; }
        public Entity UserSource { get => userSource; }
        public DotName Name { get => name; }
        public decimal Potency { get => potency; }
        public decimal Duration { get => duration; }
        public decimal Start { get => start; set => start = value; }
        public decimal LastTick { get => lastTick; set => lastTick = value; }
        public Dictionary<DamageModifierType, decimal> UsersModifiersDictionary { get => usersModifiersDictionary; }
        public Dictionary<AttributeType, decimal> UsersChancesDictionary { get => usersChancesDictionary; }
        public List<Buff> UsersBuffList { get => usersBuffList; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public DoT(Entity dotTarget, Entity userSource, Dictionary<DamageModifierType, decimal> usersModifiersDictionary, Dictionary<AttributeType, decimal> usersChancesDictionary, List<Buff> usersBuffList, DotName name, decimal potency, decimal duration, decimal start, bool isActive)
        {
            this.dotTarget = dotTarget;
            this.userSource = userSource;
            this.name = name;
            this.potency = potency;
            this.duration = duration;
            this.start = start;
            lastTick = 0;
            this.usersModifiersDictionary = usersModifiersDictionary;
            this.usersChancesDictionary = usersChancesDictionary;
            this.isActive = isActive;

            // Copies the buff list:
            this.usersBuffList = new List<Buff>();
            foreach(Buff b in usersBuffList)
            {
                this.usersBuffList.Add(new Buff(b.BuffTarget, b.UserSource, b.Name, b.Type, b.Duration, b.Start, b.Modifier, b.IsActive));
            }
        }
    }
}

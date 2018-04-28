using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class ModifierStateComponent : Component
    {
        private BuffsDictionary buffDictionary = new BuffsDictionary();
        private List<Buff> buffList = new List<Buff>();
        private List<Enabler> enablerList = new List<Enabler>();


        public BuffsDictionary BuffDictionary { get => buffDictionary; set => buffDictionary = value; }
        public List<Buff> BuffList { get => buffList; set => buffList = value; }
        public List<Enabler> EnablerList { get => enablerList; set => enablerList = value; }

        public ModifierStateComponent(Entity parent) : base(parent)
        {

        }
    }
}

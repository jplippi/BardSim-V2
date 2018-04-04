using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class ModifierStateComponent : Component
    {
        private Dictionary<AttributeType, decimal> buffDictionary;
        private List<Buff> buffList = new List<Buff>();
        private List<Enabler> enablerList = new List<Enabler>();


        public Dictionary<AttributeType, decimal> BuffDictionary { get => buffDictionary; set => buffDictionary = value; }
        public List<Buff> BuffList { get => buffList; set => buffList = value; }
        public List<Enabler> EnablerList { get => enablerList; set => enablerList = value; }

        public ModifierStateComponent(Entity parent) : base(parent)
        {
            buffDictionary = new Dictionary<AttributeType, decimal>
            {
                { AttributeType.Strenght, 0 },
                { AttributeType.Dexterity, 0 },
                { AttributeType.Vitality, 0 },
                { AttributeType.Intelligence, 0 },
                { AttributeType.Mind, 0},
                { AttributeType.CriticalHit, 0},
                { AttributeType.Determination, 0},
                { AttributeType.DirectHit, 0},
                { AttributeType.SkillSpeed, 0},
                { AttributeType.SpellSpeed, 0},
                { AttributeType.Tenacity, 0},
                { AttributeType.Piety, 0 },
                { AttributeType.WeaponDamage, 0 },
                { AttributeType.WeaponDelay, 0 },
                { AttributeType.CriticalHitRate, 0 },
                { AttributeType.DirectHitRate, 0 },
                { AttributeType.Arrow, 0 },
                { AttributeType.FeyWind, 0 },
                { AttributeType.Haste, 0 },
                { AttributeType.SpeedType1, 0 },
                { AttributeType.SpeedType2, 0 },
                { AttributeType.RiddleOfFire, 100 },
                { AttributeType.AstralUmbral, 100 },
            };
        }
    }
}

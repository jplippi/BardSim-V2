using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class BuffStateComponent : Component
    {
        private Dictionary<AttributeType, decimal> attributesDictionary;
        private Dictionary<SpecialBuffType, decimal> specialBuffDictionary;
        private List<AttributeBuff> attributeBuffList = new List<AttributeBuff>();
        private List<SpecialBuff> specialBuffList = new List<SpecialBuff>();

        public Dictionary<AttributeType, decimal> AttributesDictionary { get => attributesDictionary; }
        public Dictionary<SpecialBuffType, decimal> SpecialBuffDictionary { get => specialBuffDictionary; }
        public List<AttributeBuff> AttributeBuffList { get => attributeBuffList; }
        public List<SpecialBuff> SpecialBuffList { get => specialBuffList; }

        public BuffStateComponent(Entity parent) : base(parent)
        {
            attributesDictionary = new Dictionary<AttributeType, decimal>
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
                { AttributeType.WeaponDelay, 0 }
            };

            specialBuffDictionary = new Dictionary<SpecialBuffType, decimal>
            {
                { SpecialBuffType.Damage, 0 },
                { SpecialBuffType.CriticalHitRate, 0 },
                { SpecialBuffType.DirectHitRate, 0 },
                { SpecialBuffType.Arrow, 0 },
                { SpecialBuffType.FeyWind, 0 },
                { SpecialBuffType.Haste, 0 },
                { SpecialBuffType.SpeedType1, 0 },
                { SpecialBuffType.SpeedType2, 0 },
                { SpecialBuffType.RiddleOfFire, 100 },
                { SpecialBuffType.AstralUmbral, 100 },
            };
        }
    }
}

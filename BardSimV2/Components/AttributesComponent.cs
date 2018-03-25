using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AttributesComponent : Component
    {
        private Dictionary<AttributeType, decimal> attributesDictionary;

        public Dictionary<AttributeType, decimal> AttributesDictionary { get => attributesDictionary; }

        public AttributesComponent(Entity parent, decimal strenght, decimal dexterity, decimal vitality, decimal intelligence, decimal mind, decimal criticalHit, decimal determination, decimal directHit, decimal skillSpeed, decimal spellSpeed, decimal tenacity, decimal piety, decimal weaponDamage, decimal weaponDelay) : base (parent)
        {
            attributesDictionary = new Dictionary<AttributeType, decimal>
            {
                { AttributeType.Strenght, strenght },
                { AttributeType.Dexterity, dexterity },
                { AttributeType.Vitality, vitality },
                { AttributeType.Intelligence, intelligence },
                { AttributeType.Mind, mind},
                { AttributeType.CriticalHit, criticalHit},
                { AttributeType.Determination, determination},
                { AttributeType.DirectHit, directHit},
                { AttributeType.SkillSpeed, skillSpeed},
                { AttributeType.SpellSpeed, spellSpeed},
                { AttributeType.Tenacity, tenacity},
                { AttributeType.Piety, piety },
                { AttributeType.WeaponDamage, weaponDamage },
                { AttributeType.WeaponDelay, weaponDelay }
            };
        }
    }
}

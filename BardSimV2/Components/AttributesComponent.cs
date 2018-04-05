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

        public AttributesComponent(Entity parent, decimal strenght = 264, decimal dexterity = 386, decimal vitality = 292, decimal intelligence = 247, decimal mind = 232, decimal criticalHit = 364, decimal determination = 292, decimal directHit = 364, decimal skillSpeed = 364, decimal spellSpeed = 364, decimal tenacity = 364, decimal piety = 364, decimal weaponDamage = 4, decimal weaponDelay = 2.8m) : base (parent)
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

        public AttributesComponent(Entity parent, Dictionary<AttributeType, decimal> attributesDictionary) : base(parent)
        {
            this.attributesDictionary = attributesDictionary;
        }

    }
}

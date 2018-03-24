using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AttributesComponent : Component
    {
        private Dictionary<AttributeType, int> attributesDictionary;

        public Dictionary<AttributeType, int> AttributesDictionary { get => attributesDictionary; }

        public AttributesComponent(Entity parent, int strenght, int dexterity, int vitality, int intelligence, int mind, int criticalHit, int determination, int directHit, int skillSpeed, int spellSpeed, int tenacity, int piety, int weaponDamage, int weaponDelay) : base (parent)
        {
            attributesDictionary = new Dictionary<AttributeType, int>
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

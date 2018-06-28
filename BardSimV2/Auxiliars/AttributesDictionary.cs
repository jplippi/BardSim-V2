using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AttributesDictionary : Dictionary<AttributeType, decimal>
    {
        public AttributesDictionary() : base()
        {
            //TODO: Change to race-specific and job-specific attributes
            this.Add(AttributeType.Strenght, 264);
            this.Add(AttributeType.Dexterity, 386);
            this.Add(AttributeType.Vitality, 292);
            this.Add(AttributeType.Intelligence, 247);
            this.Add(AttributeType.Mind, 232);
            this.Add(AttributeType.CriticalHit, 364);
            this.Add(AttributeType.Determination, 292);
            this.Add(AttributeType.DirectHit, 364);
            this.Add(AttributeType.SkillSpeed, 364);
            this.Add(AttributeType.SpellSpeed, 364);
            this.Add(AttributeType.Tenacity, 364);
            this.Add(AttributeType.Piety, 292);
            this.Add(AttributeType.WeaponDamage, 4);
            this.Add(AttributeType.WeaponDelay, 2.8m);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class BuffsDictionary : Dictionary<AttributeType, decimal>
    {
        public BuffsDictionary() : base()
        {
            this.Add(AttributeType.Strenght, 0);
            this.Add(AttributeType.Dexterity, 0);
            this.Add(AttributeType.Vitality, 0);
            this.Add(AttributeType.Intelligence, 0);
            this.Add(AttributeType.Mind, 0);
            this.Add(AttributeType.CriticalHit, 0);
            this.Add(AttributeType.Determination, 0);
            this.Add(AttributeType.DirectHit, 0);
            this.Add(AttributeType.SkillSpeed, 0);
            this.Add(AttributeType.SpellSpeed, 0);
            this.Add(AttributeType.Tenacity, 0);
            this.Add(AttributeType.Piety, 0);
            this.Add(AttributeType.WeaponDamage, 0);
            this.Add(AttributeType.WeaponDelay, 0);
            this.Add(AttributeType.CriticalHitRate, 0);
            this.Add(AttributeType.DirectHitRate, 0);
            this.Add(AttributeType.Arrow, 0);
            this.Add(AttributeType.FeyWind, 0);
            this.Add(AttributeType.Haste, 0);
            this.Add(AttributeType.SpeedType1, 0);
            this.Add(AttributeType.SpeedType2, 0);
            this.Add(AttributeType.RiddleOfFire, 100);
            this.Add(AttributeType.AstralUmbral, 100);
        }
    }
}

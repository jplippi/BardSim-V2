using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    enum AttributeType
    {
        // Base attributes
        Strenght,
        Dexterity,
        Vitality,
        Intelligence,
        Mind,
        CriticalHit,
        Determination,
        DirectHit,
        SkillSpeed,
        SpellSpeed,
        Tenacity,
        Piety,
        WeaponDamage,
        WeaponDelay,

        // Special attributes
        CriticalHitRate,
        DirectHitRate,

        // Speed attributes
        Arrow,
        FeyWind,
        Haste,
        SpeedType1,
        SpeedType2,
        RiddleOfFire,
        AstralUmbral,

        // This one fucker that doesn't like playing nice
        Damage
    }
}

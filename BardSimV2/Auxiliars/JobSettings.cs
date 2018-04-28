using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    static class JobSettings
    {
        public readonly static List<AttributeType> brdAttributeTypes = new List<AttributeType> {
            AttributeType.Dexterity,
            AttributeType.DirectHit,
            AttributeType.CriticalHit,
            AttributeType.Determination,
            AttributeType.SkillSpeed,
            AttributeType.Vitality
        };
    }
}

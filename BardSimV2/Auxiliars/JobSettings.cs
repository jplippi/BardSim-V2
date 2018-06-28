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

        public readonly static List<SkillName> brdSkillNames = new List<SkillName>
        {
            SkillName.ArmysPaeon,
            SkillName.Barrage,
            SkillName.BattleVoice,
            SkillName.Bloodletter,
            SkillName.CausticBite,
            SkillName.EmpyrealArrow,
            SkillName.FoeRequiem,
            SkillName.HeavyShot,
            SkillName.IronJaws,
            SkillName.MagesBallad,
            SkillName.MiserysEnd,
            SkillName.NaturesMinne,
            SkillName.PitchPerfect,
            SkillName.QuickNock,
            SkillName.RagingStrikes,
            SkillName.RainOfDeath,
            SkillName.RefulgentArrow,
            SkillName.RepellingShot,
            SkillName.Sidewinder,
            SkillName.Stormbite,
            SkillName.StraightShot,
            SkillName.TheWanderersMinuet,
            SkillName.TheWardensPaean,
            SkillName.Troubadour,
            SkillName.VenomousBite,
            SkillName.Windbite
        };
    }
}

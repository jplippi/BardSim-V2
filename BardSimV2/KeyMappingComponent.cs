using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class KeyMappingComponent : Component
    {
        // Technically, they would be hotbar slots, but since it's just a simulation, they're straight up skill commands.
        private List<KeyBind> keyBinds;
        public List<KeyBind> KeyBinds { get => keyBinds; }

        public KeyMappingComponent(Entity parent) : base(parent)
        {
            keyBinds = new List<KeyBind> {
                new KeyBind(SkillName.HeavyShot, Keys.Num1),
                new KeyBind(SkillName.StraightShot, Keys.Num2),
                new KeyBind(SkillName.RefulgentArrow, Keys.F3),
                new KeyBind(SkillName.Bloodletter, Keys.R),
                new KeyBind(SkillName.EmpyrealArrow, Keys.Num6),
                new KeyBind(SkillName.PitchPerfect, Keys.T)
            };
        }
    }
}

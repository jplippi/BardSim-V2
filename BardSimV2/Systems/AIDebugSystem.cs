using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AIDebugSystem : ISystem
    {
        Entity player;
        ModifierStateComponent modStateComponent;
        OverTimeStateComponent targOtComp;
        SkillBaseComponent straightShotBaseComp;
        SkillBaseComponent barrageBaseComp;
        CooldownComponent barrageCooldownComp;
        TargetComponent targComp;


        public AIDebugSystem(Entity player, List<CooldownComponent> cooldownComponents, List<ModifierStateComponent> modifierStateComponents, List<OverTimeStateComponent> overtimeStateComponents, List<SkillBaseComponent> skillBaseComponents, List<TargetComponent> targetComponents)
        {
            this.player = player;
            modStateComponent = modifierStateComponents.Find(x => x.Parent == player);
            straightShotBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.StraightShot);
            barrageBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.Barrage);
            barrageCooldownComp = cooldownComponents.Find(x => x.Parent == barrageBaseComp.Parent);
            targComp = targetComponents.Find(x => x.Parent == player);
            targOtComp = overtimeStateComponents.Find(x => x.Parent == targComp.Target);

        }

        public void Update(decimal timer, Keyboard keyboard)
        {
            if(targOtComp.DotList.Find(x => x.Name == DotName.StormBite && x.Duration - timer + x.Start < 4m) != null || targOtComp.DotList.Find(x => x.Name == DotName.CausticBite && x.Duration - timer + x.Start < 4m) != null)
            {
                UseKey(keyboard, Keys.Num5);
            }
            else if(modStateComponent.EnablerList.Find(x => x.Name == StatusName.StraighterShot) != null && barrageCooldownComp.UsableAt <= timer)
            {
                UseKey(keyboard, Keys.F2);
            }
            else if(modStateComponent.EnablerList.Find(x => x.Name == StatusName.StraighterShot) != null)
            {
                UseKey(keyboard, Keys.F3);
            }
            else if(modStateComponent.BuffList.Find(x => x.IsActive && x.Name == StatusName.StraighterShot) == null)
            {
                UseKey(keyboard, Keys.Num2);
            }
            else if(targOtComp.DotList.Find(x => x.IsActive == true && x.Name == DotName.StormBite) == null)
            {
                UseKey(keyboard, Keys.Num3);
            }
            else if(targOtComp.DotList.Find(x => x.IsActive == true && x.Name == DotName.CausticBite) == null)
            {
                UseKey(keyboard, Keys.Num4);
            }
            else
            {
                UseKey(keyboard, Keys.Num1);
            }
        }

        void UseKey(Keyboard keyboard, Keys key)
        {

            foreach(Keys k in Enum.GetValues(typeof(Keys)))
            {
                if(k == key)
                {
                    keyboard.keysDictionary[k] = true;
                }
                else
                {
                    keyboard.keysDictionary[k] = false;
                }
            }
        }

        decimal NextGCD(CooldownComponent heavyShotCdComp, decimal timer)
        {
            return heavyShotCdComp.Start;
        }
    }
}

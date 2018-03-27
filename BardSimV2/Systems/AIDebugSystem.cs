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
        SkillBaseComponent skillBaseComp;
        TargetComponent targComp;


        public AIDebugSystem(Entity player, List<ModifierStateComponent> modifierStateComponents, List<OverTimeStateComponent> overtimeStateComponents, List<SkillBaseComponent> skillBaseComponents, List<TargetComponent> targetComponents)
        {
            this.player = player;
            modStateComponent = modifierStateComponents.Find(x => x.Parent == player);
            skillBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.StraightShot);
            targComp = targetComponents.Find(x => x.Parent == player);
            targOtComp = overtimeStateComponents.Find(x => x.Parent == targComp.Target);

        }

        public void Update(ulong timer, Keyboard keyboard)
        {
            if(modStateComponent.BuffList.Find(x => x.IsActive && x.Name == StatusName.StraighterShot) == null)
            {
                keyboard.keysDictionary[Keys.Num1] = false;
                keyboard.keysDictionary[Keys.Num2] = true;
                keyboard.keysDictionary[Keys.Num3] = false;
                keyboard.keysDictionary[Keys.Num4] = false;
            }
            else
            {
                if(targOtComp.DotList.Find(x => x.IsActive == true && x.Name == DotName.StormBite) == null)
                {
                    keyboard.keysDictionary[Keys.Num1] = false;
                    keyboard.keysDictionary[Keys.Num2] = false;
                    keyboard.keysDictionary[Keys.Num3] = true;
                    keyboard.keysDictionary[Keys.Num4] = false;
                }
                else if(targOtComp.DotList.Find(x => x.IsActive == true && x.Name == DotName.CausticBite) == null)
                {
                    keyboard.keysDictionary[Keys.Num1] = false;
                    keyboard.keysDictionary[Keys.Num2] = false;
                    keyboard.keysDictionary[Keys.Num3] = false;
                    keyboard.keysDictionary[Keys.Num4] = true;
                }
                else
                {
                    keyboard.keysDictionary[Keys.Num1] = true;
                    keyboard.keysDictionary[Keys.Num2] = false;
                    keyboard.keysDictionary[Keys.Num3] = false;
                    keyboard.keysDictionary[Keys.Num4] = false;
                }

            }
        }
    }
}

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
        BuffStateComponent buffStateComponent;
        SkillBaseComponent skillBaseComp;


        public AIDebugSystem(Entity player, List<BuffStateComponent> buffStateComponents, List<SkillBaseComponent> skillBaseComponents)
        {
            this.player = player;
            buffStateComponent = buffStateComponents.Find(x => x.Parent == player);
            skillBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.StraightShot);

        }

        public void Update(ulong timer, Keyboard keyboard)
        {
            if(buffStateComponent.BuffList.Find(x => x.SkillSource == skillBaseComp.Parent && x.IsActive && x.Type == AttributeType.CriticalHitRate) != null)
            {
                keyboard.keysDictionary[Keys.Num1] = true;
                keyboard.keysDictionary[Keys.Num2] = false;
            }
            else
            {
                keyboard.keysDictionary[Keys.Num1] = false;
                keyboard.keysDictionary[Keys.Num2] = true;
            }
        }
    }
}

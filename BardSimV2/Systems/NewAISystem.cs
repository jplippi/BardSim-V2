using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class NewAISystem : ISystem
    {
        Entity player;
        ModifierStateComponent modStateComponent;
        OverTimeStateComponent targOtComp;
        SkillBaseComponent straightShotBaseComp;
        SkillBaseComponent barrageBaseComp;
        SkillBaseComponent heavyShotBaseComp;
        SkillBaseComponent wmBaseComp;
        SkillBaseComponent mbBaseComp;
        SkillBaseComponent apBaseComp;
        SkillBaseComponent eaBaseComp;
        SkillBaseComponent blBaseComp;
        SkillBaseComponent rsBaseComp;
        SkillBaseComponent ppBaseComp;
        SkillBaseComponent swBaseComp;
        CooldownComponent barrageCooldownComp;
        CooldownComponent heavyShotCdComp;
        CooldownComponent wmCooldownComp;
        CooldownComponent mbCooldownComp;
        CooldownComponent apCooldownComp;
        CooldownComponent eaCooldownComp;
        CooldownComponent blCooldownComp;
        CooldownComponent rsCooldownComp;
        CooldownComponent ppCooldownComp;
        CooldownComponent swCooldownComp;
        TargetComponent targComp;
        BardComponent brdComp;

        bool onOpener = true;


        public NewAISystem(Entity player, List<BardComponent> bardComponents, List<CooldownComponent> cooldownComponents, List<ModifierStateComponent> modifierStateComponents, List<OverTimeStateComponent> overtimeStateComponents, List<SkillBaseComponent> skillBaseComponents, List<TargetComponent> targetComponents)
        {
            this.player = player;
            heavyShotBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.HeavyShot);
            heavyShotCdComp = cooldownComponents.Find(x => x.Parent == heavyShotBaseComp.Parent);
            modStateComponent = modifierStateComponents.Find(x => x.Parent == player);
            straightShotBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.StraightShot);
            barrageBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.Barrage);
            barrageCooldownComp = cooldownComponents.Find(x => x.Parent == barrageBaseComp.Parent);
            wmBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.TheWanderersMinuet);
            wmCooldownComp = cooldownComponents.Find(x => x.Parent == wmBaseComp.Parent);
            mbBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.MagesBallad);
            mbCooldownComp = cooldownComponents.Find(x => x.Parent == mbBaseComp.Parent);
            apBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.ArmysPaeon);
            apCooldownComp = cooldownComponents.Find(x => x.Parent == apBaseComp.Parent);
            eaBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.EmpyrealArrow);
            eaCooldownComp = cooldownComponents.Find(x => x.Parent == eaBaseComp.Parent);
            blBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.Bloodletter);
            blCooldownComp = cooldownComponents.Find(x => x.Parent == blBaseComp.Parent);
            rsBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.RagingStrikes);
            rsCooldownComp = cooldownComponents.Find(x => x.Parent == rsBaseComp.Parent);
            ppBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.PitchPerfect);
            ppCooldownComp = cooldownComponents.Find(x => x.Parent == ppBaseComp.Parent);
            swBaseComp = skillBaseComponents.Find(x => x.Name == SkillName.Sidewinder);
            swCooldownComp = cooldownComponents.Find(x => x.Parent == swBaseComp.Parent);
            targComp = targetComponents.Find(x => x.Parent == player);
            targOtComp = overtimeStateComponents.Find(x => x.Parent == targComp.Target);
            brdComp = bardComponents.Find(x => x.Parent == player);

        }

        public void Update(decimal timer, Keyboard keyboard)
        {
            // GCD: Base structure
            if (!IsDoTActive(targOtComp, DotName.StormBite))
            {
                UseKey(keyboard, Keys.Num3);
            }
            else if (!IsDoTActive(targOtComp, DotName.CausticBite))
            {
                UseKey(keyboard, Keys.Num4);
            }
            else if (IsDoTActive(targOtComp, DotName.CausticBite) && IsDoTActive(targOtComp, DotName.StormBite) && (TimeLeftOnDot(targOtComp, DotName.CausticBite, timer) <= 3m || TimeLeftOnDot(targOtComp, DotName.StormBite, timer) <= 3m))
            {
                UseKey(keyboard, Keys.Num5);
            }
            else if (IsStatusActive(modStateComponent, StatusName.StraighterShot) && CooldownLeft(barrageCooldownComp, timer) < 10m)
            {
                UseKey(keyboard, Keys.F3);
            }
            else if (!IsStatusActive(modStateComponent, StatusName.StraightShot) || TimeLeftOnStatus(modStateComponent, StatusName.StraightShot, timer) <= 5m)
            {
                UseKey(keyboard, Keys.Num2);
            }
            else
            {
                UseKey(keyboard, Keys.Num1);
            }

        }

        void UseKey(Keyboard keyboard, Keys key)
        {

            foreach (Keys k in Enum.GetValues(typeof(Keys)))
            {
                if (k == key)
                {
                    keyboard.keysDictionary[k] = true;
                }
                else
                {
                    keyboard.keysDictionary[k] = false;
                }
            }
        }

        decimal NextGCD(CooldownComponent heavyShotCdComp)
        {
            return heavyShotCdComp.UsableAt;
        }

        decimal CooldownLeft(CooldownComponent cdComp, decimal timer)
        {
            return Math.Min(0, cdComp.UsableAt - timer);
        }

        // Conditions
        bool IsStatusActive(ModifierStateComponent modComp, StatusName name)
        {
            foreach (Buff b in modComp.BuffList)
            {
                if (b.Name == name && b.IsActive)
                {
                    return true;
                }
            }

            foreach (Enabler e in modComp.EnablerList)
            {
                if (e.Name == name)
                {
                    return true;
                }
            }

            return false;

        }

        decimal TimeLeftOnStatus(ModifierStateComponent modComp, StatusName name, decimal timer)
        {
            foreach (Buff b in modComp.BuffList)
            {
                if (b.Name == name && b.IsActive)
                {
                    return b.Duration - timer + b.Start;
                }
            }

            foreach (Enabler e in modComp.EnablerList)
            {
                if (e.Name == name)
                {
                    return e.Duration - timer + e.Start;
                }
            }

            // If no buff was found
            throw new Exception("No buff found.");
        }

        bool IsDoTActive(OverTimeStateComponent targOtComp, DotName name)
        {
            foreach (DoT d in targOtComp.DotList)
            {
                if (d.Name == name && d.IsActive == true)
                {
                    return true;
                }
            }
            return false;
        }

        decimal TimeLeftOnDot(OverTimeStateComponent targOtComp, DotName name, decimal timer)
        {
            foreach (DoT d in targOtComp.DotList)
            {
                if (d.Name == name && d.IsActive == true)
                {
                    return d.Duration - timer + d.Start;
                }
            }

            // If no buff was found
            throw new Exception("No dot found.");
        }
    }
}

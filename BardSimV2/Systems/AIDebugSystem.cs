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
        SkillBaseComponent heavyShotBaseComp;
        SkillBaseComponent wmBaseComp;
        SkillBaseComponent mbBaseComp;
        SkillBaseComponent apBaseComp;
        SkillBaseComponent eaBaseComp;
        SkillBaseComponent blBaseComp;
        SkillBaseComponent rsBaseComp;
        CooldownComponent barrageCooldownComp;
        CooldownComponent heavyShotCdComp;
        CooldownComponent wmCooldownComp;
        CooldownComponent mbCooldownComp;
        CooldownComponent apCooldownComp;
        CooldownComponent eaCooldownComp;
        CooldownComponent blCooldownComp;
        CooldownComponent rsCooldownComp;
        TargetComponent targComp;
        BardComponent brdComp;


        public AIDebugSystem(Entity player, List<BardComponent> bardComponents, List<CooldownComponent> cooldownComponents, List<ModifierStateComponent> modifierStateComponents, List<OverTimeStateComponent> overtimeStateComponents, List<SkillBaseComponent> skillBaseComponents, List<TargetComponent> targetComponents)
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
            targComp = targetComponents.Find(x => x.Parent == player);
            targOtComp = overtimeStateComponents.Find(x => x.Parent == targComp.Target);
            brdComp = bardComponents.Find(x => x.Parent == player);

        }

        public void Update(decimal timer, Keyboard keyboard)
        {
            if(!(timer <= NextGCD(heavyShotCdComp) - Constants.AnimationLock) || NextGCD(heavyShotCdComp) <= 0m)
            {
                if (targOtComp.DotList.Find(x => x.Name == DotName.StormBite && x.Duration - timer + x.Start < 4m) != null || targOtComp.DotList.Find(x => x.Name == DotName.CausticBite && x.Duration - timer + x.Start < 4m) != null)
                {
                    UseKey(keyboard, Keys.Num5);
                }
                else if (modStateComponent.EnablerList.Find(x => x.Name == StatusName.StraighterShot) != null)
                {
                    UseKey(keyboard, Keys.F3);
                }
                else if (modStateComponent.BuffList.Find(x => x.IsActive && x.Name == StatusName.StraighterShot) == null)
                {
                    UseKey(keyboard, Keys.Num2);
                }
                else if (targOtComp.DotList.Find(x => x.IsActive == true && x.Name == DotName.StormBite) == null)
                {
                    UseKey(keyboard, Keys.Num3);
                }
                else if (targOtComp.DotList.Find(x => x.IsActive == true && x.Name == DotName.CausticBite) == null)
                {
                    UseKey(keyboard, Keys.Num4);
                }
                else
                {
                    UseKey(keyboard, Keys.Num1);
                }
            }
            else
            {
                if (brdComp.Song != SongName.TheWanderersMinuet && wmCooldownComp.UsableAt <= timer && apCooldownComp.UsableAt - timer <= 60m)
                {
                    UseKey(keyboard, Keys.M1);
                }
                else if (brdComp.Song != SongName.MagesBallad && mbCooldownComp.UsableAt <= timer && wmCooldownComp.UsableAt - timer <= 50m)
                {
                    UseKey(keyboard, Keys.M2);
                }
                else if (brdComp.Song != SongName.ArmysPaeon && apCooldownComp.UsableAt <= timer && mbCooldownComp.UsableAt - timer <= 50m && wmCooldownComp.UsableAt - timer <= 20m)
                {
                    UseKey(keyboard, Keys.M3);
                }
                else if (blCooldownComp.UsableAt <= timer)
                {
                    UseKey(keyboard, Keys.R);
                }
                else if(rsCooldownComp.UsableAt <= timer)
                {
                    UseKey(keyboard, Keys.F1);
                }
                else if (modStateComponent.EnablerList.Find(x => x.Name == StatusName.StraighterShot) != null && barrageCooldownComp.UsableAt <= timer && !(targOtComp.DotList.Find(x => x.Name == DotName.StormBite && x.Duration - timer + x.Start < 4m) != null || targOtComp.DotList.Find(x => x.Name == DotName.CausticBite && x.Duration - timer + x.Start < 4m) != null))
                {
                    UseKey(keyboard, Keys.F2);
                }else if (!(modStateComponent.EnablerList.Find(x => x.Name == StatusName.StraighterShot) != null && modStateComponent.EnablerList.Find(x => x.Name == StatusName.Barrage) != null) && eaCooldownComp.UsableAt <= timer)
                {
                    UseKey(keyboard, Keys.Num6);
                }
            }




            //if(targOtComp.DotList.Find(x => x.Name == DotName.StormBite && x.Duration - timer + x.Start < 4m) != null || targOtComp.DotList.Find(x => x.Name == DotName.CausticBite && x.Duration - timer + x.Start < 4m) != null)
            //{
            //    UseKey(keyboard, Keys.Num5);
            //}
            //else if(modStateComponent.EnablerList.Find(x => x.Name == StatusName.StraighterShot) != null && barrageCooldownComp.UsableAt <= timer)
            //{
            //    UseKey(keyboard, Keys.F2);
            //}
            //else if(modStateComponent.EnablerList.Find(x => x.Name == StatusName.StraighterShot) != null)
            //{
            //    UseKey(keyboard, Keys.F3);
            //}
            //else if(modStateComponent.BuffList.Find(x => x.IsActive && x.Name == StatusName.StraighterShot) == null)
            //{
            //    UseKey(keyboard, Keys.Num2);
            //}
            //else if(targOtComp.DotList.Find(x => x.IsActive == true && x.Name == DotName.StormBite) == null)
            //{
            //    UseKey(keyboard, Keys.Num3);
            //}
            //else if(targOtComp.DotList.Find(x => x.IsActive == true && x.Name == DotName.CausticBite) == null)
            //{
            //    UseKey(keyboard, Keys.Num4);
            //}
            //else
            //{
            //    UseKey(keyboard, Keys.Num1);
            //}
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

        decimal NextGCD(CooldownComponent heavyShotCdComp)
        {
            return heavyShotCdComp.UsableAt;
        }
    }
}

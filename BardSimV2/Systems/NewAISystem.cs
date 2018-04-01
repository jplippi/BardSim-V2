﻿using ExtensionMethods;
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

        public void Update(decimal timer, Keyboard keyboard, ref int gcdCounter)
        {
            if (timer == NextGCD(heavyShotCdComp))
            {
                gcdCounter++;
            }

            // Opener:
            if (gcdCounter <= 9)
            {
                if (NextGCD(heavyShotCdComp) <= timer)
                {
                    // GCD: opener
                    switch (gcdCounter)
                    {
                        case 1:
                            UseKey(keyboard, Keys.Num3);
                            break;
                        case 2:
                            UseKey(keyboard, Keys.Num4);
                            break;
                        case 3:
                            UseKey(keyboard, Keys.Num2);
                            break;
                        case 4:
                            UseKey(keyboard, Keys.Num5);
                            break;
                        case 5:
                            UseKey(keyboard, Keys.Num1);
                            break;
                        case 6:
                        case 7:
                        case 8:
                            if (IsStatusActive(modStateComponent, StatusName.StraighterShot))
                            {
                                UseKey(keyboard, Keys.F3);
                            }
                            else
                            {
                                UseKey(keyboard, Keys.Num1);
                            }
                            break;
                        case 9:
                            UseKey(keyboard, Keys.Num5);
                            break;
                    }
                }
                else if (timer < NextGCD(heavyShotCdComp))
                {
                    // oGCD: Base structure
                    switch (gcdCounter)
                    {
                        case 1:
                            if (CooldownLeft(blCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.R);
                            }
                            else if (CooldownLeft(rsCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.F1);
                            }
                            else
                            {
                                ReleaseKeys(keyboard);
                            }
                            break;
                        case 2:
                            if (CooldownLeft(wmCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.M1);
                            }
                            else if (CooldownLeft(eaCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.Num6);
                            }
                            else
                            {
                                ReleaseKeys(keyboard);
                            }
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            if (Repertoire(brdComp) == 3 && CooldownLeft(ppCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.T);
                            }
                            else if (IsStatusActive(modStateComponent, StatusName.StraighterShot) && CooldownLeft(barrageCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.F2);
                            }
                            else if (CooldownLeft(eaCooldownComp, timer) <= 0 && CooldownLeft(barrageCooldownComp, timer) > 0 && !IsStatusActive(modStateComponent, StatusName.Barrage) && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.Num6);
                            }
                            else if (CooldownLeft(swCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.Num7);
                            }
                            else if (CooldownLeft(blCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.R);
                            }
                            else
                            {
                                ReleaseKeys(keyboard);
                            }
                            break;
                        case 8:
                            if (Repertoire(brdComp) == 3 && CooldownLeft(ppCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.T);
                            }
                            else if (CooldownLeft(eaCooldownComp, timer) <= 0 && CooldownLeft(barrageCooldownComp, timer) > 0 && !IsStatusActive(modStateComponent, StatusName.Barrage) && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.Num6);
                            }
                            else if (CooldownLeft(swCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.Num7);
                            }
                            else if (CooldownLeft(blCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.R);
                            }
                            else
                            {
                                ReleaseKeys(keyboard);
                            }
                            break;
                        case 9:
                            if (Repertoire(brdComp) == 3 && CooldownLeft(ppCooldownComp, timer) <= 0 && NoClip(timer))
                            {
                                UseKey(keyboard, Keys.T);
                            }
                            else if (CooldownLeft(barrageCooldownComp, timer) <= 0 && !IsStatusActive(modStateComponent, StatusName.StraighterShot))
                            {
                                UseKey(keyboard, Keys.F2);
                            }
                            else if ((IsStatusActive(modStateComponent, StatusName.Barrage) && !IsStatusActive(modStateComponent, StatusName.StraighterShot) && CooldownLeft(eaCooldownComp, timer) <= 0) || (CooldownLeft(barrageCooldownComp,timer) > 0 && NoClip(timer) && CooldownLeft(eaCooldownComp, timer) <= 0))
                            {
                                UseKey(keyboard, Keys.Num6);
                            }
                            else
                            {
                                ReleaseKeys(keyboard);
                            }
                            break;
                    }
                }
            }
            else
            {
                if (TimeLeftOnSong(brdComp, SongName.TheWanderersMinuet, timer) > 0)
                {
                    if (NextGCD(heavyShotCdComp) <= timer)
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
                        else if (IsStatusActive(modStateComponent, StatusName.StraighterShot) && CooldownLeft(barrageCooldownComp, timer) >= 10m)
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
                    else if (timer < NextGCD(heavyShotCdComp))
                    {
                        // oGCD: Base structure

                        // Mage's Ballad
                        if (TimeLeftOnSong(brdComp, SongName.TheWanderersMinuet, timer) <= 2m && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.M2);
                        }
                        // Raging Strikes
                        else if (CooldownLeft(rsCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.F1);
                        }
                        // Pitch Perfect
                        else if ((Repertoire(brdComp) == 3 || (IsSongActive(brdComp, SongName.TheWanderersMinuet) && TimeLeftOnSong(brdComp, SongName.TheWanderersMinuet, timer) <= 4m)) && CooldownLeft(ppCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.T);
                        }
                        // Barrage
                        else if ((IsStatusActive(modStateComponent, StatusName.StraighterShot) || IsStatusActive(modStateComponent, StatusName.RagingStrikes) && TimeLeftOnStatus(modStateComponent, StatusName.RagingStrikes, timer) <= 3m) && CooldownLeft(barrageCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.F2);
                        }
                        // Bloodletter
                        else if (CooldownLeft(blCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.R);
                        }
                        // Empyreal Arrow
                        else if (IsStatusActive(modStateComponent, StatusName.RagingStrikes) && TimeLeftOnStatus(modStateComponent, StatusName.RagingStrikes, timer) <= 15m && IsStatusActive(modStateComponent, StatusName.Barrage) && CooldownLeft(eaCooldownComp, timer) <= 0 /* CAN CLIP */)
                        {
                            UseKey(keyboard, Keys.Num6);
                        }
                        // Empyreal Arrow
                        else if ((IsStatusActive(modStateComponent, StatusName.RagingStrikes) && TimeLeftOnStatus(modStateComponent, StatusName.RagingStrikes, timer) > 15m ) || !IsStatusActive(modStateComponent, StatusName.RagingStrikes) && CooldownLeft(eaCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.Num6);
                        }
                        // Sidewinder
                        else if (IsDoTActive(targOtComp, DotName.CausticBite) && IsDoTActive(targOtComp, DotName.StormBite) && CooldownLeft(swCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.Num7);
                        }
                        else
                        {
                            ReleaseKeys(keyboard);
                        }
                    }
                    else
                    {
                        ReleaseKeys(keyboard);
                    }

                }
                else if (TimeLeftOnSong(brdComp, SongName.MagesBallad, timer) > 0)
                {
                    if (NextGCD(heavyShotCdComp) <= timer)
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
                        else if (IsStatusActive(modStateComponent, StatusName.StraighterShot) && CooldownLeft(barrageCooldownComp, timer) >= 10m)
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
                    else if (timer < NextGCD(heavyShotCdComp))
                    {
                        // oGCD: Base structure

                        // Army's Paeon
                        if (CooldownLeft(apCooldownComp, timer) <= 0 && CooldownLeft(wmCooldownComp, timer) <= 20 && CooldownLeft(mbCooldownComp, timer) <= 50 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.M3);
                        }
                        // Bloodletter
                        else if (CooldownLeft(blCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.R);
                        }
                        // Empyreal Arrow
                        else if (CooldownLeft(blCooldownComp, timer) > 3m && CooldownLeft(eaCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.Num6);
                        }
                        // Sidewinder
                        else if (IsDoTActive(targOtComp, DotName.CausticBite) && IsDoTActive(targOtComp, DotName.StormBite) && CooldownLeft(swCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.Num7);
                        }
                        else
                        {
                            ReleaseKeys(keyboard);
                        }
                    }
                    else
                    {
                        ReleaseKeys(keyboard);
                    }
                }
                else if (TimeLeftOnSong(brdComp, SongName.ArmysPaeon, timer) > 0)
                {
                    if (NextGCD(heavyShotCdComp) <= timer)
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
                        else if (IsStatusActive(modStateComponent, StatusName.StraighterShot) && CooldownLeft(barrageCooldownComp, timer) >= 10m)
                        {
                            UseKey(keyboard, Keys.F3);
                        }
                        else if (!IsStatusActive(modStateComponent, StatusName.StraightShot) || TimeLeftOnStatus(modStateComponent, StatusName.StraightShot, timer) <= 5m || ((TimeLeftOnStatus(modStateComponent, StatusName.StraightShot, timer) - CooldownLeft(rsCooldownComp, timer) - 20m) <= 5m && CooldownLeft(rsCooldownComp, timer) <= 5m))
                        {
                            UseKey(keyboard, Keys.Num2);
                        }
                        else
                        {
                            UseKey(keyboard, Keys.Num1);
                        }
                    }
                    else if (timer < NextGCD(heavyShotCdComp))
                    {
                        // oGCD: Base structure

                        // Wanderer's Minuet
                        if (CooldownLeft(wmCooldownComp, timer) <= 0m && CooldownLeft(mbCooldownComp, timer) <= 30 && CooldownLeft(apCooldownComp, timer) <= 60 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.M1);
                        }
                        // Bloodletter
                        else if (CooldownLeft(blCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.R);
                        }
                        // Empyreal Arrow
                        else if (CooldownLeft(blCooldownComp, timer) > 3m && CooldownLeft(eaCooldownComp, timer) <= 0 && NoClip(timer) && TimeLeftOnSong(brdComp, SongName.ArmysPaeon, timer) > 15)
                        {
                            UseKey(keyboard, Keys.Num6);
                        }
                        // Sidewinder
                        else if (IsDoTActive(targOtComp, DotName.CausticBite) && IsDoTActive(targOtComp, DotName.StormBite) && CooldownLeft(swCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.Num7);
                        }
                        else
                        {
                            ReleaseKeys(keyboard);
                        }
                    }
                    else
                    {
                        ReleaseKeys(keyboard);
                    }
                }
                else
                {
                    if (NextGCD(heavyShotCdComp) <= timer)
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
                        else if (IsStatusActive(modStateComponent, StatusName.StraighterShot) && CooldownLeft(barrageCooldownComp, timer) >= 10m)
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
                    else if (timer < NextGCD(heavyShotCdComp))
                    {
                        // oGCD: Base structure
                        // The Wanderer's Minuet
                        if(IsSongActive(brdComp, SongName.None) && CooldownLeft(wmCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.M1);
                        }
                        // Mage's Ballad
                        else if (IsSongActive(brdComp, SongName.None) && CooldownLeft(mbCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.M2);
                        }
                        // Army's Paeon
                        else if (IsSongActive(brdComp, SongName.None) && CooldownLeft(apCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.M3);
                        }
                        // Bloodletter
                        else if (CooldownLeft(blCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.R);
                        }
                        // Sidewinder
                        else if (IsDoTActive(targOtComp, DotName.CausticBite) && IsDoTActive(targOtComp, DotName.StormBite) && CooldownLeft(swCooldownComp, timer) <= 0 && NoClip(timer))
                        {
                            UseKey(keyboard, Keys.Num7);
                        }
                        else
                        {
                            ReleaseKeys(keyboard);
                        }
                    }
                    else
                    {
                        ReleaseKeys(keyboard);
                    }
                }
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

        void ReleaseKeys(Keyboard keyboard)
        {
            foreach (Keys k in Enum.GetValues(typeof(Keys)))
            {
                 keyboard.keysDictionary[k] = false;
            }
        }

        decimal NextGCD(CooldownComponent heavyShotCdComp)
        {
            return heavyShotCdComp.UsableAt;
        }

        decimal CooldownLeft(CooldownComponent cdComp, decimal timer)
        {
            return Math.Max(0, cdComp.UsableAt - timer);
        }

        // Conditions
        bool NoClip(decimal timer)
        {
            return timer < NextGCD(heavyShotCdComp) - Constants.AnimationLock;
        }


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

        decimal TimeLeftOnSong(BardComponent brdComp, SongName song, decimal timer)
        {
            if(brdComp.Song == song)
            {
                 return Math.Max(0, brdComp.SongDuration - (timer - brdComp.SongStart));
            }
            return 0;
        }

        bool IsSongActive(BardComponent brdComp, SongName song)
        {
            return brdComp.Song == song;
        }

        int Repertoire(BardComponent brdComp)
        {
            return brdComp.Repertoire;
        }
    }
}
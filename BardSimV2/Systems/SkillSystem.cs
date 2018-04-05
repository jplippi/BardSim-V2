using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class SkillSystem : ISystem
    {
        List<AnimationLockComponent> animationLockComponents;
        List<AttributesComponent> attributesComponents;
        List<BardComponent> bardComponents;
        List<ConditionalPotencyComponent> conditionalPotencyComponents;
        List<CooldownComponent> cooldownComponents;
        List<DotEffectComponent> dotEffectComponents;
        List<EnhancedEmpyrealArrowComponent> enhancedEmpyrealArrowComponents;
        List<GenericStatusEffectComponent> genericStatusEffectComponents;
        List<HealthComponent> healthComponents;
        List<IronJawsEffectComponent> ironJawsEffectComponents;
        List<KeyMappingComponent> keyMappingComponents;
        List<ModifierStateComponent> modifierStateComponents;
        List<OverTimeStateComponent> overtimeStateComponents;
        List<PotencyComponent> potencyComponents;
        List<SkillBaseComponent> skillBaseComponents;
        List<SongComponent> songComponents;
        List<StatusEffectComponent> statusEffectComponents;
        List<StraighterShotEffectComponent> straighterShotEffectComponents;
        List<TargetComponent> targetComponents;
        List<UseConditionComponent> useConditionComponents;
        List<UsesEnablerComponent> usesEnablerComponents;
        List<UsesRepertoireComponent> usesRepertoireComponents;

        Random rng;


        public SkillSystem(List<AnimationLockComponent> animationLockComponents, List<AttributesComponent> attributesComponents, List<BardComponent> bardComponents, List<ConditionalPotencyComponent> conditionalPotencyComponents, List<CooldownComponent> cooldownComponents, List<DotEffectComponent> dotEffectComponents, List<EnhancedEmpyrealArrowComponent> enhancedEmpyrealArrowComponents, List<GenericStatusEffectComponent> genericStatusEffectComponents, List <HealthComponent> healthComponents, List<IronJawsEffectComponent> ironJawsEffectComponents, List<KeyMappingComponent> keyMappingComponents, List<ModifierStateComponent> modifierStateComponents, List<OverTimeStateComponent> overtimeStateComponents, List<PotencyComponent> potencyComponents, List<SkillBaseComponent> skillBaseComponents, List<SongComponent> songComponents, List<StatusEffectComponent> statusEffectComponents, List<StraighterShotEffectComponent> straighterShotEffectComponents, List<TargetComponent> targetComponents, List<UseConditionComponent> useConditionComponents, List<UsesEnablerComponent> usesEnablerComponents, List<UsesRepertoireComponent> usesRepertoireComponents)
        {
            this.animationLockComponents = animationLockComponents;
            this.attributesComponents = attributesComponents;
            this.bardComponents = bardComponents;
            this.conditionalPotencyComponents = conditionalPotencyComponents;
            this.cooldownComponents = cooldownComponents;
            this.dotEffectComponents = dotEffectComponents;
            this.enhancedEmpyrealArrowComponents = enhancedEmpyrealArrowComponents;
            this.genericStatusEffectComponents = genericStatusEffectComponents;
            this.healthComponents = healthComponents;
            this.ironJawsEffectComponents = ironJawsEffectComponents;
            this.keyMappingComponents = keyMappingComponents;
            this.modifierStateComponents = modifierStateComponents;
            this.overtimeStateComponents = overtimeStateComponents;
            this.potencyComponents = potencyComponents;
            this.skillBaseComponents = skillBaseComponents;
            this.songComponents = songComponents;
            this.statusEffectComponents = statusEffectComponents;
            this.straighterShotEffectComponents = straighterShotEffectComponents;
            this.targetComponents = targetComponents;
            this.useConditionComponents = useConditionComponents;
            this.usesEnablerComponents = usesEnablerComponents;
            this.usesRepertoireComponents = usesRepertoireComponents;
            rng = new Random();
        }

        public void Update(decimal timer, Keyboard keyboard)
        {
            foreach ( KeyMappingComponent keyMapComp in keyMappingComponents)
            {
                // User
                Entity entity = keyMapComp.Parent;

                AnimationLockComponent animLockComp = animationLockComponents.Find(x => x.Parent == entity);

                if (timer - animLockComp.Start >= Constants.AnimationLock)
                {
                    foreach (KeyBind keyBind in keyMapComp.KeyBinds )
                    {
                        if (keyBind.IsActive)
                        {

                            // Skill components
                            SkillBaseComponent skillBaseComp = skillBaseComponents.Find(x => x.Name == keyBind.Command);

                            if(skillBaseComp != null)
                            {
                            // Skill
                            Entity skill = skillBaseComp.Parent;
                            CooldownComponent cdComp = cooldownComponents.Find(x => x.Parent == skill);

                                // Checking if skill is on cooldown
                                if (cdComp.UsableAt <= timer)
                                {

                                    ModifierStateComponent modStateComp = modifierStateComponents.Find(x => x.Parent == entity);
                                    BardComponent brdComp = bardComponents.Find(x => x.Parent == entity);

                                    // Determining conditional use
                                    bool isUsable = true;

                                    foreach (UseConditionComponent useCondComp in useConditionComponents.FindAll(x => x.Parent == skill))
                                    {
                                        if (!useCondComp.Function(modStateComp, brdComp))
                                        {
                                            isUsable = false;
                                            break;
                                        }
                                    }

                                    if (isUsable)
                                    {
                                        // User Components
                                        AttributesComponent attComp = attributesComponents.Find(x => x.Parent == entity);

                                        // Skill list
                                        List<Entity> skillList = brdComp.SkillList;

                                        // Target components
                                        TargetComponent targComp = targetComponents.Find(x => x.Parent == entity);
                                        HealthComponent targHealthComp = healthComponents.Find(x => x.Parent == targComp.Target);
                                        ModifierStateComponent targModStateComp = modifierStateComponents.Find(x => x.Parent == targComp.Target);
                                        OverTimeStateComponent targOtComp = overtimeStateComponents.Find(x => x.Parent == targComp.Target);


                                        // More skill components
                                        PotencyComponent potComp = potencyComponents.Find(x => x.Parent == skill);
                                        ConditionalPotencyComponent condPotComp = conditionalPotencyComponents.Find(x => x.Parent == skill);

                                        decimal recast;

                                        if (potComp != null || condPotComp != null)
                                        {
                                            // If the skill is offensive
                                            decimal wdMod = 1;
                                            decimal apMod = 1;
                                            decimal detMod = 1;
                                            decimal tenMod = 1;
                                            decimal ssMod = 1;
                                            decimal traitMod = 1;
                                            decimal critMod1 = 1;
                                            decimal dhitMod1 = 1;
                                            decimal critMod2 = 1;
                                            decimal dhitMod2 = 1;
                                            decimal critMod3 = 1;
                                            decimal dhitMod3 = 1;

                                            decimal critChance;
                                            decimal dhitChance;

                                            
                                            Dictionary<DamageModifierType, decimal> modifiersDictionary;
                                            Dictionary<AttributeType, decimal> chancesDictionary;

                                            // Calculates weapon damage modifier
                                            wdMod = CombatFormulas.WeaponDamageMod(attComp.AttributesDictionary[AttributeType.WeaponDamage]);

                                            // Calculates attack power modifier
                                            if (brdComp.Job == Job.Paladin || brdComp.Job == Job.Warrior || brdComp.Job == Job.DarkKnight || brdComp.Job == Job.Monk || brdComp.Job == Job.Dragoon || brdComp.Job == Job.Samurai)
                                            {
                                                apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Strenght] + modStateComp.BuffDictionary[AttributeType.Strenght]);
                                            }
                                            else if (brdComp.Job == Job.Ninja || brdComp.Job == Job.Bard || brdComp.Job == Job.Machinist)
                                            {
                                                apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Dexterity] + modStateComp.BuffDictionary[AttributeType.Dexterity]);
                                            }
                                            else if (brdComp.Job == Job.BlackMage || brdComp.Job == Job.Summoner || brdComp.Job == Job.RedMage)
                                            {
                                                apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Intelligence] + modStateComp.BuffDictionary[AttributeType.Intelligence]);
                                            }
                                            else if (brdComp.Job == Job.WhiteMage || brdComp.Job == Job.Scholar || brdComp.Job == Job.Astrologian)
                                            {
                                                apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Mind] + modStateComp.BuffDictionary[AttributeType.Mind]);
                                            }

                                            // Calculates determination modifier
                                            detMod = CombatFormulas.DeterminationDamageMod(attComp.AttributesDictionary[AttributeType.Determination] + modStateComp.BuffDictionary[AttributeType.Determination]);

                                            // Calculates tenacity modifier
                                            tenMod = CombatFormulas.TenacityDamageMod(attComp.AttributesDictionary[AttributeType.Tenacity] + modStateComp.BuffDictionary[AttributeType.Tenacity]);

                                            // Calculates speed modifier
                                            if (brdComp.Job == Job.BlackMage || brdComp.Job == Job.Summoner || brdComp.Job == Job.RedMage || brdComp.Job == Job.WhiteMage || brdComp.Job == Job.Scholar || brdComp.Job == Job.Astrologian)
                                            {
                                                ssMod = CombatFormulas.SpeedDamageMod(attComp.AttributesDictionary[AttributeType.SpellSpeed] + modStateComp.BuffDictionary[AttributeType.SpellSpeed]);
                                            }
                                            else
                                            {
                                                ssMod = CombatFormulas.SpeedDamageMod(attComp.AttributesDictionary[AttributeType.SkillSpeed] + modStateComp.BuffDictionary[AttributeType.SkillSpeed]);
                                            }

                                            // Calculates trait modifier
                                            if (brdComp.Job == Job.Ninja || brdComp.Job == Job.Bard || brdComp.Job == Job.Machinist)
                                            {
                                                traitMod = 1.2m;
                                            }
                                            else if (brdComp.Job == Job.BlackMage || brdComp.Job == Job.Summoner || brdComp.Job == Job.RedMage || brdComp.Job == Job.WhiteMage || brdComp.Job == Job.Scholar || brdComp.Job == Job.Astrologian)
                                            {
                                                traitMod = 1.3m;
                                            }

                                            // Checks for straighter shot guaranteed crit
                                            Enabler straighterShot = modStateComp.EnablerList.Find(x => x.Name == StatusName.StraighterShot);
                                            if (skillBaseComp.Name == SkillName.StraightShot && straighterShot != null)
                                            {
                                                critChance = 100;
                                                modStateComp.EnablerList.Remove(straighterShot);
                                            }
                                            else
                                            {
                                                critChance = CombatFormulas.CriticalHitRate(attComp.AttributesDictionary[AttributeType.CriticalHit]) + modStateComp.BuffDictionary[AttributeType.CriticalHitRate];
                                            }

                                            dhitChance = CombatFormulas.DirectHitRate(attComp.AttributesDictionary[AttributeType.DirectHit]) + modStateComp.BuffDictionary[AttributeType.DirectHitRate];

                                            // Calculates crit modifier
                                            critMod1 = CombatFormulas.CriticalHitDamageMod(attComp.AttributesDictionary[AttributeType.CriticalHit] + modStateComp.BuffDictionary[AttributeType.CriticalHit]);
                                            critMod2 = critMod1;
                                            critMod3 = critMod1;

                                            // Calculates dhit modifier
                                            dhitMod1 = CombatFormulas.DirectHitDamageMod(attComp.AttributesDictionary[AttributeType.DirectHit] + modStateComp.BuffDictionary[AttributeType.DirectHit]);
                                            dhitMod2 = dhitMod1;
                                            dhitMod3 = dhitMod1;

                                            // Compilates the chances into a dictionary:
                                            chancesDictionary = new Dictionary<AttributeType, decimal>
                                                {
                                                    { AttributeType.CriticalHitRate, critChance },
                                                    { AttributeType.DirectHitRate, dhitChance }
                                                };

                                            // Compilates all the modifiers into a dictionary:
                                            modifiersDictionary = new Dictionary<DamageModifierType, decimal>
                                                {
                                                    { DamageModifierType.WeaponDamageModifier, wdMod },
                                                    { DamageModifierType.AttackPowerModifier, apMod },
                                                    { DamageModifierType.DeterminationModifier, detMod },
                                                    { DamageModifierType.TenacityModifier, tenMod },
                                                    { DamageModifierType.TraitModifier, traitMod },
                                                    { DamageModifierType.SpeedModifier, ssMod },
                                                    { DamageModifierType.CriticalModifier, critMod1 },
                                                    { DamageModifierType.DirectModifier, dhitMod1 }
                                                };
                                            decimal totalDamage1 = 0;
                                            decimal totalDamage2 = 0;
                                            decimal totalDamage3 = 0;

                                            decimal potMod = 1;
                                            int potency = 0;

                                            // Determining potency:
                                            if(potComp != null)
                                            {
                                                potency = potComp.Amount;
                                            }
                                            else if(condPotComp != null)
                                            {
                                                potency = condPotComp.Function(entity, targOtComp, brdComp);
                                            }

                                            // Starts calculating potency modifier from base potency
                                            potMod = CombatFormulas.PotencyMod(potency);

                                            // If not critical hit, modifier is 1
                                            if ((int)(critChance * 10) < rng.Next(1, 1000))
                                            {
                                                critMod1 = 1;
                                            }

                                            // If not direct hit, modifier is 1
                                            if ((int)(dhitChance * 10) < rng.Next(1, 1000))
                                            {
                                                dhitMod1 = 1;
                                            }

                                            totalDamage1 = CombatFormulas.DirectDamage(potMod, wdMod, apMod, detMod, tenMod, traitMod, critMod1, dhitMod1, modStateComp.BuffList);


                                            // Checks for barrage
                                            Enabler barrage = modStateComp.EnablerList.Find(x => x.Name == StatusName.Barrage);
                                            if (skillBaseComp.Type == SkillType.Weaponskill && barrage != null)
                                            {
                                                // Rolls crit and dhit for two more hits

                                                // If not critical hit, modifier is 1
                                                if ((int)(critChance * 10) < rng.Next(1, 1000))
                                                {
                                                    critMod2 = 1;

                                                }

                                                // If not direct hit, modifier is 1
                                                if ((int)(dhitChance * 10) < rng.Next(1, 1000))
                                                {
                                                    dhitMod2 = 1;
                                                }

                                                // If not critical hit, modifier is 1
                                                if ((int)(critChance * 10) < rng.Next(1, 1000))
                                                {
                                                    critMod3 = 1;

                                                }

                                                // If not direct hit, modifier is 1
                                                if ((int)(dhitChance * 10) < rng.Next(1, 1000))
                                                {
                                                    dhitMod3 = 1;
                                                }

                                                // Calculates two more hits
                                                totalDamage2 = CombatFormulas.DirectDamage(potMod, wdMod, apMod, detMod, tenMod, traitMod, critMod2, dhitMod2, modStateComp.BuffList);
                                                totalDamage3 = CombatFormulas.DirectDamage(potMod, wdMod, apMod, detMod, tenMod, traitMod, critMod3, dhitMod3, modStateComp.BuffList);

                                                // Inflicts damage on target's health component
                                                targHealthComp.Amount -= totalDamage2;
                                                targHealthComp.DamageTaken += totalDamage3;

                                                // Removes the effect of barrage
                                                modStateComp.EnablerList.Remove(barrage);
                                            }

                                            // Inflicts damage on target's health component
                                            targHealthComp.Amount -= totalDamage1;
                                            targHealthComp.DamageTaken += totalDamage1;

                                            // Logic for over time effects
                                            foreach (DotEffectComponent dotEffectComp in dotEffectComponents.FindAll(x => x.Parent == skill))
                                            {
                                                DoT d = targOtComp.DotList.Find(x => x.UserSource == entity && x.Name == dotEffectComp.Name);

                                                // If there's already the same DoT applied, reapplies it
                                                if (d != null)
                                                {
                                                    d = new DoT(targComp.Target, entity, modifiersDictionary, chancesDictionary, modStateComp.BuffList, dotEffectComp.Name, dotEffectComp.Potency, dotEffectComp.Duration, timer, true);
                                                }
                                                //Otherwise, adds the dot
                                                else
                                                {
                                                    targOtComp.DotList.Add(new DoT(targComp.Target, entity, modifiersDictionary, chancesDictionary, modStateComp.BuffList, dotEffectComp.Name, dotEffectComp.Potency, dotEffectComp.Duration, timer, false));
                                                }
                                            }

                                            // Logic for straighter shot effects
                                            foreach (StraighterShotEffectComponent ssEffectComp in straighterShotEffectComponents.FindAll(x => x.Parent == skill))
                                            {
                                                // Effect is granted with a chance
                                                if ((int)(ssEffectComp.Probability * 10) > rng.Next(0, 1000))
                                                {
                                                    Enabler e = modStateComp.EnablerList.Find(x => x.User == entity && x.Name == ssEffectComp.Name);

                                                    // If there's already the same enabler applied, refresh the timer
                                                    if (e != null)
                                                    {
                                                        e.Start = timer;
                                                    }
                                                    // Otherwise, adds the enabler
                                                    else
                                                    {
                                                        modStateComp.EnablerList.Add(new Enabler(entity, ssEffectComp.Name, ssEffectComp.Duration, timer));
                                                    }
                                                }
                                            }

                                            // Logic for iron jaws effects
                                            foreach (IronJawsEffectComponent ijEffectComp in ironJawsEffectComponents.FindAll(x => x.Parent == skill))
                                            {
                                                for (int i = 0; i < targOtComp.DotList.Count; i++)
                                                {
                                                    DotEffectComponent ijDotEffectComp = ijEffectComp.DotList.Find(x => x.Name == targOtComp.DotList[i].Name);

                                                    // If the dot is present, reapplies it
                                                    if (ijDotEffectComp != null)
                                                    {
                                                        targOtComp.DotList[i] = new DoT(targComp.Target, entity, modifiersDictionary, chancesDictionary, modStateComp.BuffList, ijDotEffectComp.Name, ijDotEffectComp.Potency, ijDotEffectComp.Duration, timer, true);
                                                    }
                                                }
                                            }

                                            // Logic for song effects
                                            foreach (SongComponent songComp in songComponents.FindAll(x => x.Parent == skill))
                                            {
                                                // Logic for ending AP buff
                                                Buff apEffect = null;
                                                ModifierStateComponent brdModComp = modifierStateComponents.Find(x => x.Parent == brdComp.Parent);

                                                if (brdModComp != null)
                                                {
                                                    apEffect = brdModComp.BuffList.Find(x => x.Name == StatusName.ArmysPaeon);
                                                }

                                                if (apEffect != null)
                                                {
                                                    brdModComp.BuffDictionary[apEffect.Type] -= apEffect.Modifier;
                                                    brdModComp.BuffList.Remove(apEffect);
                                                }

                                                // Logic for resetting repertoire
                                                brdComp.Repertoire = 0;

                                                // Applying the song
                                                brdComp.Song = songComp.Song;
                                                brdComp.SongStart = timer;
                                                brdComp.SongDuration = songComp.Duration;
                                            }

                                            // Logic for enhanced empyreal arrow effects
                                            foreach (EnhancedEmpyrealArrowComponent eaComp in enhancedEmpyrealArrowComponents.FindAll(x => x.Parent == skill))
                                            {
                                                if (brdComp.Song != SongName.None)
                                                {
                                                    if (brdComp.Song == SongName.TheWanderersMinuet && brdComp.Repertoire < 3)
                                                    {
                                                        brdComp.Repertoire++;
                                                    }
                                                    else if (brdComp.Song == SongName.MagesBallad)
                                                    {
                                                        brdComp.Repertoire++;
                                                    }
                                                    else if (brdComp.Song == SongName.ArmysPaeon && brdComp.Repertoire < 4)
                                                    {
                                                        brdComp.Repertoire++;
                                                    }
                                                }
                                            }

                                            // Logic for consuming repertoire
                                            foreach (UsesRepertoireComponent repComp in usesRepertoireComponents.FindAll(x => x.Parent == skill))
                                            {
                                                brdComp.Repertoire = 0;
                                            }

                                            // Logic for consuming enabler
                                            foreach (UsesEnablerComponent enComp in usesEnablerComponents.FindAll(x => x.Parent == skill))
                                            {
                                                Enabler e = modStateComp.EnablerList.Find(x => x.Name == enComp.Name);

                                                if (e != null)
                                                {
                                                    modStateComp.EnablerList.Remove(e);
                                                }
                                            }
                                        }

                                        // Activates animation lock
                                        animLockComp.Start = timer;

                                        // Adjusts cooldown
                                        if (skillBaseComp.Type == SkillType.Weaponskill)
                                        {
                                            recast = CombatFormulas.SpeedRecast
                                                (
                                                    attComp.AttributesDictionary[AttributeType.SkillSpeed] + modStateComp.BuffDictionary[AttributeType.SkillSpeed],
                                                    cdComp.BaseRecast,
                                                    modStateComp.BuffDictionary[AttributeType.Arrow],
                                                    modStateComp.BuffDictionary[AttributeType.Haste],
                                                    modStateComp.BuffDictionary[AttributeType.FeyWind],
                                                    modStateComp.BuffDictionary[AttributeType.SpeedType1],
                                                    modStateComp.BuffDictionary[AttributeType.SpeedType2],
                                                    modStateComp.BuffDictionary[AttributeType.RiddleOfFire],
                                                    modStateComp.BuffDictionary[AttributeType.AstralUmbral]
                                                );
                                        }
                                        else if (skillBaseComp.Type == SkillType.Spell)
                                        {
                                            recast = CombatFormulas.SpeedRecast
                                                (
                                                    attComp.AttributesDictionary[AttributeType.SpellSpeed] + modStateComp.BuffDictionary[AttributeType.SpellSpeed],
                                                    cdComp.BaseRecast,
                                                    modStateComp.BuffDictionary[AttributeType.Arrow],
                                                    modStateComp.BuffDictionary[AttributeType.Haste],
                                                    modStateComp.BuffDictionary[AttributeType.FeyWind],
                                                    modStateComp.BuffDictionary[AttributeType.SpeedType1],
                                                    modStateComp.BuffDictionary[AttributeType.SpeedType2],
                                                    modStateComp.BuffDictionary[AttributeType.RiddleOfFire],
                                                    modStateComp.BuffDictionary[AttributeType.AstralUmbral]
                                                );
                                        }
                                        else
                                        {
                                            recast = cdComp.BaseRecast;
                                        }

                                        // Logic for cooldown
                                        foreach (Entity s in cdComp.SharedCooldownList)
                                        {
                                            CooldownComponent c = cooldownComponents.Find(x => x.Parent == s);
                                            c.Start = timer;
                                            c.UsableAt = c.Start + recast;

                                        }

                                        // Logic for buff effects
                                        foreach (StatusEffectComponent statusComp in statusEffectComponents.FindAll(x => x.Parent == skill))
                                        {
                                            Buff b = modStateComp.BuffList.Find(x => x.UserSource == entity && x.Name == statusComp.Name);

                                            // If there's already the same buff applied, refresh the timer
                                            if (b != null)
                                            {
                                                b.Start = timer;
                                            }
                                            // Otherwise, adds the buff
                                            else
                                            {
                                                if (statusComp.Actor == ActorType.Self)
                                                {
                                                    modStateComp.BuffList.Add(new Buff(entity, entity, statusComp.Name, statusComp.Type, statusComp.Duration, timer, statusComp.Modifier, false));
                                                }
                                                else if (statusComp.Actor == ActorType.Target)
                                                {
                                                    modStateComp.BuffList.Add(new Buff(targComp.Target, entity, statusComp.Name, statusComp.Type, statusComp.Duration, timer, statusComp.Modifier, false));
                                                }
                                            }
                                        }

                                        // Logic for generic buff effects
                                        foreach (GenericStatusEffectComponent genStatusComp in genericStatusEffectComponents.FindAll(x => x.Parent == skill))
                                        {
                                            Enabler e = modStateComp.EnablerList.Find(x => x.User == entity && x.Name == genStatusComp.Name);

                                            // If there's already the same enabler applied, refresh the timer
                                            if (e != null)
                                            {
                                                e.Start = timer;
                                            }
                                            // Otherwise, adds the enabler
                                            else
                                            {
                                                modStateComp.EnablerList.Add(new Enabler(entity, genStatusComp.Name, genStatusComp.Duration, timer));
                                            }
                                        }

                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}

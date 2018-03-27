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
        List<AttributesComponent> attributesComponents;
        List<CooldownComponent> cooldownComponents;
        List<DotEffectComponent> dotEffectComponents;
        List<HealthComponent> healthComponents;
        List<JobComponent> jobComponents;
        List<KeyMappingComponent> keyMappingComponents;
        List<ModifierStateComponent> modifierStateComponents;
        List<OverTimeStateComponent> overtimeStateComponents;
        List<PotencyComponent> potencyComponents;
        List<SkillBaseComponent> skillBaseComponents;
        List<StatusEffectComponent> statusEffectComponents;
        List<TargetComponent> targetComponents;

        Random rng;


        public SkillSystem(List<AttributesComponent> attributesComponents, List<CooldownComponent> cooldownComponents, List<DotEffectComponent> dotEffectComponents, List <HealthComponent> healthComponents, List<JobComponent> jobComponents, List<KeyMappingComponent> keyMappingComponents, List<ModifierStateComponent> modifierStateComponents, List<OverTimeStateComponent> overtimeStateComponents, List<PotencyComponent> potencyComponents, List<SkillBaseComponent> skillBaseComponents, List<StatusEffectComponent> statusEffectComponents, List<TargetComponent> targetComponents)
        {
            this.attributesComponents = attributesComponents;
            this.modifierStateComponents = modifierStateComponents;
            this.dotEffectComponents = dotEffectComponents;
            this.healthComponents = healthComponents;
            this.jobComponents = jobComponents;
            this.keyMappingComponents = keyMappingComponents;
            this.overtimeStateComponents = overtimeStateComponents;
            this.potencyComponents = potencyComponents;
            this.cooldownComponents = cooldownComponents;
            this.skillBaseComponents = skillBaseComponents;
            this.statusEffectComponents = statusEffectComponents;
            this.targetComponents = targetComponents;
            rng = new Random();
        }

        public void Update(ulong timer, Keyboard keyboard)
        {
            foreach ( KeyMappingComponent keyMapComp in keyMappingComponents)
            {
                // User
                Entity entity = keyMapComp.Parent;

                // User Components
                AttributesComponent attComp = attributesComponents.Find(x => x.Parent == entity);
                ModifierStateComponent modStateComp = modifierStateComponents.Find(x => x.Parent == entity);
                JobComponent jobComp = jobComponents.Find(x => x.Parent == entity);

                // Target components
                TargetComponent targComp = targetComponents.Find(x => x.Parent == entity);
                HealthComponent targHealthComp = healthComponents.Find(x => x.Parent == targComp.Target);
                ModifierStateComponent targModStateComp = modifierStateComponents.Find(x => x.Parent == targComp.Target);
                OverTimeStateComponent targOtComp = overtimeStateComponents.Find(x => x.Parent == targComp.Target);

                // Skill list
                List<Entity> skillList = jobComp.SkillList;

                foreach (KeyBind keyBind in keyMapComp.KeyBinds )
                {
                    // Skill components
                    SkillBaseComponent skillBaseComp = skillBaseComponents.Find(x => x.Name == keyBind.Command);

                    if(skillBaseComp != null)
                    {
                        // Skill
                        Entity skill = skillBaseComp.Parent;

                        // More skill components
                        PotencyComponent potComp = potencyComponents.Find(x => x.Parent == skill);
                        CooldownComponent cdComp = cooldownComponents.Find(x => x.Parent == skill);

                        if (keyBind.IsActive)
                        {
                            // Determining skill recast time
                            decimal recast;
                            if(skillBaseComp.Type == SkillType.Weaponskill)
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
                            }else if(skillBaseComp.Type == SkillType.Spell)
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

                            // Checking if skill is on cooldown
                            if (timer - cdComp.Start  >= (ulong)recast.SecondsToMilli())
                            {
                                //DEBUG: debug strings
                                string critical = " Critical! ";
                                string direct = "Direct Hit! ";

                                decimal totalDamage = 0;

                                decimal potMod = 1;
                                decimal wdMod = 1;
                                decimal apMod = 1;
                                decimal detMod = 1;
                                decimal tenMod = 1;
                                decimal ssMod = 1;
                                decimal traitMod = 1;
                                decimal critMod = 1;
                                decimal dhitMod = 1;

                                decimal critChance;
                                decimal dhitChance;

                                // Starts calculating potency modifier from base potency
                                potMod = CombatFormulas.PotencyMod(potComp.Amount);

                                // Calculates weapon damage modifier
                                wdMod = CombatFormulas.WeaponDamageMod(attComp.AttributesDictionary[AttributeType.WeaponDamage]);

                                // Calculates attack power modifier
                                if(jobComp.Job == Jobs.Paladin || jobComp.Job == Jobs.Warrior || jobComp.Job == Jobs.DarkKnight || jobComp.Job == Jobs.Monk || jobComp.Job == Jobs.Dragoon || jobComp.Job == Jobs.Samurai)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Strenght] + modStateComp.BuffDictionary[AttributeType.Strenght]);
                                }else if(jobComp.Job == Jobs.Ninja || jobComp.Job == Jobs.Bard || jobComp.Job == Jobs.Machinist)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Dexterity] + modStateComp.BuffDictionary[AttributeType.Dexterity]);
                                }else if (jobComp.Job == Jobs.BlackMage || jobComp.Job == Jobs.Summoner || jobComp.Job == Jobs.RedMage)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Intelligence] + modStateComp.BuffDictionary[AttributeType.Intelligence]);
                                }else if (jobComp.Job == Jobs.WhiteMage || jobComp.Job == Jobs.Scholar || jobComp.Job == Jobs.Astrologian)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Mind] + modStateComp.BuffDictionary[AttributeType.Mind]);
                                }

                                // Calculates determination modifier
                                detMod = CombatFormulas.DeterminationDamageMod(attComp.AttributesDictionary[AttributeType.Determination] + modStateComp.BuffDictionary[AttributeType.Determination]);

                                // Calculates tenacity modifier
                                tenMod = CombatFormulas.TenacityDamageMod(attComp.AttributesDictionary[AttributeType.Tenacity] + modStateComp.BuffDictionary[AttributeType.Tenacity]);

                                // Calculates speed modifier
                                if(jobComp.Job == Jobs.BlackMage || jobComp.Job == Jobs.Summoner || jobComp.Job == Jobs.RedMage || jobComp.Job == Jobs.WhiteMage || jobComp.Job == Jobs.Scholar || jobComp.Job == Jobs.Astrologian)
                                {
                                    ssMod = CombatFormulas.SpeedDamageMod(attComp.AttributesDictionary[AttributeType.SpellSpeed] + modStateComp.BuffDictionary[AttributeType.SpellSpeed]);
                                }
                                else
                                {
                                    ssMod = CombatFormulas.SpeedDamageMod(attComp.AttributesDictionary[AttributeType.SkillSpeed] + modStateComp.BuffDictionary[AttributeType.SkillSpeed]);
                                }

                                // Calculates trait modifier
                                if(jobComp.Job == Jobs.Ninja || jobComp.Job == Jobs.Bard || jobComp.Job == Jobs.Machinist)
                                {
                                    traitMod = 1.2m;
                                }else if(jobComp.Job == Jobs.BlackMage || jobComp.Job == Jobs.Summoner || jobComp.Job == Jobs.RedMage || jobComp.Job == Jobs.WhiteMage || jobComp.Job == Jobs.Scholar || jobComp.Job == Jobs.Astrologian)
                                {
                                    traitMod = 1.3m;
                                }

                                critChance = CombatFormulas.CriticalHitRate(attComp.AttributesDictionary[AttributeType.CriticalHit]) + modStateComp.BuffDictionary[AttributeType.CriticalHitRate];
                                dhitChance = CombatFormulas.DirectHitRate(attComp.AttributesDictionary[AttributeType.DirectHit]) + modStateComp.BuffDictionary[AttributeType.DirectHitRate];

                                // Calculates crit modifier
                                critMod = CombatFormulas.CriticalHitDamageMod(attComp.AttributesDictionary[AttributeType.CriticalHit] + modStateComp.BuffDictionary[AttributeType.CriticalHit]);

                                // Calculates dhit modifier
                                dhitMod = CombatFormulas.DirectHitDamageMod(attComp.AttributesDictionary[AttributeType.DirectHit] + modStateComp.BuffDictionary[AttributeType.DirectHit]);

                                // Compilates the chances into a dictionary:
                                Dictionary<AttributeType, decimal> chancesDictionary = new Dictionary<AttributeType, decimal>
                                {
                                    { AttributeType.CriticalHitRate, critChance },
                                    { AttributeType.DirectHitRate, dhitChance }
                                };

                                // Compilates all the modifiers into a dictionary:
                                Dictionary<DamageModifierType, decimal> modifiersDictionary = new Dictionary<DamageModifierType, decimal>
                                {
                                    { DamageModifierType.PotencyModifier, potMod },
                                    { DamageModifierType.WeaponDamageModifier, wdMod },
                                    { DamageModifierType.AttackPowerModifier, apMod },
                                    { DamageModifierType.DeterminationModifier, detMod },
                                    { DamageModifierType.TenacityModifier, tenMod },
                                    { DamageModifierType.TraitModifier, traitMod },
                                    { DamageModifierType.SpeedModifier, ssMod },
                                    { DamageModifierType.CriticalModifier, critMod },
                                    { DamageModifierType.DirectModifier, dhitMod }
                                };

                                // If not critical hit, modifier is 1
                                if ((int)(critChance * 10) <= rng.Next(0, 1000))
                                {
                                    critMod = 1;

                                    //DEBUG: debug string
                                    critical = " ";
                                }

                                // If not direct hit, modifier is 1
                                if ((int)(dhitChance * 10) <= rng.Next(0, 1000))
                                {
                                    dhitMod = 1;

                                    //DEBUG: debug string
                                    direct = "";
                                }


                                totalDamage = CombatFormulas.DirectDamage(potMod, wdMod, apMod, detMod, tenMod, traitMod, critMod, dhitMod, modStateComp.BuffList);

                                // Inflicts damage on target's health component
                                targHealthComp.Amount -= totalDamage;
                                targHealthComp.DamageTaken += totalDamage;

                                // Logic for cooldown
                                foreach (Entity s in cdComp.SharedCooldownList)
                                {
                                    cooldownComponents.Find(x => x.Parent == s).Start = timer;
                                }

                                // Logic for buff effects
                                foreach(StatusEffectComponent statusComp in statusEffectComponents.FindAll(x => x.Parent == skill))
                                {
                                    Buff b = modStateComp.BuffList.Find(x => x.UserSource == entity && x.Name == statusComp.Name);

                                    // If there's already the same buff applied, refresh the timer
                                    if(b != null)
                                    {
                                        b.Start = timer;
                                    }
                                    // Otherwise, adds the buff
                                    else
                                    {
                                        if(statusComp.Actor == ActorType.Self)
                                        {
                                            modStateComp.BuffList.Add(new Buff(entity, entity, statusComp.Name, statusComp.Type, statusComp.Duration, timer, statusComp.Modifier, false));
                                        }else if(statusComp.Actor == ActorType.Target)
                                        {
                                            modStateComp.BuffList.Add(new Buff(targComp.Target, entity, statusComp.Name, statusComp.Type, statusComp.Duration, timer, statusComp.Modifier, false));
                                        }
                                    }
                                }

                                // Logic for over time effects
                                foreach (DotEffectComponent dotEffectComp in dotEffectComponents.FindAll(x => x.Parent == skill))
                                {
                                    DoT d = targOtComp.DotList.Find(x => x.UserSource == entity && x.Name == dotEffectComp.Name);

                                    // If there's already the same DoT applied
                                    if(d != null)
                                    {
                                        d.Start = timer;
                                    }
                                    //Otherwise, adds the dot
                                    else
                                    {
                                        targOtComp.DotList.Add(new DoT(targComp.Target, entity, modifiersDictionary, chancesDictionary, modStateComp.BuffList, dotEffectComp.Name, dotEffectComp.Potency, dotEffectComp.Duration, timer, false));
                                    }
                                }

                                //DEBUG: Console log
                                Console.WriteLine("[{0:00.00}]{3}{4}Used {1} for {2} damage. (Crit Chance: {5})", timer.MilliToSeconds(),skillBaseComp.Name.ToString(), totalDamage,critical,direct,critChance);
                            }
                        }
                    }
                }
            }
        }
    }
}

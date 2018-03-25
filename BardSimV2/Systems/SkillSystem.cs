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
        List<ApplyBuffEffectComponent> applyBuffEffectComponents;
        List<AttributesComponent> attributesComponents;
        List<BuffStateComponent> buffStateComponents;
        List<HealthComponent> healthComponents;
        List<JobComponent> jobComponents;
        List<KeyMappingComponent> keyMappingComponents;
        List<PotencyComponent> potencyComponents;
        List<SharedCooldownComponent> sharedCooldownComponents;
        List<SkillBaseComponent> skillBaseComponents;
        List<TargetComponent> targetComponents;

        Random rng;


        public SkillSystem(List<ApplyBuffEffectComponent> applyBuffEffectComponents, List<AttributesComponent> attributesComponents, List<BuffStateComponent> buffStateComponents, List<HealthComponent> healthComponents, List<JobComponent> jobComponents, List<KeyMappingComponent> keyMappingComponents, List<PotencyComponent> potencyComponents, List<SharedCooldownComponent> sharedCooldownComponents, List<SkillBaseComponent> skillBaseComponents, List<TargetComponent> targetComponents)
        {
            this.applyBuffEffectComponents = applyBuffEffectComponents;
            this.attributesComponents = attributesComponents;
            this.buffStateComponents = buffStateComponents;
            this.healthComponents = healthComponents;
            this.jobComponents = jobComponents;
            this.keyMappingComponents = keyMappingComponents;
            this.potencyComponents = potencyComponents;
            this.sharedCooldownComponents = sharedCooldownComponents;
            this.skillBaseComponents = skillBaseComponents;
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
                BuffStateComponent buffStateComp = buffStateComponents.Find(x => x.Parent == entity);
                JobComponent jobComp = jobComponents.Find(x => x.Parent == entity);

                // Target components
                TargetComponent targComp = targetComponents.Find(x => x.Parent == entity);
                HealthComponent healthComp = healthComponents.Find(x => x.Parent == targComp.Target);

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
                        ApplyBuffEffectComponent appBuffComp = applyBuffEffectComponents.Find(x => x.Parent == skill);
                        SharedCooldownComponent sharedCdComp = sharedCooldownComponents.Find(x => x.Parent == skill);

                        if (keyBind.IsActive)
                        {
                            // Determining skill recast time
                            decimal recast;
                            if(skillBaseComp.Type == SkillType.Weaponskill)
                            {
                                recast = CombatFormulas.SpeedRecast
                                    (
                                        attComp.AttributesDictionary[AttributeType.SkillSpeed] + buffStateComp.BuffDictionary[AttributeType.SkillSpeed],
                                        skillBaseComp.Cooldown.BaseRecast,
                                        buffStateComp.BuffDictionary[AttributeType.Arrow],
                                        buffStateComp.BuffDictionary[AttributeType.Haste],
                                        buffStateComp.BuffDictionary[AttributeType.FeyWind],
                                        buffStateComp.BuffDictionary[AttributeType.SpeedType1],
                                        buffStateComp.BuffDictionary[AttributeType.SpeedType2],
                                        buffStateComp.BuffDictionary[AttributeType.RiddleOfFire],
                                        buffStateComp.BuffDictionary[AttributeType.AstralUmbral]
                                    );
                            }else if(skillBaseComp.Type == SkillType.Spell)
                            {
                                recast = CombatFormulas.SpeedRecast
                                    (
                                        attComp.AttributesDictionary[AttributeType.SpellSpeed] + buffStateComp.BuffDictionary[AttributeType.SpellSpeed],
                                        skillBaseComp.Cooldown.BaseRecast,
                                        buffStateComp.BuffDictionary[AttributeType.Arrow],
                                        buffStateComp.BuffDictionary[AttributeType.Haste],
                                        buffStateComp.BuffDictionary[AttributeType.FeyWind],
                                        buffStateComp.BuffDictionary[AttributeType.SpeedType1],
                                        buffStateComp.BuffDictionary[AttributeType.SpeedType2],
                                        buffStateComp.BuffDictionary[AttributeType.RiddleOfFire],
                                        buffStateComp.BuffDictionary[AttributeType.AstralUmbral]
                                    );
                            }
                            else
                            {
                                recast = skillBaseComp.Cooldown.BaseRecast;
                            }

                            // Checking if skill is on cooldown
                            if (timer - skillBaseComp.Cooldown.Start  >= (ulong)recast.SecondsToMilli())
                            {
                                //DEBUG: debug strings
                                string critical = " ";
                                string direct = "";
                                decimal critchance;

                                decimal totalDamage = 0;

                                decimal potMod = 1;
                                decimal wdMod = 1;
                                decimal apMod = 1;
                                decimal detMod = 1;
                                decimal tenMod = 1;
                                decimal traitMod = 1;
                                decimal critMod = 1;
                                decimal dhitMod = 1;

                                // Starts calculating potency modifier from base potency
                                potMod = CombatFormulas.PotencyMod(potComp.Amount);

                                // Calculates weapon damage modifier
                                wdMod = CombatFormulas.WeaponDamageMod(attComp.AttributesDictionary[AttributeType.WeaponDamage]);

                                // Calculates attack power modifier
                                if(jobComp.Job == Jobs.Paladin || jobComp.Job == Jobs.Warrior || jobComp.Job == Jobs.DarkKnight || jobComp.Job == Jobs.Monk || jobComp.Job == Jobs.Dragoon || jobComp.Job == Jobs.Samurai)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Strenght] + buffStateComp.BuffDictionary[AttributeType.Strenght]);
                                }else if(jobComp.Job == Jobs.Ninja || jobComp.Job == Jobs.Bard || jobComp.Job == Jobs.Machinist)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Dexterity] + buffStateComp.BuffDictionary[AttributeType.Dexterity]);
                                }else if (jobComp.Job == Jobs.BlackMage || jobComp.Job == Jobs.Summoner || jobComp.Job == Jobs.RedMage)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Intelligence] + buffStateComp.BuffDictionary[AttributeType.Intelligence]);
                                }else if (jobComp.Job == Jobs.WhiteMage || jobComp.Job == Jobs.Scholar || jobComp.Job == Jobs.Astrologian)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Mind] + buffStateComp.BuffDictionary[AttributeType.Mind]);
                                }

                                // Calculates determination modifier
                                detMod = CombatFormulas.DeterminationDamageMod(attComp.AttributesDictionary[AttributeType.Determination] + buffStateComp.BuffDictionary[AttributeType.Determination]);

                                // Calculates tenacity modifier
                                tenMod = CombatFormulas.TenacityDamageMod(attComp.AttributesDictionary[AttributeType.Tenacity] + buffStateComp.BuffDictionary[AttributeType.Tenacity]);

                                // Calculates trait modifier
                                if(jobComp.Job == Jobs.Ninja || jobComp.Job == Jobs.Bard || jobComp.Job == Jobs.Machinist)
                                {
                                    traitMod = 1.2m;
                                }else if(jobComp.Job == Jobs.BlackMage || jobComp.Job == Jobs.Summoner || jobComp.Job == Jobs.RedMage || jobComp.Job == Jobs.WhiteMage || jobComp.Job == Jobs.Scholar || jobComp.Job == Jobs.Astrologian)
                                {
                                    traitMod = 1.3m;
                                }

                                // DEBUG: debug value
                                critchance = ((CombatFormulas.CriticalHitRate(attComp.AttributesDictionary[AttributeType.CriticalHit])) + (buffStateComp.BuffDictionary[AttributeType.CriticalHitRate]));

                                // Checks for critical hit chance
                                if (((int)(CombatFormulas.CriticalHitRate(attComp.AttributesDictionary[AttributeType.CriticalHit]) * 10) + (int)(buffStateComp.BuffDictionary[AttributeType.CriticalHitRate] * 10)) > rng.Next(0, 1000))
                                {

                                    // Calculates crit modifier
                                    critMod = CombatFormulas.CriticalHitDamageMod(attComp.AttributesDictionary[AttributeType.CriticalHit] + buffStateComp.BuffDictionary[AttributeType.CriticalHit]);

                                    //DEBUG: debug string
                                    critical = " Critical! ";
                                }

                                // Checks for direct hit chance
                                if (((int)(CombatFormulas.DirectHitRate(attComp.AttributesDictionary[AttributeType.DirectHit]) * 10) + (int)(buffStateComp.BuffDictionary[AttributeType.DirectHitRate] * 10)) > rng.Next(0, 1000))
                                {
                                    // Calculates dhit modifier
                                    dhitMod = CombatFormulas.DirectHitDamageMod(attComp.AttributesDictionary[AttributeType.DirectHit] + buffStateComp.BuffDictionary[AttributeType.DirectHit]);

                                    //DEBUG: debug string
                                    direct = "Direct Hit! ";
                                }

                                totalDamage = CombatFormulas.DirectDamage(potMod, wdMod, apMod, detMod, tenMod, traitMod, critMod, dhitMod, buffStateComp.BuffList);

                                // Inflicts damage on target's health component
                                healthComp.Amount -= totalDamage;
                                healthComp.DamageTaken += totalDamage;

                                // Logic for cooldown
                                if(sharedCdComp != null)
                                {
                                    foreach (Entity s in sharedCdComp.SkillList)
                                    {
                                        var sbComp = skillBaseComponents.Find(x => x.Parent == s);

                                        sbComp.Cooldown.Start = timer;
                                    }
                                }
                                else
                                {
                                    skillBaseComp.Cooldown.Start = timer;
                                }

                                // Logic for buff effects
                                if(appBuffComp != null)
                                {
                                    Buff b = buffStateComp.BuffList.Find(x => x.SkillSource == skill && x.UserSource == entity && x.Type == appBuffComp.Type);

                                    // If there's already the same buff applied, refreshed the timer
                                    if(b != null)
                                    {
                                        b.Start = timer;
                                    }
                                    // Otherwise, adds the buff
                                    else
                                    {
                                        buffStateComp.BuffList.Add(new Buff(skill, entity, appBuffComp.Type, appBuffComp.Duration, timer, appBuffComp.Modifier, false));
                                    }
                                }

                                //DEBUG: Console log
                                Console.WriteLine("[{0:00.00}]{3}{4}Used {1} for {2} damage. (Crit Chance: {5})", timer.MilliToSeconds(),skillBaseComp.Name.ToString(), totalDamage,critical,direct,critchance);
                            }
                        }
                    }
                }
            }
        }
    }
}

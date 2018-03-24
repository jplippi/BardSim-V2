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
        // TODO: Populate with actual components
        List<KeyMappingComponent> keyMappingComponents = new List<KeyMappingComponent>();
        List<TargetComponent> targetComponents = new List<TargetComponent>();
        List<HealthComponent> healthComponents = new List<HealthComponent>();
        List<AttributesComponent> attributesComponents = new List<AttributesComponent>();
        List<BuffStateComponent> buffStateComponents = new List<BuffStateComponent>();
        List<PotencyComponent> potencyComponents = new List<PotencyComponent>();
        List<CooldownComponent> cooldownComponents = new List<CooldownComponent>();
        List<JobComponent> jobComponents = new List<JobComponent>();

        public void Update(Stopwatch timer, Keyboard keyboard)
        {
            foreach ( KeyMappingComponent keyMapComp in keyMappingComponents)
            {
                Entity entity = keyMapComp.Parent;

                foreach(KeyBind keyBind in keyMapComp.KeyBinds )
                {
                    //TODO: Have a proper 'enum'
                    if(keyBind.Command == "Heavy Shot")
                    {
                        if (keyBind.IsActive)
                        {
                            CooldownComponent cdComp = cooldownComponents.Find(x => x.Parent == entity);
                            
                            if (cdComp.Start - timer.ElapsedMilliseconds < cdComp.Recast)
                            {
                                Random rng = new Random();
                                decimal totalDamage = 0;

                                decimal potMod = 1;
                                decimal wdMod = 1;
                                decimal apMod = 1;
                                decimal detMod = 1;
                                decimal tenMod = 1;
                                decimal traitMod = 1;
                                decimal critMod = 1;
                                decimal dhitMod = 1;

                                // Target components
                                TargetComponent targComp = targetComponents.Find(x => x.Parent == entity);
                                HealthComponent healthComp = healthComponents.Find(x => x.Parent == targComp.Target);

                                // User Components
                                AttributesComponent attComp = attributesComponents.Find(x => x.Parent == entity);
                                BuffStateComponent buffStateComp = buffStateComponents.Find(x => x.Parent == entity);
                                JobComponent jobComp = jobComponents.Find(x => x.Parent == entity);

                                // Skill Components
                                PotencyComponent potComp = potencyComponents.Find(x => x.Parent == entity);

                                // Starts calculating potency modifier from base potency
                                potMod = CombatFormulas.PotencyMod(potComp.Amount);

                                // Calculates weapon damage modifier
                                wdMod = CombatFormulas.WeaponDamageMod(attComp.AttributesDictionary[AttributeType.WeaponDamage]);

                                // Calculates attack power modifier
                                if(jobComp.Job == Jobs.Paladin || jobComp.Job == Jobs.Warrior || jobComp.Job == Jobs.DarkKnight || jobComp.Job == Jobs.Monk || jobComp.Job == Jobs.Dragoon || jobComp.Job == Jobs.Samurai)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Strenght] + buffStateComp.AttributesDictionary[AttributeType.Strenght]);
                                }else if(jobComp.Job == Jobs.Ninja || jobComp.Job == Jobs.Bard || jobComp.Job == Jobs.Machinist)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Dexterity] + buffStateComp.AttributesDictionary[AttributeType.Dexterity]);
                                }else if (jobComp.Job == Jobs.BlackMage || jobComp.Job == Jobs.Summoner || jobComp.Job == Jobs.RedMage)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Intelligence] + buffStateComp.AttributesDictionary[AttributeType.Intelligence]);
                                }else if (jobComp.Job == Jobs.WhiteMage || jobComp.Job == Jobs.Scholar || jobComp.Job == Jobs.Astrologian)
                                {
                                    apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Mind] + buffStateComp.AttributesDictionary[AttributeType.Mind]);
                                }

                                // Calculates determination modifier
                                detMod = CombatFormulas.DeterminationDamageMod(attComp.AttributesDictionary[AttributeType.Determination] + buffStateComp.AttributesDictionary[AttributeType.Determination]);

                                // Calculates tenacity modifier
                                tenMod = CombatFormulas.TenacityDamageMod(attComp.AttributesDictionary[AttributeType.Tenacity] + buffStateComp.AttributesDictionary[AttributeType.Tenacity]);

                                // Calculates trait modifier
                                if(jobComp.Job == Jobs.Ninja || jobComp.Job == Jobs.Bard || jobComp.Job == Jobs.Machinist)
                                {
                                    traitMod = 1.2m;
                                }else if(jobComp.Job == Jobs.BlackMage || jobComp.Job == Jobs.Summoner || jobComp.Job == Jobs.RedMage || jobComp.Job == Jobs.WhiteMage || jobComp.Job == Jobs.Scholar || jobComp.Job == Jobs.Astrologian)
                                {
                                    traitMod = 1.3m;
                                }

                                // Checks for critical hit chance
                                if (((int)(CombatFormulas.CriticalHitRate(attComp.AttributesDictionary[AttributeType.CriticalHit])*10) + (int)(buffStateComp.SpecialBuffDictionary[SpecialBuffType.CriticalHitRate]*10)) < rng.Next(0, 1000))
                                {
                                    // Calculates crit modifier
                                    critMod = CombatFormulas.CriticalHitDamageMod(attComp.AttributesDictionary[AttributeType.CriticalHit] + buffStateComp.AttributesDictionary[AttributeType.CriticalHit]);
                                }

                                // Checks for direct hit chance
                                if (((int)(CombatFormulas.DirectHitRate(attComp.AttributesDictionary[AttributeType.DirectHit]) * 10) + (int)(buffStateComp.SpecialBuffDictionary[SpecialBuffType.DirectHitRate] * 10)) < rng.Next(0, 1000))
                                {
                                    // Calculates dhit modifier
                                    dhitMod = CombatFormulas.DirectHitDamageMod(attComp.AttributesDictionary[AttributeType.DirectHit] + buffStateComp.AttributesDictionary[AttributeType.DirectHit]);
                                }

                                totalDamage = CombatFormulas.DirectDamage(potMod, wdMod, apMod, detMod, tenMod, traitMod, critMod, dhitMod, buffStateComp.SpecialBuffList);

                                // Inflicts damage on target's health component
                                healthComp.Amount -= totalDamage;
                                healthComp.DamageTaken += totalDamage;

                                // Puts skill on cooldown
                                cdComp.Start = timer.ElapsedMilliseconds;
                            }
                        }
                    }
                }
            }
        }
    }
}

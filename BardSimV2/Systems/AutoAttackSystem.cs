using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AutoAttackSystem : ISystem
    {
        List<AttributesComponent> attributesComponents;
        List<AutoAttackComponent> autoAttackComponents;
        List<BardComponent> bardComponents;
        List<HealthComponent> healthComponents;
        List<ModifierStateComponent> modifiterStateComponents;
        List<TargetComponent> targetComponents;

        Random rng = new Random();

        public AutoAttackSystem(List<AttributesComponent> attributesComponents, List<AutoAttackComponent> autoAttackComponents, List<BardComponent> bardComponents, List<HealthComponent> healthComponents, List<ModifierStateComponent> modifiterStateComponents, List<TargetComponent> targetComponents)
        {
            this.attributesComponents = attributesComponents;
            this.autoAttackComponents = autoAttackComponents;
            this.bardComponents = bardComponents;
            this.healthComponents = healthComponents;
            this.modifiterStateComponents = modifiterStateComponents;
            this.targetComponents = targetComponents;
        }


        public void Update(decimal timer, Keyboard keyboard, LogData log)
        {
            foreach (AutoAttackComponent aaComp in autoAttackComponents)
            {
                AttributesComponent attComp = attributesComponents.Find(x => x.Parent == aaComp.Parent);
                ModifierStateComponent modStateComp = modifiterStateComponents.Find(x => x.Parent == aaComp.Parent);
                BardComponent brdComp = bardComponents.Find(x => x.Parent == aaComp.Parent);
                TargetComponent targComp = targetComponents.Find(x => x.Parent == aaComp.Parent);
                HealthComponent targHealthComp = healthComponents.Find(x => x.Parent == targComp.Target);

                // To assure autos start right away
                if(aaComp.NextAuto == 0)
                {
                    aaComp.NextAuto = timer;
                }

                // Inflict auto attack damage every X seconds, where X is the weapon delay
                if( timer == aaComp.NextAuto)
                {
                    decimal potMod = 1;
                    decimal aaMod = 1;
                    decimal apMod = 1;
                    decimal detMod = 1;
                    decimal tenMod = 1;
                    decimal ssMod = 1;
                    decimal critMod = 1;
                    decimal dhitMod = 1;

                    decimal critChance;
                    decimal dhitChance;

                    decimal totalDamage;

                    // Starts calculating potency modifier from base potency
                    potMod = CombatFormulas.PotencyMod(aaComp.Potency);

                    // Calculates auto attack modifier
                    aaMod = CombatFormulas.AutoAttackMod(attComp.AttributesDictionary[AttributeType.WeaponDamage], attComp.AttributesDictionary[AttributeType.WeaponDelay]);

                    // Calculates attack power modifier (every job uses str, except dex-based jobs)
                    if (brdComp.Job == Job.Ninja || brdComp.Job == Job.Bard || brdComp.Job == Job.Machinist)
                    {
                        apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Dexterity] + modStateComp.BuffDictionary[AttributeType.Dexterity]);
                    }
                    else
                    {
                        apMod = CombatFormulas.AttackPowerDamageMod(attComp.AttributesDictionary[AttributeType.Strenght] + modStateComp.BuffDictionary[AttributeType.Strenght]);
                    }

                    // Calculates determination modifier
                    detMod = CombatFormulas.DeterminationDamageMod(attComp.AttributesDictionary[AttributeType.Determination] + modStateComp.BuffDictionary[AttributeType.Determination]);

                    // Calculates tenacity modifier
                    tenMod = CombatFormulas.TenacityDamageMod(attComp.AttributesDictionary[AttributeType.Tenacity] + modStateComp.BuffDictionary[AttributeType.Tenacity]);

                    // Calculates speed modifier (even casters use sks for this)

                    ssMod = CombatFormulas.SpeedDamageMod(attComp.AttributesDictionary[AttributeType.SkillSpeed] + modStateComp.BuffDictionary[AttributeType.SkillSpeed]);

                    critChance = CombatFormulas.CriticalHitRate(attComp.AttributesDictionary[AttributeType.CriticalHit]) + modStateComp.BuffDictionary[AttributeType.CriticalHitRate];

                    dhitChance = CombatFormulas.DirectHitRate(attComp.AttributesDictionary[AttributeType.DirectHit]) + modStateComp.BuffDictionary[AttributeType.DirectHitRate];

                    // Calculates crit modifier
                    // If not critical hit, modifier is 1
                    if ((int)(critChance * 10) < rng.Next(1, 1000))
                    {
                        critMod = 1;
                    }
                    else
                    {
                        critMod = CombatFormulas.CriticalHitDamageMod(attComp.AttributesDictionary[AttributeType.CriticalHit] + modStateComp.BuffDictionary[AttributeType.CriticalHit]);
                    }

                    // Calculates dhit modifier
                    // If not direct hit, modifier is 1
                    if ((int)(dhitChance * 10) < rng.Next(1, 1000))
                    {
                        dhitMod = 1;
                    }
                    else
                    {
                        dhitMod = CombatFormulas.DirectHitDamageMod(attComp.AttributesDictionary[AttributeType.DirectHit] + modStateComp.BuffDictionary[AttributeType.DirectHit]);
                    }

                    totalDamage = CombatFormulas.AutoAttackDamage(potMod, aaMod, apMod, detMod, tenMod, 1, ssMod, critMod, dhitMod, modStateComp.BuffList);

                    // Inflicts damage on target's health component
                    targHealthComp.Amount -= totalDamage;
                    targHealthComp.DamageTaken += totalDamage;

                    // Adds the damage to the log, if it exists
                    if (log != null)
                    {
                            log.Log.Rows.Add(LogActionType.AutoAttack, timer, "Auto attack", totalDamage, critMod > 1, dhitMod > 1, false);
                    }

                    // Sets next auto
                    aaComp.NextAuto += attComp.AttributesDictionary[AttributeType.WeaponDelay];
                }
            }
        }
    }
}

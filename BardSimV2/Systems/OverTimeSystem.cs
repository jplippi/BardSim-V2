using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class OverTimeSystem : ISystem
    {
        List<DoT> toBeRemoved = new List<DoT>();
        List<HealthComponent> healthComponents;
        List<OverTimeStateComponent> overtimeStateComponents;
        Random rng = new Random();

        public OverTimeSystem(List<HealthComponent> healthComponents, List<OverTimeStateComponent> overtimeStateComponents)
        {
            this.healthComponents = healthComponents;
            this.overtimeStateComponents = overtimeStateComponents;
        }

        public void Update(ulong timer, Keyboard keyboard)
        {
            foreach(OverTimeStateComponent otStateComp in overtimeStateComponents)
            {
                // Gets the HealthComponent of the entity inflicted by the over time effect
                HealthComponent healthComp = healthComponents.Find(x => x.Parent == otStateComp.Parent);

                foreach(DoT dot in otStateComp.DotList)
                {
                    // Remove the DoT if it's expired
                    if(timer - dot.Start > (ulong)dot.Duration.SecondsToMilli())
                    {
                        toBeRemoved.Add(dot);
                    }
                    else
                    {
                        // Activates if not active
                        dot.IsActive = true;

                        // Executes the damage
                        if(((timer - (ulong)otStateComp.Offset.SecondsToMilli()) % (ulong)(3m.SecondsToMilli()) == 0) && (timer - dot.LastTick >= (ulong)3m.SecondsToMilli()))
                        {
                            //DEBUG: debug strings
                            string critical = " ";
                            string direct = "";

                            decimal critMod = 1;
                            decimal dhitMod = 1;

                            if( (int)(dot.UsersChancesDictionary[AttributeType.CriticalHitRate] * 10) > rng.Next(0, 1000))
                            {
                                critMod = dot.UsersModifiersDictionary[DamageModifierType.CriticalModifier];

                                //DEBUG: debug string
                                critical = " Critical! ";
                            }

                            if( (int)(dot.UsersChancesDictionary[AttributeType.DirectHitRate] * 10) > rng.Next(0, 1000))
                            {
                                dhitMod = dot.UsersModifiersDictionary[DamageModifierType.DirectModifier];

                                //DEBUG: debug string
                                direct = " Direct! ";
                            }

                            decimal dotTick = CombatFormulas.DoTDamage
                                (
                                    CombatFormulas.PotencyMod(dot.Potency),
                                    dot.UsersModifiersDictionary[DamageModifierType.WeaponDamageModifier],
                                    dot.UsersModifiersDictionary[DamageModifierType.AttackPowerModifier],
                                    dot.UsersModifiersDictionary[DamageModifierType.DeterminationModifier],
                                    dot.UsersModifiersDictionary[DamageModifierType.TenacityModifier],
                                    dot.UsersModifiersDictionary[DamageModifierType.TraitModifier],
                                    dot.UsersModifiersDictionary[DamageModifierType.SpeedModifier],
                                    critMod,
                                    dhitMod,
                                    dot.UsersBuffList
                                );
                            healthComp.DamageTaken += dotTick;

                            dot.LastTick = timer;

                            //DEBUG: Console log
                            Console.WriteLine("    [{0:00.00}]{3}{4}{1} ticked for {2} damage. (Crit Chance: {5})", timer.MilliToSeconds(), dot.Name.ToString(), dotTick, critical, direct, dot.UsersChancesDictionary[AttributeType.CriticalHitRate]);
                        }
                    }
                }
                foreach (DoT dot in toBeRemoved)
                {
                    otStateComp.DotList.Remove(dot);
                }

            }
        }
    }
}

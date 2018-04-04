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

        List<BardComponent> bardComponents;
        List<CooldownComponent> cooldownComponents;
        List<HealthComponent> healthComponents;
        List<ModifierStateComponent> modifierStateComponents;
        List<OverTimeStateComponent> overtimeStateComponents;
        List<RiverOfBloodComponent> riverOfBloodComponents;

        Random rng = new Random();

        public OverTimeSystem(List<BardComponent> bardComponents, List<CooldownComponent> cooldownComponents, List<HealthComponent> healthComponents, List<ModifierStateComponent> modifierStateComponents, List<OverTimeStateComponent> overtimeStateComponents, List<RiverOfBloodComponent> riverOfBloodComponents)
        {
            this.bardComponents = bardComponents;
            this.cooldownComponents = cooldownComponents;
            this.healthComponents = healthComponents;
            this.modifierStateComponents = modifierStateComponents;
            this.overtimeStateComponents = overtimeStateComponents;
            this.riverOfBloodComponents = riverOfBloodComponents;
        }

        public void Update(decimal timer, Keyboard keyboard, bool verbose)
        {
            foreach (OverTimeStateComponent otStateComp in overtimeStateComponents)
            {
                // Gets the HealthComponent of the entity inflicted by the over time effect
                HealthComponent healthComp = healthComponents.Find(x => x.Parent == otStateComp.Parent);

                foreach(DoT dot in otStateComp.DotList)
                {
                    // Remove the DoT if it's expired
                    if(timer - dot.Start > dot.Duration)
                    {
                        toBeRemoved.Add(dot);
                    }
                    else
                    {
                        // Activates if not active
                        dot.IsActive = true;

                        // Executes the damage
                        if(((timer - otStateComp.Offset) % (3m) == 0) && (timer - dot.LastTick >= 3m))
                        {
                            //DEBUG: debug strings
                            string critical = " ";
                            string direct = "";

                            decimal critMod = 1;
                            decimal dhitMod = 1;

                            bool hasCrit = false;

                            if( (int)(dot.UsersChancesDictionary[AttributeType.CriticalHitRate] * 10) > rng.Next(0, 1000))
                            {
                                critMod = dot.UsersModifiersDictionary[DamageModifierType.CriticalModifier];

                                // For repertoire
                                hasCrit = true;

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

                            // Handles repertoire
                            if(dot.Name == DotName.CausticBite || dot.Name == DotName.StormBite)
                            {
                                // Gets the BardComponent of the entity that applied the over time effect
                                foreach (BardComponent brdComp in bardComponents.FindAll(x => x.Parent == dot.UserSource))
                                {
                                    if (hasCrit)
                                    {
                                        if(brdComp.Song == SongName.TheWanderersMinuet && brdComp.Repertoire < 3)
                                        {
                                            brdComp.Repertoire++;
                                        }
                                        else if(brdComp.Song == SongName.MagesBallad)
                                        {
                                            brdComp.Repertoire++;
                                        }
                                        else if(brdComp.Song == SongName.ArmysPaeon && brdComp.Repertoire < 4)
                                        {
                                            brdComp.Repertoire++;
                                        }
                                    }
                                }

                            }

                            if (verbose)
                            {
                                //DEBUG: Listing damage mods
                                string damageMods = "";
                                foreach (Buff b in dot.UsersBuffList)
                                {
                                    if (b.Type == AttributeType.Damage)
                                    {
                                        damageMods = $"{damageMods}+{(b.Modifier - 1) * 100}% ";
                                    }
                                }

                                //DEBUG: Console log
                                Console.WriteLine("    [{0:00.00}]{3}{4}{1} ticked for {2} damage. (Crit Chance: {5}, Repertoire: {6}, Damage buffs: {7})", timer, dot.Name.ToString(), dotTick, critical, direct, dot.UsersChancesDictionary[AttributeType.CriticalHitRate], bardComponents.Find(x => x.Parent == dot.UserSource).Repertoire, damageMods);
                            }
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

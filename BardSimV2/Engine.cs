using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Engine
    {
        List<ISystem> _systems;
        List<Entity> _entities;

        // Sets up a keyboard to watch for inputs
        Keyboard keyboard = new Keyboard();

        // Initializing player entities
        Entity player = new Entity();

        // Initializing enemy entities
        Entity enemy = new Entity();

        // Initializing skill entities
        Entity heavyShot;
        Entity straightShot;
        Entity causticBite;
        Entity stormBite;
        Entity ironJaws;
        Entity refulgentArrow;
        Entity bloodletter;
        Entity empyrealArrow;
        Entity pitchPerfect;
        Entity sidewinder;
        Entity ragingStrikes;
        Entity barrage;
        Entity theWanderersMinuet;
        Entity magesBallad;
        Entity armysPaeon;

        // Shared cooldown lists
        List<Entity> gcdSkillList;
        List<Entity> riverOfBloodSkillList;

        // Making component lists
        List<AnimationLockComponent> animationLockComponents = new List<AnimationLockComponent>();
        List<AttributesComponent> attributesComponents = new List<AttributesComponent>();
        List<AutoAttackComponent> autoAttackComponents = new List<AutoAttackComponent>();
        List<BardComponent> bardComponents = new List<BardComponent>();
        List<ConditionalPotencyComponent> conditionalPotencyComponents = new List<ConditionalPotencyComponent>();
        List<CooldownComponent> cooldownComponents = new List<CooldownComponent>();
        List<DotEffectComponent> dotEffectComponents = new List<DotEffectComponent>();
        List<EnhancedEmpyrealArrowComponent> enhancedEmpyrealArrowComponents = new List<EnhancedEmpyrealArrowComponent>();
        List<GenericStatusEffectComponent> genericStatusEffectComponents = new List<GenericStatusEffectComponent>();
        List<HealthComponent> healthComponents = new List<HealthComponent>();
        List<IronJawsEffectComponent> ironJawsEffectComponents = new List<IronJawsEffectComponent>();
        List<ModifierStateComponent> modifierStateComponents = new List<ModifierStateComponent>();
        List<OverTimeStateComponent> overtimeStateComponents = new List<OverTimeStateComponent>();
        List<PotencyComponent> potencyComponents = new List<PotencyComponent>();
        List<RiverOfBloodComponent> riverOfBloodComponents = new List<RiverOfBloodComponent>();
        List<SkillBaseComponent> skillBaseComponents = new List<SkillBaseComponent>();
        List<SkillControlComponent> skillControlComponents = new List<SkillControlComponent>();
        List<SongComponent> songComponents = new List<SongComponent>();
        List<StatusEffectComponent> statusEffectComponents = new List<StatusEffectComponent>();
        List<StraighterShotEffectComponent> straighterShotEffectComponents = new List<StraighterShotEffectComponent>();
        List<TargetComponent> targetComponents = new List<TargetComponent>();
        List<UseConditionComponent> useConditionComponents = new List<UseConditionComponent>();
        List<UsesEnablerComponent> usesEnablerComponents = new List<UsesEnablerComponent>();
        List<UsesRepertoireComponent> usesRepertoireComponents = new List<UsesRepertoireComponent>();

        public Engine( AttributesDictionary playerAttributes)
        {
            _systems = new List<ISystem>();
            _entities = new List<Entity>();

            // Sets up a keyboard to watch for inputs
            keyboard = new Keyboard();

            // Initializing player entities
            player = new Entity();

            // Initializing enemy entities
            enemy = new Entity();

            // Initializing skill entities
            heavyShot = new Entity();
            straightShot = new Entity();
            causticBite = new Entity();
            stormBite = new Entity();
            ironJaws = new Entity();
            refulgentArrow = new Entity();
            bloodletter = new Entity();
            empyrealArrow = new Entity();
            pitchPerfect = new Entity();
            sidewinder = new Entity();
            ragingStrikes = new Entity();
            barrage = new Entity();
            theWanderersMinuet = new Entity();
            magesBallad = new Entity();
            armysPaeon = new Entity();

            // Shared cooldown lists
            gcdSkillList = new List<Entity>
            {
                heavyShot,
                straightShot,
                causticBite,
                stormBite,
                ironJaws,
                refulgentArrow
            };

            riverOfBloodSkillList = new List<Entity>
            {
                bloodletter,
                //rainOfDeath
            };

            // Adding components to skills
            heavyShot.AddComponents(new List<Component>
            {
                new SkillBaseComponent(heavyShot, SkillName.HeavyShot, SkillType.Weaponskill),
                new CooldownComponent(heavyShot, 2.5m, gcdSkillList),
                new PotencyComponent(heavyShot, 150),
                new StraighterShotEffectComponent(heavyShot, StatusName.StraighterShot, 10m, 20)
            });

            straightShot.AddComponents(new List<Component>
            {
                new SkillBaseComponent(straightShot, SkillName.StraightShot, SkillType.Weaponskill),
                new CooldownComponent(straightShot, 2.5m, gcdSkillList),
                new PotencyComponent(straightShot, 140),
                new StatusEffectComponent(straightShot, AttributeType.CriticalHitRate, StatusName.StraightShot, ActorType.Self, 30, 10)
            });

            causticBite.AddComponents(new List<Component>
            {
                new SkillBaseComponent(causticBite, SkillName.CausticBite, SkillType.Weaponskill),
                new CooldownComponent(causticBite, 2.5m, gcdSkillList),
                new PotencyComponent(causticBite, 120),
                new DotEffectComponent(causticBite, DotName.CausticBite, 30m, 45)
            });

            stormBite.AddComponents(new List<Component>
            {
                new SkillBaseComponent(stormBite, SkillName.Stormbite, SkillType.Weaponskill),
                new CooldownComponent(stormBite, 2.5m, gcdSkillList),
                new PotencyComponent(stormBite, 120),
                new DotEffectComponent(stormBite, DotName.Stormbite, 30m, 55)
            });

            ironJaws.AddComponents(new List<Component>
            {
                new SkillBaseComponent(ironJaws, SkillName.IronJaws, SkillType.Weaponskill),
                new CooldownComponent(ironJaws, 2.5m, gcdSkillList),
                new PotencyComponent(ironJaws, 100),
                new IronJawsEffectComponent(ironJaws, new List<DotEffectComponent>
                {
                    (DotEffectComponent)stormBite.Components.Find(x => x is DotEffectComponent),
                    (DotEffectComponent)causticBite.Components.Find(x => x is DotEffectComponent)
                })
            });

            refulgentArrow.AddComponents(new List<Component>
            {
                new SkillBaseComponent(refulgentArrow, SkillName.RefulgentArrow, SkillType.Weaponskill),
                new CooldownComponent(refulgentArrow, 2.5m, gcdSkillList),
                new PotencyComponent(refulgentArrow, 300),
                new UseConditionComponent(refulgentArrow, ConditionalFunctions.IsRefulgentArrowUsable),
                new UsesEnablerComponent(refulgentArrow, StatusName.StraighterShot)
            });

            bloodletter.AddComponents(new List<Component>
            {
                new SkillBaseComponent(bloodletter, SkillName.Bloodletter, SkillType.Ability),
                new CooldownComponent(bloodletter, 15m),
                new PotencyComponent(bloodletter, 130)
            });

            empyrealArrow.AddComponents(new List<Component>
            {
                new SkillBaseComponent(empyrealArrow, SkillName.EmpyrealArrow, SkillType.Weaponskill),
                new CooldownComponent(empyrealArrow, 15m),
                new PotencyComponent(empyrealArrow, 230),
                new EnhancedEmpyrealArrowComponent(empyrealArrow)
            });

            pitchPerfect.AddComponents(new List<Component>
            {
                new SkillBaseComponent(pitchPerfect, SkillName.PitchPerfect, SkillType.Ability),
                new CooldownComponent(pitchPerfect, 3m),
                new ConditionalPotencyComponent(pitchPerfect, ConditionalFunctions.PitchPerfectPotency),
                new UseConditionComponent(pitchPerfect, ConditionalFunctions.IsPitchPerfectUsable),
                new UsesRepertoireComponent(pitchPerfect)
            });

            sidewinder.AddComponents(new List<Component>
            {
                new SkillBaseComponent(sidewinder, SkillName.Sidewinder, SkillType.Ability),
                new CooldownComponent(sidewinder, 60m),
                new ConditionalPotencyComponent(sidewinder, ConditionalFunctions.SidewinderPotency)
            });

            ragingStrikes.AddComponents(new List<Component>
            {
                new SkillBaseComponent(ragingStrikes, SkillName.RagingStrikes, SkillType.Ability),
                new CooldownComponent(ragingStrikes, 80m),
                new StatusEffectComponent(ragingStrikes, AttributeType.Damage, StatusName.RagingStrikes, ActorType.Self, 20m, 1.1m)
            });

            barrage.AddComponents(new List<Component>
            {
                new SkillBaseComponent(barrage, SkillName.Barrage, SkillType.Ability),
                new CooldownComponent(barrage, 80m),
                new GenericStatusEffectComponent(barrage,StatusName.Barrage, 10m)
            });

            theWanderersMinuet.AddComponents(new List<Component>
            {
                new SkillBaseComponent(theWanderersMinuet, SkillName.TheWanderersMinuet, SkillType.Spell),
                new CooldownComponent(theWanderersMinuet, 80m),
                new PotencyComponent(theWanderersMinuet, 100),
                new SongComponent(theWanderersMinuet, SongName.TheWanderersMinuet)
            });

            magesBallad.AddComponents(new List<Component>
            {
                new SkillBaseComponent(magesBallad, SkillName.MagesBallad, SkillType.Spell),
                new CooldownComponent(magesBallad, 80m),
                new PotencyComponent(magesBallad, 100),
                new SongComponent(magesBallad, SongName.MagesBallad)
            });

            armysPaeon.AddComponents(new List<Component>
            {
                new SkillBaseComponent(armysPaeon, SkillName.ArmysPaeon, SkillType.Spell),
                new CooldownComponent(armysPaeon, 80m),
                new PotencyComponent(armysPaeon, 100),
                new SongComponent(armysPaeon, SongName.ArmysPaeon)
            });

            // Initializing player components
            player.AddComponents(new List<Component>
            {
                new BardComponent(player, new List<Entity>
                {
                    heavyShot,
                    straightShot,
                    causticBite,
                    stormBite,
                    ironJaws,
                    refulgentArrow,
                    bloodletter,
                    empyrealArrow,
                    pitchPerfect,
                    sidewinder,
                    ragingStrikes,
                    barrage,
                    theWanderersMinuet,
                    magesBallad,
                    armysPaeon
                }),
                new AttributesComponent(player, playerAttributes),
                new ModifierStateComponent(player),
                new AnimationLockComponent(player),
                new AutoAttackComponent(player, 100),
                new RiverOfBloodComponent(player, riverOfBloodSkillList),
                new SkillControlComponent(player, Job.Bard),
                // DEBUG: Setting enemy as player's target
                new TargetComponent(player, enemy)
            });

            // Initializing enemy components
            enemy.AddComponents(new List<Component>
                {
                    new HealthComponent(enemy, 0),
                    new OverTimeStateComponent(enemy)
                });

            // Making entities list
            _entities.Add(player);
            _entities.Add(enemy);
            _entities.Add(heavyShot);
            _entities.Add(straightShot);
            _entities.Add(causticBite);
            _entities.Add(stormBite);
            _entities.Add(ironJaws);
            _entities.Add(refulgentArrow);
            _entities.Add(bloodletter);
            _entities.Add(empyrealArrow);
            _entities.Add(pitchPerfect);
            _entities.Add(sidewinder);
            _entities.Add(ragingStrikes);
            _entities.Add(barrage);
            _entities.Add(theWanderersMinuet);
            _entities.Add(magesBallad);
            _entities.Add(armysPaeon);

            foreach (Entity e in _entities)
            {
                foreach (Component c in e.Components)
                {
                    if (c is AnimationLockComponent)
                    {
                        animationLockComponents.Add((AnimationLockComponent)c);
                    }
                    else if (c is AttributesComponent)
                    {
                        attributesComponents.Add((AttributesComponent)c);
                    }
                    else if (c is AutoAttackComponent)
                    {
                        autoAttackComponents.Add((AutoAttackComponent)c);
                    }
                    else if (c is BardComponent)
                    {
                        bardComponents.Add((BardComponent)c);
                    }
                    else if (c is ConditionalPotencyComponent)
                    {
                        conditionalPotencyComponents.Add((ConditionalPotencyComponent)c);
                    }
                    else if (c is CooldownComponent)
                    {
                        cooldownComponents.Add((CooldownComponent)c);
                    }
                    else if (c is DotEffectComponent)
                    {
                        dotEffectComponents.Add((DotEffectComponent)c);
                    }
                    else if (c is EnhancedEmpyrealArrowComponent)
                    {
                        enhancedEmpyrealArrowComponents.Add((EnhancedEmpyrealArrowComponent)c);
                    }
                    else if (c is GenericStatusEffectComponent)
                    {
                        genericStatusEffectComponents.Add((GenericStatusEffectComponent)c);
                    }
                    else if (c is HealthComponent)
                    {
                        healthComponents.Add((HealthComponent)c);
                    }
                    else if (c is IronJawsEffectComponent)
                    {
                        ironJawsEffectComponents.Add((IronJawsEffectComponent)c);
                    }
                    else if (c is ModifierStateComponent)
                    {
                        modifierStateComponents.Add((ModifierStateComponent)c);
                    }
                    else if (c is OverTimeStateComponent)
                    {
                        overtimeStateComponents.Add((OverTimeStateComponent)c);
                    }
                    else if (c is PotencyComponent)
                    {
                        potencyComponents.Add((PotencyComponent)c);
                    }
                    else if (c is RiverOfBloodComponent)
                    {
                        riverOfBloodComponents.Add((RiverOfBloodComponent)c);
                    }
                    else if (c is SkillBaseComponent)
                    {
                        skillBaseComponents.Add((SkillBaseComponent)c);
                    }
                    else if (c is SkillControlComponent)
                    {
                        skillControlComponents.Add((SkillControlComponent)c);
                    }
                    else if (c is SongComponent)
                    {
                        songComponents.Add((SongComponent)c);
                    }
                    else if (c is StatusEffectComponent)
                    {
                        statusEffectComponents.Add((StatusEffectComponent)c);
                    }
                    else if (c is StraighterShotEffectComponent)
                    {
                        straighterShotEffectComponents.Add((StraighterShotEffectComponent)c);
                    }
                    else if (c is TargetComponent)
                    {
                        targetComponents.Add((TargetComponent)c);
                    }
                    else if (c is UseConditionComponent)
                    {
                        useConditionComponents.Add((UseConditionComponent)c);
                    }
                    else if (c is UsesEnablerComponent)
                    {
                        usesEnablerComponents.Add((UsesEnablerComponent)c);
                    }
                    else if (c is UsesRepertoireComponent)
                    {
                        usesRepertoireComponents.Add((UsesRepertoireComponent)c);
                    }
                }
            }

            // Initializing systems
            //_systems.Add(new AIDebugSystem(player, bardComponents, cooldownComponents, modifierStateComponents, overtimeStateComponents, skillBaseComponents, targetComponents));
            _systems.Add(new NewAISystem(player, bardComponents, cooldownComponents, modifierStateComponents, overtimeStateComponents, skillBaseComponents, skillControlComponents, targetComponents));
            _systems.Add(new OverTimeSystem(bardComponents, cooldownComponents, healthComponents, modifierStateComponents, overtimeStateComponents, riverOfBloodComponents));
            _systems.Add(new RepertoireSystem(bardComponents, cooldownComponents, modifierStateComponents, riverOfBloodComponents));
            _systems.Add(new BuffSystem(bardComponents, modifierStateComponents));
            _systems.Add(new AutoAttackSystem(attributesComponents, autoAttackComponents, bardComponents, healthComponents, modifierStateComponents, targetComponents));
            _systems.Add(new SkillSystem(animationLockComponents, attributesComponents, bardComponents, conditionalPotencyComponents, cooldownComponents, dotEffectComponents, enhancedEmpyrealArrowComponents, genericStatusEffectComponents, healthComponents, ironJawsEffectComponents, modifierStateComponents, overtimeStateComponents, potencyComponents, skillBaseComponents, skillControlComponents, songComponents, statusEffectComponents, straighterShotEffectComponents, targetComponents, useConditionComponents, usesEnablerComponents, usesRepertoireComponents));
        }

        public decimal Simulate (SimulationParameters target, decimal input)
        {
                if (target == SimulationParameters.TimeTarget)
                {
                    // Starts the timer
                    decimal fastTimer = 0;

                    while (fastTimer < input)
                    {
                        foreach (ISystem sys in _systems)
                        {
                            sys.Update(fastTimer, keyboard, null);
                        };
                        fastTimer += 0.01m;
                    }

                    return healthComponents.Find(x => x.Parent == enemy).DamageTaken / fastTimer;

                }
                else if (target == SimulationParameters.DamageTarget)
                {
                    // Starts the timer
                    decimal fastTimer = 0;

                    while (healthComponents.Find(x => x.Parent == enemy).DamageTaken < input)
                    {
                        foreach (ISystem sys in _systems)
                        {
                            sys.Update(fastTimer, keyboard, null);
                        };
                        fastTimer += 0.01m;
                    }

                    return healthComponents.Find(x => x.Parent == enemy).DamageTaken / fastTimer;

                }
            return -1;
        }

        public decimal Simulate(SimulationParameters target, decimal input, LogData log)
        {
            if (target == SimulationParameters.TimeTarget)
            {
                // Starts the timer
                decimal fastTimer = 0;

                while (fastTimer < input)
                {
                    foreach (ISystem sys in _systems)
                    {
                        sys.Update(fastTimer, keyboard, log);
                    };
                    fastTimer += 0.01m;
                }

                return healthComponents.Find(x => x.Parent == enemy).DamageTaken / fastTimer;

            }
            else if (target == SimulationParameters.DamageTarget)
            {
                // Starts the timer
                decimal fastTimer = 0;

                while (healthComponents.Find(x => x.Parent == enemy).DamageTaken < input)
                {
                    foreach (ISystem sys in _systems)
                    {
                        sys.Update(fastTimer, keyboard, log);
                    };
                    fastTimer += 0.01m;
                }

                return healthComponents.Find(x => x.Parent == enemy).DamageTaken / fastTimer;

            }
            return -1;
        }

        public void Reinitialize()
        {
            // Zeroing components

            foreach (AnimationLockComponent c in animationLockComponents)
            {
                c.Start = 0;
            }
            foreach (AutoAttackComponent c in autoAttackComponents)
            {
                c.NextAuto = 0;
            }
            foreach (BardComponent c in bardComponents)
            {
                c.Repertoire = 0;
                c.Song = SongName.None;
                c.SongDuration = 0;
                c.SongStart = 0;
            }
            foreach (CooldownComponent c in cooldownComponents)
            {
                c.Start = 0;
                c.UsableAt = 0;

            }
            foreach (HealthComponent c in healthComponents)
            {
                c.Amount = 0;
                c.DamageTaken = 0;
            }
            foreach (SkillControlComponent c in skillControlComponents)
            {
                foreach (SkillName s in c.SkillControlList)
                {
                    c.SkillControlDictionary[s] = false;
                }
            }
            foreach (ModifierStateComponent c in modifierStateComponents)
            {
                c.BuffList = new List<Buff>();
                c.EnablerList = new List<Enabler>();
                c.BuffDictionary = new BuffsDictionary();

            }
            foreach (OverTimeStateComponent c in overtimeStateComponents)
            {
                c.DotList = new List<DoT>();
                c.Offset  = (new Random().Next(0, 300) * 10).MilliToSeconds();
            }
        }
    }
}

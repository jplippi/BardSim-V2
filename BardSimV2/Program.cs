using ExtensionMethods;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ISystem> _systems = new List<ISystem>();
            List<Entity> _entities = new List<Entity>();

            // Sets up a keyboard to watch for inputs
            Keyboard keyboard = new Keyboard();

            // Initializing player entities
            Entity player =  new Entity();

            // Initializing enemy entities
            Entity enemy = new Entity();

            // Initializing skill entities
            Entity heavyShot = new Entity();
            Entity straightShot = new Entity();
            Entity causticBite = new Entity();
            Entity stormBite = new Entity();
            Entity ironJaws = new Entity();
            Entity refulgentArrow = new Entity();
            Entity bloodletter = new Entity();
            Entity empyrealArrow = new Entity();
            Entity pitchPerfect = new Entity();
            Entity sidewinder = new Entity();
            Entity ragingStrikes = new Entity();
            Entity barrage = new Entity();
            Entity theWanderersMinuet = new Entity();
            Entity magesBallad = new Entity();
            Entity armysPaeon = new Entity();

            List<Entity> gcdSkillList = new List<Entity>
            {
                heavyShot,
                straightShot,
                causticBite,
                stormBite,
                ironJaws,
                refulgentArrow
            };


            List<Entity> riverOfBloodSkillList = new List<Entity>
            {
                bloodletter,
                //rainOfDeath
            };

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
                    new StatusEffectComponent(straightShot, AttributeType.CriticalHitRate, StatusName.StraighterShot, ActorType.Self, 30, 10)
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
                    new DotEffectComponent(stormBite, DotName.StormBite, 30m, 55)
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
                    new AttributesComponent(player, 261, 2830, 2276, 249, 236, 2315, 1153, 1677, 801, 364, 364, 292, 105, 3.04m),
                    new ModifierStateComponent(player),
                    new AnimationLockComponent(player),
                    new RiverOfBloodComponent(player, riverOfBloodSkillList),
                    new KeyMappingComponent(player, new List<KeyBind>
                    {
                        new KeyBind(SkillName.HeavyShot, Keys.Num1),
                        new KeyBind(SkillName.StraightShot, Keys.Num2),
                        new KeyBind(SkillName.CausticBite, Keys.Num4),
                        new KeyBind(SkillName.Stormbite, Keys.Num3),
                        new KeyBind(SkillName.IronJaws, Keys.Num5),
                        new KeyBind(SkillName.RefulgentArrow, Keys.F3),
                        new KeyBind(SkillName.Bloodletter, Keys.R),
                        new KeyBind(SkillName.EmpyrealArrow, Keys.Num6),
                        new KeyBind(SkillName.PitchPerfect, Keys.T),
                        new KeyBind(SkillName.Sidewinder, Keys.Num7),
                        new KeyBind(SkillName.RagingStrikes, Keys.F1),
                        new KeyBind(SkillName.Barrage, Keys.F2),
                        new KeyBind(SkillName.TheWanderersMinuet, Keys.M1),
                        new KeyBind(SkillName.MagesBallad, Keys.M2),
                        new KeyBind(SkillName.ArmysPaeon, Keys.M3)
                    }),
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

            // Making component lists
            List<AnimationLockComponent> animationLockComponents = new List<AnimationLockComponent>();
            List<AttributesComponent> attributesComponents = new List<AttributesComponent>();
            List<BardComponent> bardComponents = new List<BardComponent>();
            List<ConditionalPotencyComponent> conditionalPotencyComponents = new List<ConditionalPotencyComponent>();
            List<CooldownComponent> cooldownComponents = new List<CooldownComponent>();
            List<DotEffectComponent> dotEffectComponents = new List<DotEffectComponent>();
            List<EnhancedEmpyrealArrowComponent> enhancedEmpyrealArrowComponents = new List<EnhancedEmpyrealArrowComponent>();
            List<GenericStatusEffectComponent> genericStatusEffectComponents = new List<GenericStatusEffectComponent>();
            List<HealthComponent> healthComponents = new List<HealthComponent>();
            List<IronJawsEffectComponent> ironJawsEffectComponents = new List<IronJawsEffectComponent>();
            List<KeyMappingComponent> keyMappingComponents = new List<KeyMappingComponent>();
            List<ModifierStateComponent> modifierStateComponents = new List<ModifierStateComponent>();
            List<OverTimeStateComponent> overtimeStateComponents = new List<OverTimeStateComponent>();
            List<PotencyComponent> potencyComponents = new List<PotencyComponent>();
            List<RiverOfBloodComponent> riverOfBloodComponents = new List<RiverOfBloodComponent>();
            List<SkillBaseComponent> skillBaseComponents = new List<SkillBaseComponent>();
            List<SongComponent> songComponents = new List<SongComponent>();
            List<StatusEffectComponent> statusEffectComponents = new List<StatusEffectComponent>();
            List<StraighterShotEffectComponent> straighterShotEffectComponents = new List<StraighterShotEffectComponent>();
            List<TargetComponent> targetComponents = new List<TargetComponent>();
            List<UseConditionComponent> useConditionComponents = new List<UseConditionComponent>();
            List<UsesEnablerComponent> usesEnablerComponents = new List<UsesEnablerComponent>();
            List<UsesRepertoireComponent> usesRepertoireComponents = new List<UsesRepertoireComponent>();

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
                    else if (c is KeyMappingComponent)
                    {
                        keyMappingComponents.Add((KeyMappingComponent)c);
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
            _systems.Add(new AIDebugSystem(player, bardComponents, cooldownComponents, modifierStateComponents, overtimeStateComponents, skillBaseComponents, targetComponents));
            _systems.Add(new KeystrokeListenerSystem(keyMappingComponents));
            _systems.Add(new OverTimeSystem(bardComponents, cooldownComponents, healthComponents, modifierStateComponents, overtimeStateComponents, riverOfBloodComponents));
            _systems.Add(new RepertoireSystem(bardComponents, cooldownComponents, modifierStateComponents, riverOfBloodComponents));
            _systems.Add(new BuffSystem(bardComponents, modifierStateComponents));
            _systems.Add(new SkillSystem(animationLockComponents, attributesComponents, bardComponents, conditionalPotencyComponents, cooldownComponents, dotEffectComponents, enhancedEmpyrealArrowComponents, genericStatusEffectComponents, healthComponents, ironJawsEffectComponents, keyMappingComponents, modifierStateComponents, overtimeStateComponents, potencyComponents, skillBaseComponents, songComponents, statusEffectComponents, straighterShotEffectComponents, targetComponents, useConditionComponents, usesEnablerComponents, usesRepertoireComponents));

            // Asks for run mode and duration
            bool invalidMode = true;
            bool invalidDuration = true;

            while(invalidMode == true || invalidDuration == true)
            {
                Console.WriteLine("Enter 'R' for real-time mode, or 'F' for fast mode:");
                char mode = Console.ReadLine()[0];

                Console.WriteLine("Input the duration in seconds:");
                if (decimal.TryParse(Console.ReadLine(),out decimal duration))
                {
                    invalidDuration = false;
                }

                if (mode == 'R')
                {
                    invalidMode = false;

                    // Starts the timer
                    Stopwatch realTimer = new Stopwatch();
                    realTimer.Start();

                    while (realTimer.ElapsedMilliseconds.MilliToSeconds() < duration)
                    {
                        foreach (ISystem sys in _systems)
                        {
                            sys.Update(realTimer.ElapsedMilliseconds.MilliToSeconds(), keyboard);
                        };

                    }
                    realTimer.Stop();
                    //DEBUG: Total damage
                    Console.WriteLine("\nTotal damage done: {0}\n Total DPS: {1:0.00}\n Duration: {2:00.00} seconds.", healthComponents.Find(x => x.Parent == enemy).DamageTaken, healthComponents.Find(x => x.Parent == enemy).DamageTaken / realTimer.ElapsedMilliseconds.MilliToSeconds(), realTimer.ElapsedMilliseconds.MilliToSeconds());

                }
                else if(mode == 'F')
                {
                    invalidMode = false;

                    // Starts the timer
                    decimal fastTimer = 0;

                    while (fastTimer < duration)
                    {
                        foreach (ISystem sys in _systems)
                        {
                            sys.Update(fastTimer, keyboard);
                        };
                        fastTimer += 0.01m;
                    }

                    //DEBUG: Total damage
                    Console.WriteLine("\nTotal damage done: {0}\n Total DPS: {1:0.00}\n Duration: {2:00.00} seconds.", healthComponents.Find(x => x.Parent == enemy).DamageTaken, healthComponents.Find(x => x.Parent == enemy).DamageTaken / fastTimer, fastTimer);

                }

            }
            while (true)
            {

            }

        }
    }
}

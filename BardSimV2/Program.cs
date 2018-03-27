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
            Entity refulgentArrow = new Entity();
            Entity bloodletter = new Entity();
            Entity empyrealArrow = new Entity();
            Entity pitchPerfect = new Entity();

            List<Entity> gcdSkillList = new List<Entity>
            {
                heavyShot,
                straightShot,
                causticBite,
                stormBite,
                refulgentArrow
            };

            heavyShot.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(heavyShot, SkillName.HeavyShot, SkillType.Weaponskill),
                    new CooldownComponent(heavyShot, 2.5m, gcdSkillList),
                    new PotencyComponent(heavyShot, 150),
                    // TODO: Add special effect component
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

            refulgentArrow.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(refulgentArrow, SkillName.RefulgentArrow, SkillType.Weaponskill),
                    new CooldownComponent(refulgentArrow, 2.5m, gcdSkillList),
                    new PotencyComponent(refulgentArrow, 300)
                    // TODO: Add special effect component
                    // TODO: Add conditional component
                });

            bloodletter.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(bloodletter, SkillName.Bloodletter, SkillType.Ability),
                    new CooldownComponent(bloodletter, 15m),
                    new PotencyComponent(bloodletter, 130)
                    // TODO: Add shared cooldown component
                });

            empyrealArrow.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(empyrealArrow, SkillName.EmpyrealArrow, SkillType.Weaponskill),
                    new CooldownComponent(empyrealArrow, 15m),
                    new PotencyComponent(empyrealArrow, 230)
                    // TODO: Add special effect component
                });

            pitchPerfect.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(pitchPerfect, SkillName.PitchPerfect, SkillType.Ability),
                    new CooldownComponent(pitchPerfect, 3m),
                    new PotencyComponent(pitchPerfect, 100)
                    // TODO: Add special potency component
                    // TODO: Add special condition component
                });

            // Initializing player components
            player.AddComponents(new List<Component>
                {
                    new JobComponent(player, Jobs.Bard, new List<Entity>
                    {
                        heavyShot,
                        straightShot,
                        causticBite,
                        stormBite,
                        refulgentArrow,
                        bloodletter,
                        empyrealArrow,
                        pitchPerfect
                    }),
                    new AttributesComponent(player, 261, 2830, 2276, 249, 236, 2315, 1153, 1677, 801, 364, 364, 292, 105, 3.04m),
                    new ModifierStateComponent(player),
                    new KeyMappingComponent(player, new List<KeyBind>
                    {
                        new KeyBind(SkillName.HeavyShot, Keys.Num1),
                        new KeyBind(SkillName.StraightShot, Keys.Num2),
                        new KeyBind(SkillName.CausticBite, Keys.Num4),
                        new KeyBind(SkillName.Stormbite, Keys.Num3),
                        new KeyBind(SkillName.RefulgentArrow, Keys.F3),
                        new KeyBind(SkillName.Bloodletter, Keys.R),
                        new KeyBind(SkillName.EmpyrealArrow, Keys.Num6),
                        new KeyBind(SkillName.PitchPerfect, Keys.T)
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
            _entities.Add(refulgentArrow);
            _entities.Add(bloodletter);
            _entities.Add(empyrealArrow);
            _entities.Add(pitchPerfect);

            // Making component lists
            List<AttributesComponent> attributesComponents = new List<AttributesComponent>();
            List<CooldownComponent> cooldownComponents = new List<CooldownComponent>();
            List<DotEffectComponent> dotEffectComponents = new List<DotEffectComponent>();
            List<HealthComponent> healthComponents = new List<HealthComponent>();
            List<JobComponent> jobComponents = new List<JobComponent>();
            List<KeyMappingComponent> keyMappingComponents = new List<KeyMappingComponent>();
            List<ModifierStateComponent> modifierStateComponents = new List<ModifierStateComponent>();
            List<OverTimeStateComponent> overtimeStateComponents = new List<OverTimeStateComponent>();
            List<PotencyComponent> potencyComponents = new List<PotencyComponent>();
            List<SkillBaseComponent> skillBaseComponents = new List<SkillBaseComponent>();
            List<StatusEffectComponent> statusEffectComponents = new List<StatusEffectComponent>();
            List<TargetComponent> targetComponents = new List<TargetComponent>();

            foreach (Entity e in _entities)
            {
                foreach (Component c in e.Components)
                {
                    if (c is AttributesComponent)
                    {
                        attributesComponents.Add((AttributesComponent)c);
                    }
                    else if (c is CooldownComponent)
                    {
                        cooldownComponents.Add((CooldownComponent)c);
                    }
                    else if (c is DotEffectComponent)
                    {
                        dotEffectComponents.Add((DotEffectComponent)c);
                    }
                    else if (c is HealthComponent)
                    {
                        healthComponents.Add((HealthComponent)c);
                    }
                    else if (c is JobComponent)
                    {
                        jobComponents.Add((JobComponent)c);
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
                    else if (c is SkillBaseComponent)
                    {
                        skillBaseComponents.Add((SkillBaseComponent)c);
                    }
                    else if (c is StatusEffectComponent)
                    {
                        statusEffectComponents.Add((StatusEffectComponent)c);
                    }
                    else if (c is TargetComponent)
                    {
                        targetComponents.Add((TargetComponent)c);
                    }
                }
            }

            // Initializing systems
            _systems.Add(new AIDebugSystem(player, modifierStateComponents, overtimeStateComponents, skillBaseComponents, targetComponents));
            _systems.Add(new KeystrokeListenerSystem(keyMappingComponents));
            _systems.Add(new OverTimeSystem(healthComponents, overtimeStateComponents));
            _systems.Add(new BuffSystem(modifierStateComponents));
            _systems.Add(new SkillSystem(attributesComponents, cooldownComponents, dotEffectComponents, healthComponents, jobComponents, keyMappingComponents, modifierStateComponents, overtimeStateComponents, potencyComponents, skillBaseComponents, statusEffectComponents, targetComponents));

            // Asks for run mode
            bool invalidMode = true;

            while(invalidMode == true)
            {
                Console.WriteLine("Enter 'R' for real-time mode, or 'F' for fast mode:");
                char mode = Console.ReadLine()[0];
       
                if(mode == 'R')
                {
                    invalidMode = false;

                    // Starts the timer
                    Stopwatch realTimer = new Stopwatch();
                    realTimer.Start();

                    while (true)
                    {
                        foreach (ISystem sys in _systems)
                        {
                            sys.Update((ulong)realTimer.ElapsedMilliseconds, keyboard);
                        };

                    }
                }else if(mode == 'F')
                {
                    invalidMode = false;

                    // Starts the timer
                    ulong fastTimer = 0;

                    while (true)
                    {
                        foreach (ISystem sys in _systems)
                        {
                            sys.Update(fastTimer, keyboard);
                        };
                        fastTimer += 10;
                    }
                }
            }
        }
    }
}

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
            Entity refulgentArrow = new Entity();
            Entity bloodletter = new Entity();
            Entity empyrealArrow = new Entity();
            Entity pitchPerfect = new Entity();

            List<Entity> gcdSkillList = new List<Entity>
            {
                heavyShot,
                straightShot,
                refulgentArrow
            };

            heavyShot.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(heavyShot, SkillName.HeavyShot, SkillType.Weaponskill, new Cooldown(2.5m)),
                    new PotencyComponent(heavyShot, 150),
                    new SharedCooldownComponent(heavyShot, gcdSkillList)
                    // TODO: Add special effect component
                });

            straightShot.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(straightShot, SkillName.StraightShot, SkillType.Weaponskill, new Cooldown(2.5m)),
                    new PotencyComponent(straightShot, 140),
                    new ApplyBuffEffectComponent(straightShot, AttributeType.CriticalHitRate, ActorType.Self, 30, 10),
                    new SharedCooldownComponent(straightShot, gcdSkillList)
                });

            refulgentArrow.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(refulgentArrow, SkillName.RefulgentArrow, SkillType.Weaponskill, new Cooldown(2.5m)),
                    new PotencyComponent(refulgentArrow, 300),
                    new SharedCooldownComponent(refulgentArrow, gcdSkillList)
                    // TODO: Add special effect component
                    // TODO: Add conditional component
                });

            bloodletter.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(bloodletter, SkillName.Bloodletter, SkillType.Ability, new Cooldown(15m)),
                    new PotencyComponent(bloodletter, 130)
                    // TODO: Add shared cooldown component
                });

            empyrealArrow.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(empyrealArrow, SkillName.EmpyrealArrow, SkillType.Weaponskill, new Cooldown(15m)),
                    new PotencyComponent(empyrealArrow, 230)
                    // TODO: Add special effect component
                });

            pitchPerfect.AddComponents(new List<Component>
                {
                    new SkillBaseComponent(pitchPerfect, SkillName.PitchPerfect, SkillType.Ability, new Cooldown(3m)),
                    new PotencyComponent(pitchPerfect, 100)
                    // TODO: Add shared cooldown component
                    // TODO: Add special potency component
                });

            // Initializing player components
            player.AddComponents(new List<Component>
                {
                    new JobComponent(player, Jobs.Bard, new List<Entity>
                    {
                        heavyShot,
                        straightShot,
                        refulgentArrow,
                        bloodletter,
                        empyrealArrow,
                        pitchPerfect
                    }),
                    new AttributesComponent(player, 261, 2801, 2237, 249, 236, 2278, 1286, 1552, 854, 364, 364, 292, 105, 3.04m),
                    new BuffStateComponent(player),
                    new KeyMappingComponent(player, new List<KeyBind>
                    {
                        new KeyBind(SkillName.HeavyShot, Keys.Num1),
                        new KeyBind(SkillName.StraightShot, Keys.Num2),
                        new KeyBind(SkillName.RefulgentArrow, Keys.F3),
                        new KeyBind(SkillName.Bloodletter, Keys.R),
                        new KeyBind(SkillName.EmpyrealArrow, Keys.Num6),
                        new KeyBind(SkillName.PitchPerfect, Keys.T)
                    }),
                    // DEBUG: Setting enemy as player's target
                    new TargetComponent(player, enemy)
                });

            // Initializing enemy components
            enemy.AddComponents(new List<Component> { new HealthComponent(enemy, 0) });

            // Making entities list
            _entities.Add(player);
            _entities.Add(enemy);
            _entities.Add(heavyShot);
            _entities.Add(straightShot);
            _entities.Add(refulgentArrow);
            _entities.Add(bloodletter);
            _entities.Add(empyrealArrow);
            _entities.Add(pitchPerfect);

            // Making component lists
            List<ApplyBuffEffectComponent> applyBuffEffectComponents = new List<ApplyBuffEffectComponent>();
            List<AttributesComponent> attributesComponents = new List<AttributesComponent>();
            List<BuffStateComponent> buffStateComponents = new List<BuffStateComponent>();
            List<HealthComponent> healthComponents = new List<HealthComponent>();
            List<JobComponent> jobComponents = new List<JobComponent>();
            List<KeyMappingComponent> keyMappingComponents = new List<KeyMappingComponent>();
            List<PotencyComponent> potencyComponents = new List<PotencyComponent>();
            List<SharedCooldownComponent> sharedCooldownComponents = new List<SharedCooldownComponent>();
            List<SkillBaseComponent> skillBaseComponents = new List<SkillBaseComponent>();
            List<TargetComponent> targetComponents = new List<TargetComponent>();

            foreach (Entity e in _entities)
            {
                foreach (Component c in e.Components)
                {
                    if (c is ApplyBuffEffectComponent)
                    {
                        applyBuffEffectComponents.Add((ApplyBuffEffectComponent)c);
                    } else if (c is AttributesComponent)
                    {
                        attributesComponents.Add((AttributesComponent)c);
                    } else if (c is BuffStateComponent)
                    {
                        buffStateComponents.Add((BuffStateComponent)c);
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
                    else if (c is PotencyComponent)
                    {
                        potencyComponents.Add((PotencyComponent)c);
                    }
                    else if (c is SharedCooldownComponent)
                    {
                        sharedCooldownComponents.Add((SharedCooldownComponent)c);
                    }
                    else if (c is SkillBaseComponent)
                    {
                        skillBaseComponents.Add((SkillBaseComponent)c);
                    }
                    else if (c is TargetComponent)
                    {
                        targetComponents.Add((TargetComponent)c);
                    }
                }
            }

            // Initializing systems
            _systems.Add(new AIDebugSystem(player, buffStateComponents, skillBaseComponents));
            _systems.Add(new KeystrokeListenerSystem(keyMappingComponents));
            _systems.Add(new BuffSystem(buffStateComponents));
            _systems.Add(new SkillSystem(applyBuffEffectComponents, attributesComponents, buffStateComponents, healthComponents, jobComponents, keyMappingComponents, potencyComponents, sharedCooldownComponents, skillBaseComponents, targetComponents));

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

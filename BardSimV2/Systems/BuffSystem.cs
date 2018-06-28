using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class BuffSystem : ISystem
    {
        List<BardComponent> bardComponents;
        List<ModifierStateComponent> modifierStateComponents;

        List<Buff> buffToBeRemoved = new List<Buff>();
        List<Enabler> enablerToBeRemoved = new List<Enabler>();

        public BuffSystem(List<BardComponent> bardComponents, List<ModifierStateComponent> modifierStateComponents)
        {
            this.bardComponents = bardComponents;
            this.modifierStateComponents = modifierStateComponents;
        }

        public void Update(decimal timer, Keyboard keyboard, LogData log)
        {
            foreach (ModifierStateComponent modStateComp in modifierStateComponents)
            {
                foreach (Buff buff in modStateComp.BuffList)
                {
                    // Remove buffs if they're expired
                    if (timer - buff.Start > buff.Duration)
                    {
                        if(buff.Type != AttributeType.Damage)
                        {
                            modStateComp.BuffDictionary[buff.Type] -= buff.Modifier;
                        }

                        buffToBeRemoved.Add(buff);

                    // Activates the buff if it's not active
                    }else if (!buff.IsActive)
                    {
                        if (buff.Type != AttributeType.Damage)
                        {
                            modStateComp.BuffDictionary[buff.Type] += buff.Modifier;
                        }

                        buff.IsActive = true;
                    }
                }

                foreach (Buff buff in buffToBeRemoved)
                {
                    modStateComp.BuffList.Remove(buff);
                }
                buffToBeRemoved = new List<Buff>();

                foreach (Enabler enabler in modStateComp.EnablerList)
                {
                    // Remove enabler if they're expired
                    if(timer - enabler.Start > enabler.Duration)
                    {
                        enablerToBeRemoved.Add(enabler);
                    }
                }

                foreach (Enabler enabler in enablerToBeRemoved)
                {
                    modStateComp.EnablerList.Remove(enabler);
                }
                enablerToBeRemoved = new List<Enabler>();

            }

            foreach (BardComponent brdComp in bardComponents)
            {
                if(brdComp.Song != SongName.None)
                {
                    if(brdComp.SongStart + brdComp.SongDuration <= timer)
                    {

                        // Logic for ending AP buff
                        Buff apEffect = null;
                        ModifierStateComponent brdModComp = modifierStateComponents.Find(x => x.Parent == brdComp.Parent);

                        if(brdModComp != null)
                        {
                            apEffect = brdModComp.BuffList.Find(x => x.Name == StatusName.ArmysPaeon);
                        }

                        if(apEffect != null)
                        {
                            brdModComp.BuffDictionary[apEffect.Type] -= apEffect.Modifier;
                            brdModComp.BuffList.Remove(apEffect);
                        }
                        
                        // Resetting song
                        brdComp.Song = SongName.None;
                        brdComp.SongStart = 0;
                        brdComp.SongDuration = 0;
                        brdComp.Repertoire = 0;
                    }
                }
            }
        }
    }
}

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
        List<ModifierStateComponent> modifierStateComponents;
        List<Buff> buffToBeRemoved = new List<Buff>();
        List<Enabler> enablerToBeRemoved = new List<Enabler>();

        public BuffSystem(List<ModifierStateComponent> modifierStateComponents)
        {
            this.modifierStateComponents = modifierStateComponents;
        }

        public void Update(decimal timer, Keyboard keyboard)
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

            }
        }
    }
}

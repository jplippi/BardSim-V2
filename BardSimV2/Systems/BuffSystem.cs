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
        List<Buff> toBeRemoved = new List<Buff>();

        public BuffSystem(List<ModifierStateComponent> modifierStateComponents)
        {
            this.modifierStateComponents = modifierStateComponents;
        }

        public void Update(ulong timer, Keyboard keyboard)
        {
            foreach (ModifierStateComponent modStateComp in modifierStateComponents)
            {
                foreach (Buff buff in modStateComp.BuffList)
                {
                    // Remove buffs if they're expired
                    if (timer - buff.Start > (ulong)buff.Duration.SecondsToMilli())
                    {
                        if(buff.Type != AttributeType.Damage)
                        {
                            modStateComp.BuffDictionary[buff.Type] -= buff.Modifier;
                        }

                        toBeRemoved.Add(buff);

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

                foreach (Buff buff in toBeRemoved)
                {
                    modStateComp.BuffList.Remove(buff);
                }
            }
        }
    }
}

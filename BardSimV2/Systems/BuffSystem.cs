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
        List<BuffStateComponent> buffStateComponents;
        List<Buff> toBeRemoved = new List<Buff>();

        public BuffSystem(List<BuffStateComponent> buffStateComponents)
        {
            this.buffStateComponents = buffStateComponents;
        }

        public void Update(ulong timer, Keyboard keyboard)
        {
            foreach (BuffStateComponent buffStateComp in buffStateComponents)
            {
                foreach (Buff buff in buffStateComp.BuffList)
                {
                    // Remove buffs if they're expired
                    if (timer - buff.Start > (ulong)buff.Duration.SecondsToMilli())
                    {
                        if(buff.Type != AttributeType.Damage)
                        {
                            buffStateComp.BuffDictionary[buff.Type] -= buff.Modifier;
                        }

                        toBeRemoved.Add(buff);

                    // Activates the buff if it's not active
                    }else if (!buff.IsActive)
                    {
                        buffStateComp.BuffDictionary[buff.Type] += buff.Modifier;
                        buff.IsActive = true;
                    }
                }

                foreach (Buff buff in toBeRemoved)
                {
                    buffStateComp.BuffList.Remove(buff);
                }
            }
        }
    }
}

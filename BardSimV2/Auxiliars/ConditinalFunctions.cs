using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    static class ConditionalFunctions
    {
        static public bool RefulgentArrow(ModifierStateComponent modComp, Component brdComp)
        {
            bool isUsable = false;
            Enabler toRemove = null;

            foreach(Enabler e in modComp.EnablerList)
            {
                if(e.Name == StatusName.StraighterShot)
                {
                    isUsable = true;
                    toRemove = e;
                    break;
                }
            }

            modComp.EnablerList.Remove(toRemove);
            return isUsable;
        }
    }
}

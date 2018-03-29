using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    static class ConditionalFunctions
    {
        static public bool IsRefulgentArrowUsable(ModifierStateComponent modComp, Component jobComp)
        {
            bool isUsable = false;

            foreach(Enabler e in modComp.EnablerList)
            {
                if(e.Name == StatusName.StraighterShot)
                {
                    isUsable = true;
                    break;
                }
            }

            return isUsable;
        }

        static public bool IsPitchPerfectUsable(ModifierStateComponent modComp, Component jobComp)
        {
            BardComponent brdComp = (BardComponent)jobComp;

            if (brdComp.Song == SongName.TheWanderersMinuet && brdComp.Repertoire > 0)
            {
                return true;
            }

            return false;
        }

        static public int PitchPerfectPotency(Entity user, OverTimeStateComponent targOtStateComp, Component jobComp)
        {
            BardComponent brdComp = (BardComponent)jobComp;

            switch (brdComp.Repertoire)
            {
                case 1:
                    return 100;
                case 2:
                    return 240;
                case 3:
                    return 420;
                default:
                    return 0;
            }
        }

        static public int SidewinderPotency(Entity user, OverTimeStateComponent targOtStateComp, Component jobComp)
        {
            int dotCounter = 0;
            foreach (DoT dot in targOtStateComp.DotList.FindAll(x => (x.Name == DotName.CausticBite && x.IsActive == true && x.UserSource == user) || (x.Name == DotName.StormBite && x.IsActive == true && x.UserSource == user)))
            {
                dotCounter++;
            }

            if (dotCounter == 2)
            {
                return 260;
            }
            else if(dotCounter == 2)
            {
                return 175;
            }
            return 100;

        }
    }
}

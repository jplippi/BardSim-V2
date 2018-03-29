using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    static class Constants
    {
        private static decimal animationLock = 0.7m;
        private static decimal cooldownResetLock = 1.0m;

        public static decimal AnimationLock { get => animationLock; }
        public static decimal CooldownResetLock { get => cooldownResetLock; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class KeyBind
    {
        private SkillName command;
        private Keys key;
        private bool isActive;

        public SkillName Command { get => command; }
        public Keys Key { get => key; set => key = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public KeyBind(SkillName command, Keys key, bool isActive = false)
        {
            this.command = command;
            this.key = key;
            this.isActive = isActive;
        }
    }
}

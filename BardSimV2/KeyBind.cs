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
        private Key key;
        private bool isActive;

        public SkillName Command { get => command; }
        public Key Key { get => key; set => key = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public KeyBind(SkillName command, Key key, bool isActive = false)
        {
            this.command = command;
            this.key = key;
            this.isActive = isActive;
        }
    }
}

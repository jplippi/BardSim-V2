using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class KeyBind
    {
        private string command;
        private string key;
        private bool isActive;

        public string Command { get => command; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string Key { get => key; set => key = value; }

        public KeyBind(string command, string key, bool isActive = false)
        {
            this.command = command;
            this.key = key;
            this.isActive = isActive;
        }
    }
}

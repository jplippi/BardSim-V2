using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Key
    {
        private bool isPressed;

        public bool IsPressed { get => isPressed; set => isPressed =value; }

        public Key()
        {
            isPressed = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class KeystrokeListenerSystem : ISystem
    {
        // TODO: To be populated with actual components
        List<KeyMappingComponent> components = new List<KeyMappingComponent>();

        public void Update(Stopwatch timer, Keyboard keyboard)
        {
            if (keyboard.Tab.IsPressed)
            {
                foreach (KeyMappingComponent keymapComp in components)
                {
                    foreach(KeyBind keybind in keymapComp.KeyBinds)
                    {
                        if( keybind.Key == keyboard.Tab)
                        {
                            keybind.IsActive = true;
                        }
                    }
                }
            }
            else
            {
                foreach (KeyMappingComponent keymapComp in components)
                {
                    foreach (KeyBind keybind in keymapComp.KeyBinds)
                    {
                        if (keybind.Key == keyboard.Tab)
                        {
                            keybind.IsActive = false;
                        }
                    }
                }
            }
        }
    }
}

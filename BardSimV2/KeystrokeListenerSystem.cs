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
        List<KeyMappingComponent> keyMappingComponents;

        public KeystrokeListenerSystem(List<KeyMappingComponent> keyMappingComponents)
        {
            this.keyMappingComponents = keyMappingComponents;
        }

        public void Update(Stopwatch timer, Keyboard keyboard)
        {
            foreach (KeyValuePair<Keys,bool> key in keyboard.keysDictionary)
            if (key.Value)
            {
                foreach (KeyMappingComponent keymapComp in keyMappingComponents)
                {
                    foreach(KeyBind keybind in keymapComp.KeyBinds)
                    {
                        if( keybind.Key == key.Key)
                        {
                            keybind.IsActive = true;
                        }
                    }
                }
            }
            else
            {
                foreach (KeyMappingComponent keymapComp in keyMappingComponents)
                {
                    foreach (KeyBind keybind in keymapComp.KeyBinds)
                    {
                        if (keybind.Key == key.Key)
                        {
                            keybind.IsActive = false;
                        }
                    }
                }
            }
        }
    }
}

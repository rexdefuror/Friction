using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class KeyBind
    {
        public KeyBind(KeyCode keyCode, KeyAction keyAction)
        {
            KeyCode = keyCode;
            KeyAction = keyAction;
        }

        public KeyCode KeyCode { get; set; }
        public KeyAction KeyAction { get; set; }

    }
}

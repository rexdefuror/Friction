using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Utility
{
    public class KeyAssigner
    {
        public List<KeyBind> KeyBinds { get; set; }

        public void AssignDefaultKeys()
        {
            KeyBinds = new List<KeyBind>();
            KeyBinds.Add(new KeyBind(KeyCode.UpArrow, KeyAction.Jump));
            KeyBinds.Add(new KeyBind(KeyCode.LeftArrow, KeyAction.MoveBackward));
            KeyBinds.Add(new KeyBind(KeyCode.RightArrow, KeyAction.MoveForward));
        }
    }
}

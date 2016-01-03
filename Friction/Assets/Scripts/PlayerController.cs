using System.Linq;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {

        public float MoveSpeed;
        public float JumpHeight;
        public float FrictionDamage;
        public KeyAssigner KeyAssigner;

        // Use this for initialization
        void Start()
        {
            KeyAssigner = new KeyAssigner();
            KeyAssigner.AssignDefaultKeys();
        }

        void OnGUI()
        {
        }

        // Update is called once per frame
        void Update()
        {
            var player = GetComponent<Rigidbody2D>();
            if (Input.anyKey)
            {
                foreach (var action in from KeyCode vKey in System.Enum.GetValues(typeof (KeyCode)) where Input.GetKey(vKey) select KeyAssigner.KeyBinds.Single(x => x.KeyCode == vKey))
                {
                    switch (action.KeyAction)
                    {
                        case KeyAction.Jump:
                            player.Jump(JumpHeight);
                            break;
                        case KeyAction.MoveBackward:
                            player.MoveBackwards(MoveSpeed);
                            player.ApplyFrictionDamage(FrictionDamage);
                            break;
                        case KeyAction.MoveForward:
                            player.MoveForward(MoveSpeed);
                            player.ApplyFrictionDamage(FrictionDamage);
                            break;
                    }
                }
            }

        }
    }
}

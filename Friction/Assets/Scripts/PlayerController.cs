using System;
using System.Linq;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {

        public float MoveSpeed;
        public float JumpHeight;
        public float JumpCooldownTime;
        public float JumpCooldownCounter;
        public float FrictionDamage;
        public KeyAssigner KeyAssigner;
        public Text JumpCooldownStatus;
        public Text GameOver;
        private bool _applyFriction = true;

        // Use this for initialization
        void Start()
        {
            KeyAssigner = new KeyAssigner();
            KeyAssigner.AssignDefaultKeys();
            JumpCooldownStatus = GameObject.Find("JumpCooldown").GetComponent<Text>();
            GameOver = GameObject.Find("GameOver").GetComponent<Text>();
        }

        void OnGUI()
        {
        }

        // Update is called once per frame
        void Update()
        {
            var player = GetComponent<Rigidbody2D>();

            if (player.transform.localScale.y < 0.0f)
            {
                Destroy(player);
                GameOver.text = "Game Over";
            }



            if (!String.IsNullOrEmpty(JumpCooldownStatus.text))
            {
                if (Time.time < JumpCooldownCounter)
                {
                    var jumpCdLeft = JumpCooldownCounter - Time.time;
                    JumpCooldownStatus.text = "J" + (jumpCdLeft + 1f);
                }
                else
                {
                    JumpCooldownStatus.text = String.Empty;
                }
            }

            if (Input.anyKey)
            {
                var playerCollider = player.GetComponent<BoxCollider2D>();
                if (playerCollider != null)
                {
                    var grounds = GameObject.FindGameObjectsWithTag("Ground");

                    if (grounds.Any())
                    {
                        float? distance = null;
                        GameObject relevantGround = null;

                        foreach (var ground in grounds)
                        {
                            var currentDistance = Vector2.Distance(ground.transform.position, player.transform.position);
                            if (!distance.HasValue || currentDistance <= distance.Value)
                            {
                                distance = currentDistance;
                                relevantGround = ground;
                            }
                        }

                        if (relevantGround != null)
                        {
                            if (playerCollider.IsTouching(relevantGround.GetComponent<BoxCollider2D>()))
                            {
                                var groundMesh = relevantGround.gameObject.GetComponent<MeshRenderer>();
                                var playerMesh = player.gameObject.GetComponent<MeshRenderer>();
                                _applyFriction = groundMesh.material.color != playerMesh.material.color;
                            }
                        }
                    }
                }
                foreach (var action in from KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)) where Input.GetKey(vKey) select KeyAssigner.KeyBinds.Single(x => x.KeyCode == vKey))
                {
                    switch (action.KeyAction)
                    {
                        case KeyAction.Jump:
                            if (Time.time > JumpCooldownCounter)
                            {
                                JumpCooldownCounter = Time.time + JumpCooldownTime;
                                player.Jump(JumpHeight);
                                JumpCooldownStatus.text = "J" + (JumpCooldownTime + 1f);
                            }
                            break;
                        case KeyAction.MoveBackward:
                            player.MoveBackwards(MoveSpeed);
                            if (_applyFriction)
                            {
                                player.ApplyFrictionDamage(FrictionDamage);
                            }
                            break;
                        case KeyAction.MoveForward:
                            player.MoveForward(MoveSpeed);
                            if (_applyFriction)
                            {
                                player.ApplyFrictionDamage(FrictionDamage);
                            }
                            break;
                    }
                }
            }

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Pickup"))
            {
                var mesh = other.GetComponent<MeshRenderer>();
                var playerMesh = GetComponent<MeshRenderer>();
                playerMesh.material = mesh.material;
                other.gameObject.SetActive(false);
            }
        }

    }
}

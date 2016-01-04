using System;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class RigidbodyExtensions
    {
        public static bool MoveForward(this Rigidbody2D player, float movementSpeed)
        {
            try
            {
                player.velocity = new Vector2(movementSpeed, player.velocity.y);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool MoveBackwards(this Rigidbody2D player, float movementSpeed)
        {
            try
            {
                player.velocity = new Vector2(-movementSpeed, player.velocity.y);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Jump(this Rigidbody2D player, float jumpHeight)
        {
            try
            {
                if (player.IsGrounded())
                {
                    player.velocity = new Vector2(player.velocity.x, jumpHeight);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ApplyFrictionDamage(this Rigidbody2D player, float frictionDamage)
        {
            try
            {
                if (player.IsGrounded())
                {
                    player.transform.localScale -= new Vector3(0, frictionDamage, 0);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsGrounded(this Rigidbody2D player)
        {
            return Math.Abs(player.velocity.y) < 0.01f;
        }
    }
}

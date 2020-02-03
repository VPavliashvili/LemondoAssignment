using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1 {

    public static class Helper {

        public static void FixGravity(
            ref Vector2 movementDir, ref bool isFixedGravity, Transform transform, 
            Collider2D other, SpriteRenderer doorRenderer, Sprite openDoor, SpriteMask doorMask
        ) {
            movementDir = Vector2.right;
            ChangeGravity(0, Vector2.down, transform);
            isFixedGravity = true;

            Vector3 newScale = other.transform.localScale;
            newScale.x = -newScale.x;
            other.transform.localScale = newScale;

            doorRenderer.sprite = openDoor;
            doorMask.enabled = true;
        }

        public static IEnumerator SetRandomGravity(
            Transform transform, Rigidbody2D rb,
            float startingGravityScale, float waitTime
        ) {
            float startingScale = rb.gravityScale;
            rb.gravityScale = startingGravityScale;

            yield return new WaitForSeconds(waitTime);

            int chance = Random.Range(0, 3);
            Vector2 gravityDir = Vector2.zero;
            float angle = 0;

            switch (chance) {
                case 0:
                    gravityDir = Vector2.left;
                    angle = -90;
                    break;
                case 1:
                    gravityDir = Vector2.up;
                    angle = 180;
                    break;
                case 2:
                    gravityDir = Vector2.right;
                    angle = 90;
                    break;
                default:
                    throw new System.Exception("random value error");
            }

            rb.gravityScale = startingScale;
            ChangeGravity(angle, gravityDir, transform);
        }

        public static void ChangeGravity(float desiredZAngle, Vector2 dir, Transform transform) {
            transform.DORotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, desiredZAngle), 0.3f);
            Physics2D.gravity = dir * Utils.G;
        }

        public static void Move(
                ref bool facingLeft, BoolReference isJumping, ref bool isGrounded,
                float jumpForce, Rigidbody2D rb, Transform transform,
                Vector3 movePos, float horizontalInput
            ) {
            transform.localPosition += movePos;
            FlipFacing(ref facingLeft, transform, horizontalInput);
            Jump(isJumping, ref isGrounded, rb, jumpForce);
        }

        private static void FlipFacing(ref bool facingLeft, Transform transform, float inputValue) {
            if ((inputValue > 0 && facingLeft) || (inputValue < 0 && !facingLeft)) {
                facingLeft = !facingLeft;

                Vector3 newScale = transform.localScale;
                newScale.x = -newScale.x;
                transform.localScale = newScale;
            }
        }

        private static void Jump(BoolReference isJumping, ref bool isGrounded, Rigidbody2D rb, float jumpForce) {
            if (isJumping && isGrounded) {
                rb.AddForce(-Physics2D.gravity * jumpForce);

                isGrounded = false;
                isJumping.Value = false;
            }
        }


    }

}
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1 {

    public static class MovementHelper {

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
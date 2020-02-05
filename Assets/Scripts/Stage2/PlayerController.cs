using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage2 {

    public class PlayerController : GeneralPlayerController {

        [SerializeField]
        private BoolReference isInTransition;

        void Awake() {
            rb = GetComponent<Rigidbody2D>();
            movementDir = Vector2.right;
        }

        void FixedUpdate() {
            if (!isInTransition)
                Stage1.MovementHelper.ManageMovement(
                    ref facingLeft, isJumping, ref isGrounded,
                    jumpForce, rb, transform, MovePos, horizontalInput
                );
            else
                Stage1.MovementHelper.ManageMovement(
                    ref facingLeft, isJumping, ref isGrounded,
                    jumpForce, rb, transform, MovePos, 1
                );

        }

        void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.name == "Bottom") {
                isGrounded = true;
            }

            if (other.gameObject.tag == "Box") {
                isGrounded = true;
            }

        }

    }

}
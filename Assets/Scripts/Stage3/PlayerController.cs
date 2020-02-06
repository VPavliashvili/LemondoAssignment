using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage3 {

    public class PlayerController : GeneralPlayerController {
        
        void Start() {

        }

        new void Update() {
            base.Update();
        }

        void FixedUpdate() {
            Stage1.MovementHelper.ManageMovement(
                ref facingLeft, isJumping, ref isGrounded,
                jumpForce, rb, transform, MovePos, horizontalInput
            );
        }

    }

}
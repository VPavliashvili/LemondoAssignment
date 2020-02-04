using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AnimatorHelper {

    public static Animator animator;
    
    public static void Jump() {
        animator.SetTrigger("Jump");
    }

    public static void Walk(bool isWalking) {
        animator.SetBool("IsWalking", isWalking);
    }

    public static void Move(float inputVal) {
        if (inputVal != 0)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);
    }

}
using UnityEngine;

public static class AnimatorHelper {

    public static Animator characterAnimator;
    
    public static void Jump() {
        characterAnimator.SetTrigger("Jump");
    }

    public static void Walk(bool isWalking) {
        characterAnimator.SetBool("IsWalking", isWalking);
    }

    public static void Move(float inputVal) {
        if (inputVal != 0)
            characterAnimator.SetBool("IsMoving", true);
        else
            characterAnimator.SetBool("IsMoving", false);
    }

    public static void Die() => AnimatorHelper.characterAnimator.SetBool("IsDead", true);
    public static void Respawn() => AnimatorHelper.characterAnimator.SetBool("IsDead", false);

}
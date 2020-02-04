using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationsController : MonoBehaviour {

    [SerializeField]
    private BoolReference isJumping;

    private Animator animator;
    
    void Awake() {
        animator = GetComponent<Animator>();
        AnimatorHelper.animator = animator;
    }

    void Start() {

    }

    void Update() {
        
    }

    public void GetItem() {
        animator.SetBool("ItemPicked", true);
    }

    public void ThrowItem() {
        animator.SetBool("ItemPicked", false);
    }

    public void EndClimbing() {

    }

    public void ClimbForward() {

    }

    public void ClimbUp() {

    }

}
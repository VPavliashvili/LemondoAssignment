using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationsController : MonoBehaviour {

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();    
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
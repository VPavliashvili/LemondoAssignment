using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour {

    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();    
    }

    void Start() {

    }

    void Update() {

    }

    public void GetItem() {
        Debug.Log("Item has been got");
        //itemPicked = true;
    }

    public void ThrowItem() {
        Debug.Log("Item has been thrown");
    }

    public void EndClimbing() {

    }

    public void ClimbForward() {

    }

    public void ClimbUp() {

    }

}
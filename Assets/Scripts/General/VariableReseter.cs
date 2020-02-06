using UnityEngine;

public class VariableReseter : MonoBehaviour {

    [SerializeField]
    private BoolReference isDeadByBall;
    [SerializeField]
    private BoolReference isInTransition;
    [SerializeField]
    private BoolReference isJumping;
    [SerializeField]
    private BoolReference isWalking;
    [SerializeField]
    private BoolReference isSpecialGravity;
    [SerializeField]
    private BoolReference instructionsSeen;
    [SerializeField]
    private BoolReference hasWon;

    void Awake() {
        isDeadByBall.Value = false;
        isInTransition.Value = false;
        isJumping.Value = false;
        isWalking.Value = false;
        isSpecialGravity.Value = false;
        hasWon.Value = false;
    }

    void OnApplicationQuit() {
        instructionsSeen.Value = false;    
    }

}
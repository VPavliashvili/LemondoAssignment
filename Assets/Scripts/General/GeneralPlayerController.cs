using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GeneralPlayerController : MonoBehaviour {

    [SerializeField]
    protected BoolReference isJumping;
    [SerializeField]
    protected BoolReference isWalking;
    protected Rigidbody2D rb;
    
    [SerializeField]
    [Range(5, 50)]
    protected float movementSpeed;
    [SerializeField]
    [Range(2, 5)]
    protected float walkingReduction;
    [SerializeField]
    [Range(50, 200)]
    protected float jumpForce;

    protected Vector2 movementDir;

    protected float horizontalInput;
    protected bool facingLeft;
    protected bool isGrounded;

    protected float Speed => (isWalking ? movementSpeed / walkingReduction : movementSpeed) * Time.fixedDeltaTime;
    protected Vector3 MovePos => (Vector3)movementDir * Speed * horizontalInput;


    protected void Update() {
        horizontalInput = Input.GetAxis("Horizontal");

        Stage1.MovementHelper.SetInputButtonBool("Jump", isJumping);
        Stage1.MovementHelper.SetInputButtonBool("Walk", isWalking, AnimatorHelper.animator, "IsWalking");

    }

}
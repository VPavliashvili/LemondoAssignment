using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1 {

    public class PlayerController : MonoBehaviour {

        #region Variables & Properties

        [SerializeField]
        private Transform character;
        [SerializeField]
        private Sprite openDoor;
        [SerializeField]
        private SpriteRenderer doorRenderer;
        [SerializeField]
        private SpriteMask doorMask;
        [SerializeField]
        private BoolReference isJumping;
        [SerializeField]
        private BoolReference isWalking;
        [SerializeField]
        private TrapsDeployer trapsDeployer;

        private Rigidbody2D rb;
        private BoxCollider2D boxCollider;
        private Vector2 movementDir;
        private Vector3 startingPos;

        [SerializeField]
        [Range(5, 50)]
        private float movementSpeed;
        [SerializeField]
        [Range(2, 5)]
        private float walkingReduction;
        [SerializeField]
        [Range(50, 200)]
        private float jumpForce;
        [SerializeField]
        [Range(0.1f, 1)]
        private float startingGravityScale;
        [SerializeField]
        [Range(0.1f, 1)]
        private float waitTimeBeforeGravityFlip;

        private float horizontalInput;
        private bool facingLeft;
        private bool isGrounded;
        private bool isFixedGravity;

        private float Speed => (isWalking ? movementSpeed / walkingReduction : movementSpeed) * Time.fixedDeltaTime;
        private Vector3 MovePos => (Vector3)movementDir * Speed * horizontalInput;

        #endregion

        void Awake() {
            rb = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            movementDir = Vector2.right;
            character.parent = transform;
        }

        void Start() {
            StartCoroutine(GravityHelper.SetRandomGravity(transform, rb, startingGravityScale, waitTimeBeforeGravityFlip));
        }

        void Update() {
            horizontalInput = Input.GetAxis("Horizontal");

            MovementHelper.SetInputButtonBool("Jump", isJumping);
            MovementHelper.SetInputButtonBool("Walk", isWalking, AnimatorHelper.animator, "IsWalking");
            
        }

        void FixedUpdate() {
            MovementHelper.Move(
                ref facingLeft, isJumping, ref isGrounded, 
                jumpForce, rb, transform, MovePos, horizontalInput
            );
        }

        void OnCollisionEnter2D(Collision2D other) {
            if (!isFixedGravity && other.gameObject.tag == "Wall") {

                if (other.gameObject.name != "Bottom") {
                    WallInfo wall = other.gameObject.GetComponent<WallInfo>();
                    movementDir = wall.movementDir;
                    GravityHelper.ChangeGravity(wall.gravityFlipRotation, wall.gravityDir, transform);
                }

                isGrounded = true;
            }
            if(other.gameObject.name == "Bottom") {
                isGrounded = true;
            }

        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Trap" && !isFixedGravity) {
                boxCollider.isTrigger = true;
            }
            else if(other.name == "Finish") {
                if (isFixedGravity)
                    CustomEvents.instance.RaiseOnLevelpass();
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if(other.tag == "Trap" && !isFixedGravity) {
                CustomEvents.instance.RaiseOnGameOver(
                    StartCoroutine, Destroy, trapsDeployer.Traps,
                    trapsDeployer.walls, trapsDeployer.minTrapCountOnWall, trapsDeployer.maxTrapCountOnWall,
                    Instantiate, trapsDeployer.trapPrefab, trapsDeployer.trapsContainer, startingPos, rb,
                    startingGravityScale, waitTimeBeforeGravityFlip
                );
            }
        }

        void OnTriggerStay2D(Collider2D other) {
            if (other.name == "Switch") {
                if (Input.GetButtonDown("Action") && !isFixedGravity) {
                    GravityHelper.FixGravity(
                        ref movementDir, ref isFixedGravity, transform, other, doorRenderer, openDoor, doorMask
                    );
                }
            }
        }
        
    }
}
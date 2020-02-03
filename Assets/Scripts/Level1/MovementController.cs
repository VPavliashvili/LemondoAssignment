using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1 {

    public class MovementController : MonoBehaviour {

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

        private Rigidbody2D rb;
        private BoxCollider2D boxCollider;
        private Vector2 movementDir;

        [SerializeField]
        [Range(5, 50)]
        private float movementSpeed;
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

        private float Speed => movementSpeed * Time.fixedDeltaTime;
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

            if (Input.GetButtonDown("Jump")) {
                isJumping.Value = true;
            }
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
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Trap") {
                boxCollider.isTrigger = true;
            }
            else if(other.name == "Finish") {
                Debug.Log("Level Passed");
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if(other.tag == "Trap") {
                //respawn after 1 sec
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage1 {

    public class PlayerController : GeneralPlayerController {

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
        private TrapsDeployer trapsDeployer;
        [SerializeField]
        private StageCompleter stageCompleter;
        [SerializeField]
        private GameObject nextStagePrefab;

        private BoxCollider2D boxCollider;
        private WallInfo rightWall;
        private Vector3 startingPos;

        [SerializeField]
        [Range(0.1f, 1)]
        private float startingGravityScale;
        [SerializeField]
        [Range(0.1f, 1)]
        private float waitTimeBeforeGravityFlip;

        private bool isFixedGravity;

        #endregion

        void Awake() {
            rb = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            movementDir = Vector2.right;
            character.parent = transform;
            rightWall = trapsDeployer.walls[1];
        }

        void Start() {
            StartCoroutine(GravityHelper.SetRandomGravity(transform, rb, startingGravityScale, waitTimeBeforeGravityFlip));
        }

        void FixedUpdate() {
            MovementHelper.ManageMovement(
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

            if (other.gameObject.name == "Bottom") {
                isGrounded = true;
            }

        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Trap" && !isFixedGravity) {
                boxCollider.isTrigger = true;
            }
            else if(other.name == "Finish") {
                if (isFixedGravity)
                    CustomEvents.instance.RaiseOnStagePassed(
                        stageCompleter, rightWall, 
                        Instantiate, nextStagePrefab, getNextPos()
                    );
            }
            Vector3 getNextPos() {
                Vector3 res = Vector3.zero;

                Transform background = GameObject.Find("Background").transform;

                res.y = background.position.y;

                Vector3 extents = Utils.GetBoundsWithRenderer(background.GetComponent<SpriteRenderer>()).extents;
                res.x = extents.x * 2;
                return res;
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if(other.tag == "Trap" && !isFixedGravity) {
                CustomEvents.instance.RaiseOnGameOver(
                    StartCoroutine, Destroy, trapsDeployer.Traps,
                    trapsDeployer.walls, trapsDeployer.minTrapCountOnWall, trapsDeployer.maxTrapCountOnWall,
                    Instantiate, trapsDeployer.trapPrefab, startingPos, rb,
                    startingGravityScale, waitTimeBeforeGravityFlip
                );
            }
        }

        void OnTriggerStay2D(Collider2D other) {
            if (other.name == "Switch") {
                if (Input.GetButtonDown("Action") && !isFixedGravity) {
                    GravityHelper.FixGravity(
                        ref movementDir, ref isFixedGravity, transform, other, doorRenderer, openDoor, doorMask, rightWall
                    );
                }
            }
        }
        
    }
}
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Stage2 {

    public class PlayerController : GeneralPlayerController {

        [HideInInspector]
        public SpriteRenderer playerEyeRenderer;

        [SerializeField]
        private BoolReference isInTransition;
        [SerializeField]
        private Sprite[] eyeSprites;
        [SerializeField]
        private BoolReference isDeadByBall;
        [SerializeField]
        private GameObject Stage3Prefab;
        private Rigidbody2D killerBallRb;

        private Transform startingParent;
        private Transform dragableBox;
        private Sprite OpenedEye;

        [HideInInspector]
        public Vector3 startingPos;
        private Vector3 boxStartingPos;

        private bool needToFlip;
        private bool isDragging;
        private bool callOnce;
        private bool isStaying;
        private bool isStagePassed;

        void Awake() {
            rb = GetComponent<Rigidbody2D>();
            movementDir = Vector2.right;
        }

        new void Update() {
            if (!isStagePassed)
                base.Update();
            else {
                AnimatorHelper.characterAnimator.SetBool("IsMoving", true);
                AnimatorHelper.characterAnimator.SetBool("IsWalking", true);
            }

            if (Input.GetButtonDown("Respawn") && isDeadByBall) {
                isDeadByBall.Value = false;
                StartCoroutine(Respawn(killerBallRb));
            }

            if (isStaying) {
                if (Input.GetButtonDown("Action")) {
                    isDragging = !isDragging;

                    //dragableBox.GetComponent<SpriteRenderer>().color = isDragging ? Color.green : Color.red;
                    if (isDragging)
                        AnimatorHelper.characterAnimator.SetTrigger("DragActionStarted");
                    else {
                        AnimatorHelper.characterAnimator.SetTrigger("DragActionEnded");
                        AnimatorHelper.characterAnimator.SetBool("IsDraggingAhead", false);
                        AnimatorHelper.characterAnimator.SetBool("IsDraggingBack", false);
                        isWalking.Value = false;
                    }
                    //dragableBox.transform.parent = isDragging ? transform : startingParent;
                    //rb.bodyType = isDragging ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
                   

                }
            }
            
        }

        void FixedUpdate() {
            if (!isStagePassed) {
                if (!isDeadByBall && !isDragging) {
                    if (!isInTransition)
                        Stage1.MovementHelper.ManageMovement(
                            ref facingLeft, isJumping, ref isGrounded,
                            jumpForce, rb, transform, MovePos, horizontalInput
                        );
                    else
                        Stage1.MovementHelper.ManageMovement(
                            ref facingLeft, isJumping, ref isGrounded,
                            jumpForce, rb, transform, MovePos, 1
                        );
                }
                else if (isDragging) {
                    if (horizontalInput != 0) {
                        isWalking.Value = true;
                        transform.localPosition += MovePos;
                        dragableBox.localPosition += MovePos;

                        AnimatorHelper.characterAnimator.SetBool("IsDraggingAhead", horizontalInput > 0);
                        AnimatorHelper.characterAnimator.SetBool("IsDraggingBack", horizontalInput < 0);
                    }
                }
            }
        }

        void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.name == "Bottom" || other.gameObject.tag == "Box") {
                isGrounded = true;
            }
            else if(other.gameObject.name == "KillerBall" && !isDeadByBall) {
                killerBallRb = other.gameObject.GetComponent<Rigidbody2D>();
                Die(other.transform);
                isDeadByBall.Value = true;
                killerBallRb.bodyType = RigidbodyType2D.Static;
            }

        }

        void OnTriggerEnter2D(Collider2D other) {
            if(other.name == "DragTrigger") {
                if (!callOnce) {
                    callOnce = true;
                    startingParent = other.transform.parent.parent;
                    dragableBox = other.transform.parent;
                    boxStartingPos = dragableBox.localPosition;
                }

                isStaying = true;

            }
            if(other.name == "Exit") {
                Transform bg = transform.parent.GetChild(1);
                float xIncrement = Utils.GetBoundsWithRenderer(bg.GetComponent<SpriteRenderer>()).extents.x;

                Vector3 pos = bg.position;

                pos.x += (2 * xIncrement) + 5;
                pos.y = 0;
                GameObject stage3Holder = Instantiate(Stage3Prefab, pos, Quaternion.identity);

                StartCoroutine(StageTransition(stage3Holder.transform.GetFirstChild().GetFirstChild(), 0, pos.x, Camera.main, 2.5f));
            }

            IEnumerator StageTransition(Transform characterHolder, float characterEndPosX, float cameraEndPosX, Camera camera, float time = 2.3f) {
                isStagePassed = true;

                Transform player = transform.GetFirstChild();

                player.parent = characterHolder;
                transform.localPosition = Vector3.zero;

                camera.transform.DOMoveX(cameraEndPosX, time);

                yield return new WaitForEndOfFrame();

                player.DOLocalMoveX(0, time);

                Transform leftWall = characterHolder.parent.GetChild(2).Find("LeftWall");
                leftWall.GetComponent<BoxCollider2D>().enabled = true;

                characterHolder.GetComponent<Stage3.PlayerController>().playerEyeRenderer = playerEyeRenderer;

                yield return new WaitForSeconds(time);
                Destroy(transform.parent.parent.gameObject);

            }

        }

        void OnTriggerExit2D(Collider2D other) {
            if (other.name == "DragTrigger") {
                isStaying = false;
            } 
        }

        void OnDestroy() {
            AnimatorHelper.characterAnimator.SetBool("IsMoving", false);
            AnimatorHelper.characterAnimator.SetBool("IsWalking", false);
        }

        private void Die(Transform ball) {
            if (ball.localPosition.x < transform.position.x) {
                transform.DORotate(new Vector3(0, 0, -90), 1);
            }
            else {
                transform.DORotate(new Vector3(0, 0, 90), 1);
            }
            StartCoroutine(animateEye());
            
            IEnumerator animateEye(float time = 40f) {

                OpenedEye = playerEyeRenderer.sprite;

                float spriteTime = time / eyeSprites.Length;
                spriteTime /= 100;//getting 0.08 seconds to achieve 0.4 second overall;

                foreach (Sprite eye in eyeSprites) {

                    playerEyeRenderer.sprite = eye;
                    yield return new WaitForSeconds(spriteTime);
                    
                }

            }

            needToFlip = transform.localScale.x < 0;

            AnimatorHelper.characterAnimator.SetBool("IsDead", true);

        }

        private IEnumerator Respawn(Rigidbody2D rb) {
            if (needToFlip) {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                facingLeft = !facingLeft;
            }

            AnimatorHelper.characterAnimator.SetBool("IsDead", false);

            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = new Vector3(-28, -12);
            //transform.localPosition = startingPos;
            playerEyeRenderer.sprite = OpenedEye;
            rb.bodyType = RigidbodyType2D.Dynamic;

            killerBallRb.transform.localPosition = killerBallRb.GetComponent<KillerBallController>().startingPos;
            killerBallRb.GetComponent<KillerBallController>().StartMoving();
            if (dragableBox != null)
                dragableBox.localPosition = boxStartingPos;
            
            AnimatorHelper.characterAnimator.SetBool("IsMoving", true);
            transform.DOLocalMoveX(-22, 1.5f);
            yield return new WaitForSeconds(1.5f);
            AnimatorHelper.characterAnimator.SetBool("IsMoving", false);
        }

    }

}
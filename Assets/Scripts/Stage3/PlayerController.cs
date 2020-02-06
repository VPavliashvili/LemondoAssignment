using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Stage3 {

    public class PlayerController : GeneralPlayerController {

        [HideInInspector]
        public SpriteRenderer playerEyeRenderer;

        [SerializeField]
        private BoolReference isSpecialGravity;
        [SerializeField]
        private Transform boss;
        [SerializeField]
        private Transform throwableContainer;
        [SerializeField]
        private Sprite[] eyeSprites;

        private GameObject throwAble;
        private Sprite OpenedEye;

        private bool hasPicked;
        [SerializeField]
        private bool isDead;


        void Awake() {
            isGrounded = true;
            rb = GetComponent<Rigidbody2D>();
            movementDir = Vector2.right;
        }

        new void Update() {

            if (!isDead) {
                base.Update();

                if (throwAble != null && Input.GetButtonDown("Action") && !hasPicked) {
                    throwAble.transform.parent = gameObject.transform;
                    hasPicked = true;
                    AnimatorHelper.characterAnimator.SetTrigger("Take");
                }
                else if (hasPicked) {
                    if (Input.GetButtonDown("Action")) {
                        Throw(isSpecialGravity);
                        hasPicked = false;
                        throwAble.transform.parent = throwableContainer;
                        throwAble = null;
                        AnimatorHelper.characterAnimator.SetTrigger("Throw");
                    }
                }
            }
            else {
                if (Input.GetButtonDown("Respawn")) {
                    StartCoroutine(Respawn());
                }
            }
        }

        void FixedUpdate() {
            if (!isDead) {
                Stage1.MovementHelper.ManageMovement(
                    ref facingLeft, isJumping, ref isGrounded,
                    jumpForce, rb, transform, MovePos, horizontalInput
                );
            }
        }

        void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.name == "Bottom") {
                isGrounded = true;
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.name == "GravitationTrigger") {
                isSpecialGravity.Value = true;
            }
            if(other.tag == "Throwable") {
                if (!hasPicked)
                    throwAble = other.gameObject;
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if(other.name == "GravitationTrigger") {
                isSpecialGravity.Value = false;
            }
            if (other.tag == "Throwable") {
                if (!hasPicked)
                    throwAble = null;
            }
            else if(other.tag == "ShootingObject") {
                if (!isDead) {
                    ManageDeath();
                    if (hasPicked) {
                        throwAble.transform.parent = throwableContainer;
                        throwAble = null;
                    }
                }
                
            }
        }

        private void ManageDeath() {
            isDead = true;
            AnimatorHelper.Die();
            transform.DORotate(new Vector3(0, 0, -90), 1);
            StartCoroutine(animateEye());
        }

        private IEnumerator Respawn() {

            isDead = false;
            AnimatorHelper.Respawn();
            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = new Vector3(-28, -12);
            //transform.localPosition = startingPos;
            playerEyeRenderer.sprite = OpenedEye;

            if (transform.localScale.z < 0) {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                facingLeft = !facingLeft;
            }

            AnimatorHelper.characterAnimator.SetBool("IsMoving", true);
            transform.DOLocalMoveX(-20, 1.5f);
            yield return new WaitForSeconds(1.5f);
            AnimatorHelper.characterAnimator.SetBool("IsMoving", false);

        }

        private IEnumerator animateEye(float time = 40f) {

            OpenedEye = playerEyeRenderer.sprite;

            float spriteTime = time / eyeSprites.Length;
            spriteTime /= 100;//getting 0.08 seconds to achieve 0.4 second overall;

            foreach (Sprite eye in eyeSprites) {

                playerEyeRenderer.sprite = eye;
                yield return new WaitForSeconds(spriteTime);

            }

        }

        private void Throw(bool isSpecialGravity) {
            if (isSpecialGravity) {
                int chance = Random.Range(0, 100);
                if (chance < 90)
                    throwAble.transform.DOMove(boss.position, 1.25f);
                else {
                    Vector3 pos = new Vector3(boss.position.x + Random.Range(-10, 10), boss.position.y, boss.position.z);
                    throwAble.transform.DOMove(pos, 1.25f);
                }
                Destroy(throwAble, 1.3f);
            }
        }

    }

}
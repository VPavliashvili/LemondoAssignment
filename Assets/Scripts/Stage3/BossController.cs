using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stage3 {

    public class BossController : MonoBehaviour {

        [SerializeField]
        private Transform player;
        [SerializeField]
        private GameObject ShootingObject;
        [SerializeField]
        private BoolReference hasWon;
        private Animator animator;

        private bool isShooting;
        private bool isDead;
        private float counter;
        [SerializeField]
        [Range(0.5f, 3)]
        private float shootingRate;

        private Vector3 ShootDirection => player.position - transform.position;
        private float Angle => (Mathf.Atan2(ShootDirection.y, ShootDirection.x) * Mathf.Rad2Deg) + 90;

        void Awake() {
            animator = GetComponent<Animator>();
        }

        IEnumerator Start() {
            yield return new WaitForSeconds(2);
            isShooting = true;
        }

        void Update() {

            if (isDead && Input.GetButtonDown("Respawn")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        void FixedUpdate() {
            if (!isDead) {

                if (isShooting) {
                    LookAtPlayer();
                }

                ManageShooting();
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Throwable") {
                isDead = true;
                animator.SetTrigger("Die");
                animator.SetBool("IsDead", true);
                hasWon.Value = true;
            }
        }

        private void LookAtPlayer() {
            transform.rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
        } 

        private void ManageShooting() {
            if (counter < shootingRate) {
                counter += Time.fixedDeltaTime;
            }
            else {
                Shoot();
                counter = 0;
            }
        }

        private void Shoot() {
            GameObject obj = Instantiate(
                ShootingObject, 
                transform.position + new Vector3(0, -3),
                Quaternion.AngleAxis(Angle, Vector3.forward
            ));

            Vector3 direction = transform.rotation * Vector2.down;

            obj.GetComponent<ShootingObjectController>().direction = direction;
        }

    }

}
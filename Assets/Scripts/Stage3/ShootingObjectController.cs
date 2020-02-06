using UnityEngine;

namespace Stage3 {

    public class ShootingObjectController : MonoBehaviour {

        private Rigidbody2D rb;

        [HideInInspector]
        public Vector3 direction;

        [SerializeField]
        [Range(20, 100)]
        private float speed;

        void Awake() {
            rb = GetComponent<Rigidbody2D>();    
        }

        void Start() {
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.name != "Boss")
                Destroy(gameObject);
        }

    }

}
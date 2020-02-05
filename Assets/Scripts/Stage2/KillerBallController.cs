using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage2 {

    public class KillerBallController : MonoBehaviour {

        private Rigidbody2D rb;
        [HideInInspector]
        public Vector3 startingPos;

        void Awake() {
            startingPos = transform.localPosition;    
        }

        void Start() {
            StartMoving();
        }

        public void StartMoving(float speed = 5000) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
        }

    }

}
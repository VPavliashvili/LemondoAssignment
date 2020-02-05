using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage2 {

    public class KillerBallController : MonoBehaviour {

        void Start() {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5000);
        }

    }

}
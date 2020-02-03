using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1 {

    public class TrapsDeployer : MonoBehaviour {

        [SerializeField]
        private GameObject trapPrefab;
        [SerializeField]
        [Range(1, 3)]
        private int minTrapCountOnWall;
        [SerializeField]
        [Range(3, 5)]
        private int maxTrapCountOnWall;

        void Start() {

        }

        void Update() {

        }

    }

}
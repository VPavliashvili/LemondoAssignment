using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1 {

    public class TrapsDeployer : MonoBehaviour {

        [HideInInspector]
        public List<GameObject> Traps;
        public WallInfo[] walls;

        [SerializeField]
        public GameObject trapPrefab;
        [SerializeField]
        public Transform trapsContainer;
        [SerializeField]
        [Range(1, 3)]
        public int minTrapCountOnWall;
        [SerializeField]
        [Range(3, 5)]
        public int maxTrapCountOnWall;

        void Awake() {
            Traps = new List<GameObject>();
        }

        void Start() {
            TrapsHelper.DeployTraps(
                walls, minTrapCountOnWall, maxTrapCountOnWall,
                Instantiate, trapPrefab, trapsContainer, Traps
            );

        }

    }

}
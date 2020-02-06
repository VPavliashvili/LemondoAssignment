using System.Collections.Generic;
using UnityEngine;

namespace Stage1 {

    public class TrapsDeployer : MonoBehaviour {

        [HideInInspector]
        public List<GameObject> Traps;
        public WallInfo[] walls;

        [SerializeField]
        public GameObject trapPrefab;
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
                Instantiate, trapPrefab, Traps
            );

        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1 {

    public class TrapsDeployer : MonoBehaviour {

        [HideInInspector]
        public List<GameObject> Traps;

        [SerializeField]
        private GameObject trapPrefab;
        [SerializeField]
        private Transform trapsContainer;
        [SerializeField]
        [Range(1, 3)]
        private int minTrapCountOnWall;
        [SerializeField]
        [Range(3, 5)]
        private int maxTrapCountOnWall;
        [SerializeField]
        private WallInfo[] walls;

        void Awake() {
            Traps = new List<GameObject>();
        }

        void Start() {
            foreach (WallInfo wall in walls) {

                float rotation = wall.gravityFlipRotation;
                int number = Random.Range(minTrapCountOnWall, maxTrapCountOnWall);
                Transform field = wall.TrapsField;

                Bounds bounds = Utils.GetBoundsWithRenderer(field.GetComponent<SpriteRenderer>());

                Vector2[] positionsToDeploy = wall.GetTrappositions(number, bounds, Utils.GetBoundsWithRenderer(trapPrefab.GetComponent<SpriteRenderer>()).size);

                for (int i = 0; i < number; i++) {
                    GameObject trap = Instantiate(trapPrefab, positionsToDeploy[i], Quaternion.identity);
                    trap.transform.parent = trapsContainer;
                    trap.transform.localEulerAngles = new Vector3(0, 0, rotation);
                    Traps.Add(trap);
                }

            }
        }

    }

}
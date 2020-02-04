using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level1 {

    public static class TrapsHelper {

        public static void DeployTraps(
            WallInfo[] walls, int min, int max,
            System.Func<GameObject,Vector3,Quaternion,GameObject> instantiate, 
            GameObject trapPrefab, Transform trapsContainer, List<GameObject> traps
        ) {
            foreach (WallInfo wall in walls) {

                float rotation = wall.gravityFlipRotation;
                int number = Random.Range(min, max);
                Transform field = wall.TrapsField;

                Bounds bounds = Utils.GetBoundsWithRenderer(field.GetComponent<SpriteRenderer>());
                Vector3 trapSize = Utils.GetBoundsWithRenderer(trapPrefab.GetComponent<SpriteRenderer>()).size;

                Vector2[] positionsToDeploy = wall.GetTrappositions(number, bounds, trapSize);

                for (int i = 0; i < number; i++) {
                    GameObject trap = instantiate(trapPrefab, positionsToDeploy[i], Quaternion.identity);
                    trap.transform.parent = trapsContainer;
                    trap.transform.localEulerAngles = new Vector3(0, 0, rotation);
                    traps.Add(trap);
                }

            }
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Stage1 {

    public static class GameOverHelper {

        public static void OnGameOver(
            Func<IEnumerator, Coroutine> startCoroutine,
            Action<GameObject> destroy,
            List<GameObject> traps,
            WallInfo[] walls, int min, int max,
            Func<GameObject, Vector3, Quaternion, GameObject> instantiate,
            GameObject trapPf,
            Vector3 position, Rigidbody2D rb,
            float startGravityScale, float waitTime
        ) {

            startCoroutine(OnGameOverRoutine(
                traps, destroy, walls, min, max, instantiate, trapPf,
                 position, rb, startGravityScale, waitTime, startCoroutine
            ));
            

        }

        private static void ClearTraps(List<GameObject> traps, Action<GameObject> destroy) {
            foreach (GameObject trap in new List<GameObject>(traps)) {
                traps.Remove(trap);
                destroy(trap);
            }
        }

        private static void Respawn(Vector3 position, Rigidbody2D rb) {
            rb.velocity = Vector2.zero;
            rb.transform.localPosition = position;
            BoxCollider2D collider = rb.GetComponent<BoxCollider2D>();
            collider.isTrigger = false;
        }

        private static IEnumerator OnGameOverRoutine(
            List<GameObject> traps, 
            Action<GameObject> destroy,
            WallInfo[] walls, int min, int max,
            Func<GameObject, Vector3, Quaternion, GameObject> instantiate,
            GameObject trapPf,
            Vector3 position, Rigidbody2D rb,
            float startGravityScale, float waitTime,
            Func<IEnumerator, Coroutine> startCoroutine
        ) {

            ClearTraps(traps, destroy);
            TrapsHelper.DeployTraps(walls, min, max, instantiate, trapPf, traps);

            startCoroutine(GravityHelper.SetRandomGravity(rb.transform, rb, startGravityScale, waitTime));

            yield return new WaitForSeconds(waitTime + 0.25f);
            Respawn(position, rb);

        }

    }

}

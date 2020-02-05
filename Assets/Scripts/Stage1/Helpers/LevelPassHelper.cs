using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage1 {
    public static class StagePassHelper {
        
        public static void OnStagePassed(
            StageCompleter stageCompleter, WallInfo rightWall,
            System.Func<GameObject,Vector3,Quaternion,GameObject> instantiate,
            GameObject nextStagePrefab, Vector3 onPosition
        ) {
            stageCompleter.enabled = true;
            rightWall.gameObject.GetComponent<Collider2D>().isTrigger = true;
            SpawnNextStage(instantiate, nextStagePrefab, onPosition, stageCompleter);
        }

        private static void SpawnNextStage(
            System.Func<GameObject, Vector3, Quaternion, GameObject> instantiate,
             GameObject nextStagePrefab, Vector3 onPosition, StageCompleter stageCompleter
        ) {
            GameObject nextStage = instantiate(nextStagePrefab, onPosition, Quaternion.identity);
            stageCompleter.playerController = nextStage.GetFirstChild().GetFirstChild().GetComponent<Stage2.PlayerController>();
        }

    }
}

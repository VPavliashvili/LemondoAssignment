using UnityEngine;

namespace Stage1 {
    public static class StagePassHelper {
        
        public static void OnStagePassed(
            Stage1.StageCompleter stageCompleter, WallInfo rightWall,
            System.Func<GameObject,Vector3,Quaternion,GameObject> instantiate,
            GameObject nextStagePrefab, Vector3 onPosition
        ) {
            stageCompleter.enabled = true;
            rightWall.gameObject.GetComponent<Collider2D>().isTrigger = true;
            SpawnNextStage(instantiate, nextStagePrefab, onPosition, stageCompleter);
        }

        private static void SpawnNextStage(
            System.Func<GameObject, Vector3, Quaternion, GameObject> instantiate,
             GameObject nextStagePrefab, Vector3 onPosition, Stage1.StageCompleter stageCompleter
        ) {
            GameObject nextStage = instantiate(nextStagePrefab, onPosition, Quaternion.identity);
            stageCompleter.Stage2PlayerController = nextStage.GetFirstChild().GetFirstChild().GetComponent<Stage2.PlayerController>();
        }

    }
}

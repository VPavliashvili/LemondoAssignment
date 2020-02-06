using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Stage1 {

    public class StageCompleter : MonoBehaviour {

        [HideInInspector]
        public Stage2.PlayerController Stage2PlayerController;
        [SerializeField]
        public BoolReference isIntransition;
        [SerializeField]
        private SpriteRenderer eyeRenderer;

        private GameObject stage1;
        private PlayerController Stage1Controller;

        public float nextCameraPosX = 51.7f;

        [SerializeField]
        [Range(1, 5)]
        private float transitionTime;

        private Vector3 CameraEndPos {
            get {
                Vector3 bgPos = Stage2PlayerController.transform.parent.parent.localPosition;
                return new Vector3(bgPos.x, 0, Camera.main.transform.localPosition.z);
            }
        }

        void OnEnable() {
            Stage1Controller = transform.parent.GetComponent<PlayerController>();
            stage1 = transform.parent.parent.gameObject;



            StartCoroutine(LevelTransition(0, nextCameraPosX, Camera.main, transitionTime));
        }

        private IEnumerator LevelTransition(float characterEndPosX, float cameraEndPosX, Camera camera, float time) {
            isIntransition.Value = true;
            transform.parent = null;
            Destroy(Stage1Controller);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            AnimatorHelper.characterAnimator.SetBool("IsMoving", true);

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();



            camera.transform.DOMoveX(cameraEndPosX, time, false);
            //gameObject.transform.DOMove(characterEndPos, time, true);
            transform.parent = Stage2PlayerController.transform;
            Stage2PlayerController.playerEyeRenderer = eyeRenderer;

            yield return new WaitForEndOfFrame();

            transform.DOLocalMoveX(characterEndPosX, time);

            yield return new WaitForSeconds(time - 1);
            //AnimatorHelper.animator.SetBool("IsMoving", false);
            yield return new WaitForSeconds(1);
            Destroy(stage1);
            isIntransition.Value = false;
            GameObject.Find("LeftWall").GetComponent<BoxCollider2D>().enabled = true;

            Stage2PlayerController.startingPos = transform.position;

            enabled = false;
        }

    }
}
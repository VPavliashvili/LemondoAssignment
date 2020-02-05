using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageCompleter : MonoBehaviour {
    
    [HideInInspector]
    public Stage2.PlayerController playerController;
    [SerializeField]
    public BoolReference isIntransition;
    [SerializeField]
    private GameObject leftWall;

    private GameObject stage1;
    private Stage1.PlayerController Stage1Controller;

    [SerializeField][Range(1,5)]
    private float transitionTime; 

    private Vector3 CameraEndPos {
        get {
            Vector3 bgPos = playerController.transform.parent.parent.localPosition;
            return new Vector3(bgPos.x, 0, Camera.main.transform.localPosition.z);
        }
    }

    void OnEnable() {
        Stage1Controller = transform.parent.GetComponent<Stage1.PlayerController>();
        stage1 = transform.parent.parent.gameObject;

        
        
        StartCoroutine(LevelTransition(0, 51.7f, Camera.main, transitionTime));
    }

    private IEnumerator LevelTransition(float characterEndPosX, float cameraEndPosX, Camera camera, float time) {
        isIntransition.Value = true;
        transform.parent = null;
        Destroy(Stage1Controller);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        AnimatorHelper.animator.SetBool("IsMoving", true);
        Debug.Log(AnimatorHelper.animator.GetBool("IsMoving"));

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        

        camera.transform.DOMoveX(cameraEndPosX, time, false);
        //gameObject.transform.DOMove(characterEndPos, time, true);
        transform.parent = playerController.transform;

        yield return new WaitForEndOfFrame();

        transform.DOLocalMoveX(characterEndPosX, time);

        yield return new WaitForSeconds(time - 1);
        //AnimatorHelper.animator.SetBool("IsMoving", false);
        yield return new WaitForSeconds(1);
        Destroy(stage1);
        isIntransition.Value = false;
        GameObject.Find("LeftWall").GetComponent<BoxCollider2D>().enabled = true;

        enabled = false;
    }

}
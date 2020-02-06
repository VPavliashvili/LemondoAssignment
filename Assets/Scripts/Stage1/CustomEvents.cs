using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage1 {

    public class CustomEvents : MonoBehaviour {

        public static CustomEvents instance;

        void Awake() {
            if (instance == null) {
                instance = this;
            }
            else if (instance != this) {
                Destroy(gameObject);
            }
        }

        void Start() {
            instance.OnGameOver += GameOverHelper.OnGameOver;

            instance.OnStagePassed += StagePassHelper.OnStagePassed;
        }
        
        public event Action
            <Func<IEnumerator, Coroutine>, 
            Action<GameObject>,  
            List<GameObject>,
            WallInfo[], int, int,
            Func<GameObject,Vector3,Quaternion,GameObject>,
            GameObject, Vector3, Rigidbody2D, float, float
            > OnGameOver;

        public void RaiseOnGameOver(
            Func<IEnumerator, Coroutine> coroutine, 
            Action<GameObject> destroy,
            List<GameObject> traps,
            WallInfo[] walls, int min, int max,
            Func<GameObject, Vector3, Quaternion, GameObject> instantiate,
            GameObject trapPf,
            Vector3 position, Rigidbody2D rb,
            float startScale, float waitTime
        ) => OnGameOver?.Invoke(
                coroutine, destroy, traps, walls, min, max, instantiate, 
                trapPf, position, rb, startScale, waitTime
            );

        public event Action
            <Stage1.StageCompleter, WallInfo,
            Func<GameObject, Vector3,Quaternion,GameObject>,
            GameObject, Vector3
            > OnStagePassed;
        public void RaiseOnStagePassed(
            Stage1.StageCompleter completer, WallInfo rightWall, 
            Func<GameObject, Vector3, Quaternion, GameObject> instantiate,
            GameObject nextStagePrefab, Vector3 onPosition
            ) => OnStagePassed?.Invoke(completer, rightWall, instantiate, nextStagePrefab, onPosition);
            

    }

}
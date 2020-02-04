using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Level1 {

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

            instance.OnLevelPass += EventMethods.OnLevelPass;
        }
        
        public event Action
            <Func<IEnumerator, Coroutine>, 
            Action<GameObject>,  
            List<GameObject>,
            WallInfo[], int, int,
            Func<GameObject,Vector3,Quaternion,GameObject>,
            GameObject, Transform, Vector3, Rigidbody2D, float, float
            > OnGameOver;

        public void RaiseOnGameOver(
            Func<IEnumerator, Coroutine> coroutine, 
            Action<GameObject> destroy,
            List<GameObject> traps,
            WallInfo[] walls, int min, int max,
            Func<GameObject, Vector3, Quaternion, GameObject> instantiate,
            GameObject trapPf, Transform trapsContainer,
            Vector3 position, Rigidbody2D rb,
            float startScale, float waitTime
        ) => OnGameOver?.Invoke(
                coroutine, destroy, traps, walls, min, max, instantiate, 
                trapPf, trapsContainer, position, rb, startScale, waitTime
            );

        public event Action OnLevelPass;
        public void RaiseOnLevelpass() => OnLevelPass?.Invoke();

    }

}
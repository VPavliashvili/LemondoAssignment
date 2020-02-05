using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage1 {

    public static class GravityHelper {

        public static void FixGravity(
            ref Vector2 movementDir, ref bool isFixedGravity, Transform transform,
            Collider2D other, SpriteRenderer doorRenderer, Sprite openDoor, SpriteMask doorMask, WallInfo rightWall
        ) {
            movementDir = Vector2.right;
            ChangeGravity(0, Vector2.down, transform);
            isFixedGravity = true;

            Vector3 newScale = other.transform.localScale;
            newScale.x = -newScale.x;
            other.transform.localScale = newScale;

            doorRenderer.sprite = openDoor;
            doorMask.enabled = true;
            OpenRightWall(rightWall.transform);
        }

        public static IEnumerator SetRandomGravity(
            Transform transform, Rigidbody2D rb,
            float startingGravityScale, float waitTime
        ) {
            float startingScale = rb.gravityScale;
            rb.gravityScale = startingGravityScale;

            yield return new WaitForSeconds(waitTime);

            int chance = Random.Range(0, 3);
            Vector2 gravityDir = Vector2.zero;
            float angle = 0;

            switch (chance) {
                case 0:
                    gravityDir = Vector2.left;
                    angle = -90;
                    break;
                case 1:
                    gravityDir = Vector2.up;
                    angle = 180;
                    break;
                case 2:
                    gravityDir = Vector2.right;
                    angle = 90;
                    break;
                default:
                    throw new System.Exception("random value error");
            }

            rb.gravityScale = startingScale;
            ChangeGravity(angle, gravityDir, transform);
        }

        public static void ChangeGravity(float desiredZAngle, Vector2 dir, Transform transform) {
            transform.DORotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, desiredZAngle), 0.3f);
            Physics2D.gravity = dir * Utils.G;
        }

        private static void OpenRightWall(Transform wallTrans) {
            float endPosY = wallTrans.localPosition.y + 30;
            wallTrans.DOLocalMoveY(endPosY, 1.5f);
        }

    }

}
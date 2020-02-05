using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInfo : MonoBehaviour {

    private enum Side { Left, Right, Bottom, Top }

    [HideInInspector]
    public BoxCollider2D Collider;
    [HideInInspector]
    public Transform TrapsField;
    [HideInInspector]
    public Vector2 movementDir;
    [HideInInspector]
    public Vector2 gravityDir;

    [HideInInspector]
    public float gravityFlipRotation;

    [SerializeField]
    private Side side;

    void Awake() {
        Collider = GetComponent<BoxCollider2D>();
        TrapsField = transform.GetChild(0);
        switch (side) {
            case Side.Left:
                movementDir = Vector2.down;
                gravityDir = Vector2.left;
                gravityFlipRotation = -90;
                break;
            case Side.Right:
                movementDir = Vector2.up;
                gravityDir = Vector2.right;
                gravityFlipRotation = 90;
                break;
            case Side.Top:
                movementDir = Vector2.left;
                gravityDir = Vector2.up;
                gravityFlipRotation = 180;
                break;
            case Side.Bottom:
                movementDir = Vector2.right;
                gravityDir = Vector2.down;
                gravityFlipRotation = 0;
                break;
            default:
                throw new System.Exception("Side enum value error");
        }
    }

    #region very ugly method hides here
    public Vector2[] GetTrappositions(int number, Bounds bounds, Vector3 trapSize) {

        float min = 0;
        float max = 0;
        float staticPos = 0;
        bool IsLeftOrRight = false;

        if (side == Side.Left || side == Side.Right) {
            min = bounds.min.y;
            max = bounds.max.y;
            staticPos = bounds.center.x;
            IsLeftOrRight = true;
        }
        else {
            min = bounds.min.x;
            max = bounds.max.x;
            staticPos = bounds.center.y;
            IsLeftOrRight = false;
        }

        List<Vector2> res = new List<Vector2>();

        float dynamicPos = 0;
        for (int i = 0; i < number; i++) {
            RandomChoose:
            dynamicPos = Random.Range(min, max);
            Vector2 pos = IsLeftOrRight ? new Vector2(staticPos, dynamicPos) : new Vector2(dynamicPos, staticPos);
            Rect posRect = IsLeftOrRight ?
                new Rect(pos.x, pos.y, trapSize.y, trapSize.x) :
                new Rect(pos.x, pos.y, trapSize.x, trapSize.y);
            //Debug.Log(posRect);

            foreach (Vector2 position in res) {
                Rect rect = IsLeftOrRight ?
                    new Rect(position.x, position.y, trapSize.y, trapSize.x) :
                    new Rect(position.x, position.y, trapSize.x, trapSize.y);
                if (rect.Overlaps(posRect)) {
                    goto RandomChoose;
                }
            }

            res.Add(pos);

        }

        return res.ToArray();
    }
    #endregion
}
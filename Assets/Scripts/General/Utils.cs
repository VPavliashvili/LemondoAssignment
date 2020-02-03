using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

    public const float G = 9.81f;

    public static Bounds GetBoundsWithCollider(Collider2D obj) => obj.bounds;
    public static Bounds GetBoundsWithCollider(Collision2D obj) => obj.collider.bounds;
    public static Bounds GetBoundsWithRenderer(Renderer obj) => obj.bounds;

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatReference {

    [SerializeField]
    private bool isConst = true;
    [SerializeField]
    private float constValue;
    [SerializeField]
    private FloatVariable variable;

    public float Value {
        get => isConst ? constValue : variable.Value;
        set {
            if (isConst)
                constValue = value;
            else
                variable.Value = value;
        }
    }

    public override string ToString() => Value.ToString();

    public static implicit operator float(FloatReference fRef) => fRef.Value;
    
}
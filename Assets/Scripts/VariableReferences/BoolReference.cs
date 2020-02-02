using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoolReference {
    [SerializeField]
    private BoolVariable variable;

    public bool Value {
        get => variable.Value;
        set {
            variable.Value = value;
        }
    }

    public override string ToString() => Value.ToString();

    public static implicit operator bool(BoolReference bRef) => bRef.Value;
    //public static implicit operator BoolReference(bool b) => b;

}
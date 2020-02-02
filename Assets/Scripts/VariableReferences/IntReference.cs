using UnityEngine;

[System.Serializable]
public class IntReference {

    [SerializeField]
    private bool isConst = true;
    [SerializeField]
    private int constValue;
    [SerializeField]
    private IntVariable variable;

    public int Value {
        get => isConst ? constValue : variable.Value;
        set {
            if (isConst) {
                constValue = value;
            }
            else {
                variable.Value = value;
            }
        }
    }


    public override string ToString() => Value.ToString();
    public static implicit operator int(IntReference iRef) => iRef.Value;
    
}

using System.Linq.Expressions;
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

    public static string GetVariableName<T>(Expression<System.Func<T>> expr) {
        var body = (MemberExpression)expr.Body;

        return body.Member.Name;
    }

}
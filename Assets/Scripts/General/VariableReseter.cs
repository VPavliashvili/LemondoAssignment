using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

public class VariableReseter : MonoBehaviour {

    [SerializeField]
    private BoolReference isDeadByBall;
    [SerializeField]
    private BoolReference isInTransition;
    [SerializeField]
    private BoolReference isJumping;
    [SerializeField]
    private BoolReference isWalking;

    void Awake() {
        isDeadByBall.Value = false;
        isInTransition.Value = false;
        isJumping.Value = false;
        isWalking.Value = false;
    }
    
}
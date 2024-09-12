using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBlendTree : StateMachineBehaviour
{
    Player player;
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = player ?? GameManager.Instance.Player;
        player.RestoreSpeed();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdleState : BaseState
{
    private PlayerInput playerInput;

    public override void Enter()
    {
        playerInput = PlayerInput.instance;
    }

    public override void Execute()
    {
        // kiểm tra xem có enemy trong tầm detect hay không 
        if (playerInput.EnemiesAroundPlayer().Count != 0)
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {
        // để fasle trước khi thoát State
    }
}

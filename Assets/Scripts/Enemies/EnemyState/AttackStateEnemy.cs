using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateEnemy : BaseStateEnemy
{
    public override void Enter()
    {
        //Debug.Log("Enter attack enemy");
        enemy.Anim.SetBool("isAttack", true);
    }

    public override void Execute()
    {
        //Debug.Log("Execute attack enemy");
        if (!enemy.DetectCastle())
        {
            stateMachineEnemy.ChangeState(new MovingStateEnemy());
        }
    }

    public override void Exit()
    {
        //Debug.Log("Exit attack enemy");
        enemy.Anim.SetBool("isAttack", false);
    }
}

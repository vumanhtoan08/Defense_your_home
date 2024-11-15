using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieStateEnemy : BaseStateEnemy
{
    private float timeToChangeState = 2f;

    public override void Enter()
    {
        SoundControl.instance.PlayEnemyDie();
        //maxOffset = 0;
        //maxOffset = enemy.Agent.baseOffset;
        //Debug.Log("Enter Die");
        enemy.Agent.SetDestination(enemy.transform.position); // dừng enemy lại
        enemy.Anim.SetTrigger("isDie");
    }

    public override void Execute()
    {
        //Debug.Log("Execute Die");

        timeToChangeState -= Time.deltaTime;
        if (timeToChangeState <= 0)
        {
            stateMachineEnemy.ChangeState(new MovingStateEnemy());
        }
    }

    public override void Exit()
    {
        //Debug.Log("Exit Die");

        timeToChangeState = 0;
    }
}

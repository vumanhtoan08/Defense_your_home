using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingStateEnemy : BaseStateEnemy
{
    private int waypointIndex = 0;
    private Transform castlePos;

    public override void Enter()
    {
    }

    public override void Execute()
    {
        MovingToPath();
        if (enemy.DetectCastle())
        {
            foreach (Collider hit in enemy.Hits)
            {
                Transform temp = hit.transform;
                castlePos = temp;
            }
            // cho enemy di chuyển ra castle với khoảng cách đúng bằng rangeAttack rồi mới bắt đầu Attack
            MovingToTarget(castlePos);
        }
    }

    public override void Exit()
    {
    }

    void MovingToPath()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
            // vẫn chưa đi đến waypoint cuối cùng 
            if (waypointIndex < enemy.path.waypoints.Count - 1)
            {
                waypointIndex++;
            }
        }
    }

    void MovingToTarget(Transform target)
    {
        // lấy vị trí của target và enemy
        Vector3 targetPos = target.position;
        Vector3 enemyPos = enemy.transform.position;

        // tính khoảng cách giữa target và enemy 
        float distanceE2T = Vector3.Distance(targetPos, enemyPos);
        float rangeAttack = enemy.attackRange;

        // so sánh khoảng cách giữa target và enemy với attackRange
        if (distanceE2T > rangeAttack) // nếu target trong vùng đánh của enemy 
        {
            // lấy direction từ enemy tới target
            Vector3 direction = (targetPos - enemyPos).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);

            enemy.Agent.SetDestination(target.position);
        }
        if (distanceE2T <= rangeAttack)
        {
            enemy.Agent.SetDestination(enemy.transform.position);
            stateMachineEnemy.ChangeState(new AttackStateEnemy());
        }
    }
}

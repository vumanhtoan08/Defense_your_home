using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSeleketon : Enemy
{
    public Transform attackPoint;
    public Collider[] attackhits;

    public override void Attack()
    {
        base.Attack();
        SoundControl.instance.PlaySeleketonAttack();

        attackhits = Physics.OverlapSphere(attackPoint.position, 2f, castleLayer);
        foreach (Collider hit in hits)
        {
            CastleHealth castleHealth = hit.GetComponent<CastleHealth>();
            castleHealth.TakeDamage(damage);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 2f);
    }
}

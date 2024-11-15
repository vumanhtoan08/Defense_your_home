using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : Enemy
{
    public Transform firePoint;
    private Transform castlePos;

    protected override void Start()
    {
        base.Start();
        castlePos = GameObject.FindGameObjectWithTag("Castle").transform;
    }

    public override void Attack()
    {
        SoundControl.instance.PlayDevilAttack();

        // tính ra direction từ firepoint với target
        Vector3 direction = (castlePos.position - firePoint.position).normalized;

        GameObject magicBullet = MagicBulletPool.instance.GetPooledObject();
        if (magicBullet != null)
        {
            magicBullet.transform.position = firePoint.transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
            magicBullet.GetComponent<Rigidbody>().velocity = Vector3.zero;

            magicBullet.SetActive(true);
            magicBullet.GetComponent<Rigidbody>().velocity = direction * 10f;
        }
    }
}

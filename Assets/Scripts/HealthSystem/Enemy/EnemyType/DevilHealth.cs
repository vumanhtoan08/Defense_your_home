using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilHealth : EnemyHealth
{
    protected override void Start()
    {
        maxHealth = 50;
        base.Start();
    }
}

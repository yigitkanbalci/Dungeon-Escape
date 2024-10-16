using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamagable
{
    public int Health { get; set; }

    public void Damage()
    {
        Health = base.health;
    }

    public override void Init()
    {
        base.Init();
    }
}

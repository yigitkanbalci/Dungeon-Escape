using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamagable
{
    public int Health { get; set; }

    public void Damage()
    {
        if (isDead)
        {
            return;
        }

        Debug.Log("Damaged: " + gameObject.name);
        isHit = true;
        anim.SetTrigger("Hit");
        anim.SetBool("InCombat", true);
        Debug.Log("Skeleton Hit: " + isHit + "In Combat? " + anim.GetBool("InCombat"));
        Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            Death();
            Destroy(this.gameObject, 1.2f);
        }
    }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
}

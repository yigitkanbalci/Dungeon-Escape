using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamagable
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
        Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            Death();
            Destroy(this.gameObject, 1.5f);
        }
    }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

}

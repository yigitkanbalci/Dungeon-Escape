using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamagable
{
    public int Health { get; set; }

    public GameObject acidEffectPrefab;

    public void Damage()
    {
        Debug.Log("Damaged: " + gameObject.name);
        isHit = true;
        anim.SetTrigger("Hit");
        anim.SetBool("InCombat", true);
        Debug.Log("Spider Hit: " + isHit + "In Combat? " + anim.GetBool("InCombat"));
        Health--;
        if (Health < 1)
        {
            anim.SetTrigger("Death");
            Destroy(this.gameObject, 1.1f);
        }
    }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Attack()
    {
        GameObject acid = Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
        acid.transform.SetParent(transform);
    }

    public override void Movement()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        float threshold = 0.1f;


        if (Vector3.Distance(transform.position, pointA.position) < threshold)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (Vector3.Distance(transform.position, pointB.position) < threshold)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance > 5.0f)
            {
                isHit = false;
                anim.SetBool("InCombat", false);
            } else
            {
                anim.SetTrigger("Idle");
                anim.SetBool("InCombat", true);
            }

            if (anim.GetBool("InCombat"))
            {
                if (transform.position.x > player.transform.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }
        }
    }
}

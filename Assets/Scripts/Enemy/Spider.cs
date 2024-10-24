using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamagable
{
    public int Health { get; set; }

    public GameObject acidEffectPrefab;
    public float attackCooldown = 2f; // Time between attacks
    private float lastAttackTime = 0f; // Time when the last attack happened

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
        Debug.Log("Spider Hit: " + isHit + "In Combat? " + anim.GetBool("InCombat"));
        Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            Death();
            Destroy(this.gameObject, 1.1f);
        }
    }

    public override void Init()
    {
        base.Init();
        Health = base.health;
        lastAttackTime = -attackCooldown; // Allow immediate first attack

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
                isHit = true;
                TryAttack();
                
            }

            if (anim.GetBool("InCombat"))
            {
                if (Time.time >= lastAttackTime + attackCooldown) // Attack only after cooldown
                {
                    anim.SetBool("Cooldown", false); ; // Trigger the attack animation
                    lastAttackTime = Time.time; // Record the attack time
                } else
                {
                    anim.SetBool("Cooldown", true);
                    anim.SetTrigger("Idle");
                }

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

    private void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time; // Record the time of this attack
            anim.Play("Attack"); // Play the attack animation
        }
    }
}

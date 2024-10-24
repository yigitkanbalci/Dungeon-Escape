using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;

    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isHit = false;
    protected bool isDead = false;

    protected Player player;
    [SerializeField]
    protected GameObject diamondPrefab;

    public virtual void Attack()
    {

    }

    public virtual void Init()
    {
        currentTarget = pointB.position;
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
        {
            return;
        }

        Movement();
    }

    public virtual void Death()
    {
        GameObject diamond = (GameObject)Instantiate(diamondPrefab, transform.position, Quaternion.identity);
        Diamond diamondScript = diamond.GetComponent<Diamond>();
        diamondScript.SetAmount(gems);
    }

    public virtual void Movement()
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
       
            if (distance > 2.0f)
            {
                
                isHit = false;
                anim.SetBool("InCombat", false);
            }

            if (anim.GetBool("InCombat"))
            {
                if (transform.position.x > player.transform.position.x)
                {
                    sprite.flipX = true;
                } else
                {
                    sprite.flipX = false;
                }
            }
        }

    }
}

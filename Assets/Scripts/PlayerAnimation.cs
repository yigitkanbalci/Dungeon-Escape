using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Move(float move)
    {
        Flip(move);
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool val)
    {
        _anim.SetBool("Jumping", val);
    }


    private void Flip(float move)
    {
        if (move < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}

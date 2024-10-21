using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    private Spider _spider;

    private void Start()
    {
        _spider = transform.GetComponentInParent<Spider>();
        if (_spider == null)
        {
            Debug.Log("Can't find spider! ");
        }
    }

    public void Fire()
    {
        Debug.Log("Spider should fire");
        _spider.Attack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Vector3 _direction;

    private void Start()
    {
        _sprite = transform.parent.GetComponentInChildren<SpriteRenderer>();
        if (_sprite.flipX == true)
        {
            _direction = Vector3.left;
            Debug.Log("Direction: " + _direction);
        }
        else
        {
            _direction = Vector3.right;
            Debug.Log("Direction: " + _direction);
        }
        Destroy(this.gameObject, 5.0f);
    }

    private void Update()
    {
        transform.Translate(_direction * 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IDamagable hit = collision.GetComponent<IDamagable>();

            if (hit != null)
            {
                hit.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}

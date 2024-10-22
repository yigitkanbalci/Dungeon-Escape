using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    private Player _player;
    [SerializeField]
    private int amount;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (_player == null)
        {
            print("Null");
        }
        amount = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Collect it
            print("Gem collected " + amount);
            _player.CollectGems(amount);
            Destroy(this.gameObject);
        }
    }

    public void SetAmount(int val)
    {
        amount = val;
    }
}

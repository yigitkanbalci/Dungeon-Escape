using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopPanel;
    [SerializeField]
    private Player _player;
    private int selectedItem;
    private int selectedItemCost;

    private void Start()
    {

    }

    private void Update()
    {
        if (!_player)
        {
            return;
        }

        float distance = Vector3.Distance(_player.transform.position, this.transform.position);
        if (distance > 2.0f)
        {
            _shopPanel.SetActive(false);
        }
        else
        {
            UIManager.Instance.OpenShop(_player.GetGems());
            _shopPanel.SetActive(true);
        }
    }

    public void SelectItem(int item)
    {
        Debug.Log("Selected: " + item);
        int yPos = 0;

        selectedItem = item;
        switch (item)
        {
            case 0:
                yPos = 71;
                selectedItemCost = 200;
                break;
            case 1:
                yPos = -33;
                selectedItemCost = 400;
                break;
            case 2:
                yPos = -134;
                selectedItemCost = 100;
                break;
        }

        UIManager.Instance.UpdateSelection(yPos);
    }

    public void BuyItem()
    {
        int gems = _player.GetGems();
        if (gems >= selectedItemCost)
        {

            print(selectedItem);
            if (selectedItem == 2)
            {
                print("here");
                GameManager.Instance.hasKeyToCastle = true;
            }
            // Get item
            _player.SetGems(gems - selectedItemCost);
            Debug.Log("Item bought, gems left: " + _player.GetGems());

        } else
        {
            //Cannot afford
            Debug.Log("Not enough gold!");
        }
    }
}

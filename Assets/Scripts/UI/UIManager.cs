using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is Null!");
            }

            return _instance;
        }
    }

    public TMP_Text _playerGemCount;
    public Image SelectionImg;
    public TMP_Text _gemCountText;
    public Image[] HealthBars;

    public void OpenShop(int gems)
    {
        _playerGemCount.text = gems.ToString() + "G";
    }
    
    private void Awake()
    {
        _instance = this;
    }

    public void UpdateSelection(int yPos)
    {
        SelectionImg.GetComponent<Image>().enabled = true;
        SelectionImg.rectTransform.anchoredPosition = new Vector2(SelectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        print(count);
        _gemCountText.text = count.ToString();
    }

    public void UpdateLives(int lives)
    {
        for (int i = 0; i <= lives; i++)
        {
            if (i == lives) {
                HealthBars[i].enabled = false;
            }
        }
    }
}

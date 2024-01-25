using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LojaControl : MonoBehaviour
{

    private StatusPlayer statusPlayer;
    private MoveControl moveControl;

    [SerializeField] private GameObject shop;
    [SerializeField] private TextMeshProUGUI maxLifeText;
    [SerializeField] private TextMeshProUGUI maxStaminaText;
    [SerializeField] private TextMeshProUGUI damageText;


    private bool inShop = false;

    void Update()
    {
        ShopLogic();
    }

    public void ShopLogic()
    {
        if (inShop == true)
        {
            shop.SetActive(true);
            moveControl.canMoveValue = false;
            maxLifeText.text = statusPlayer.maxLifeValue.ToString();
            maxStaminaText.text = statusPlayer.maxStaminaValue.ToString();
            damageText.text = statusPlayer.damageValue.ToString();
        }
        if (inShop == true && Input.GetKeyDown(KeyCode.Q) == true)
        {
            inShop = false;
            shop.SetActive(false);
            moveControl.canMoveValue = true;
        }
    }

    public void MaxLifeUp()
    {
        if (statusPlayer.coinsValue >= 10)
        {
            statusPlayer.maxLifeValue += 2;
            statusPlayer.coinsValue -= 10;
        }
    }

    public void MaxStaminaUp()
    {
        if (statusPlayer.coinsValue >= 20)
        {
            statusPlayer.maxStaminaValue += 1;
            statusPlayer.coinsValue -= 20;
        }
}
    public void DamageUp()
    {
        if (statusPlayer.coinsValue >= 10)
        {
            statusPlayer.damageValue += 1;
            statusPlayer.coinsValue -= 10;
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MoveControl>() == null)
            return;

        if (Input.GetKeyDown(KeyCode.E) == true)
        {
            inShop = true;
            statusPlayer = collision.GetComponent<StatusPlayer>();
            moveControl = collision.GetComponent<MoveControl>();
        }
    }

}

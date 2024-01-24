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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        if (inShop == true && moveControl != null && moveControl.GetComponent<InputControl>().inputYValue < 0)
        {
            inShop = false;
            shop.SetActive(false);
            moveControl.canMoveValue = true;
        }
    }

    public void MaxLifeUp()
    {
        if (statusPlayer.coinsValue >= 20)
        {
            statusPlayer.maxLifeValue += 50;
            statusPlayer.coinsValue -= 20;
        }
    }

    public void MaxStaminaUp()
    {
        if (statusPlayer.coinsValue >= 20)
        {
            statusPlayer.maxStaminaValue += 20;
            statusPlayer.coinsValue -= 20;
        }
}
    public void DamageUp()
    {
        if (statusPlayer.coinsValue >= 20)
        {
            statusPlayer.damageValue += 2;
            statusPlayer.coinsValue -= 20;
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<InputControl>().inputYValue > 0)
        {
            inShop = true;
            statusPlayer = collision.GetComponent<StatusPlayer>();
            moveControl = collision.GetComponent<MoveControl>();
        }
    }
}

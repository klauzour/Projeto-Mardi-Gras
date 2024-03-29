using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LojaControl : MonoBehaviour
{

    private StatusPlayer statusPlayer;
    private ItensControl itensControl;
    private MoveControl moveControl;

    [SerializeField] private GameObject shop;
    [SerializeField] private TextMeshProUGUI maxLifeText;
    [SerializeField] private TextMeshProUGUI maxStaminaText;
    [SerializeField] private TextMeshProUGUI damageText;

    [SerializeField] private TextMeshProUGUI minePotionLifeText;
    [SerializeField] private TextMeshProUGUI normalPotionLifeText;
    [SerializeField] private TextMeshProUGUI minePotionStaminaText;
    [SerializeField] private TextMeshProUGUI normalPotionStaminaText;

    [SerializeField] private TextMeshProUGUI potionLifeAndStaminaText;

    private bool inShop = false;

    void Update()
    {
        ShopLogic();
    }

    public bool inShopValue
    { 
        get { return inShop; } 
        set {  inShop = value; } 
    }

    public void ShopLogic()
    {
        if (statusPlayer != null && itensControl != null)
        {
            maxLifeText.text = statusPlayer.maxLifeValue.ToString();
            maxStaminaText.text = statusPlayer.maxStaminaValue.ToString();
            damageText.text = statusPlayer.damageValue.ToString();

            minePotionLifeText.text = itensControl.itemMinePotionLifeValue.ToString();
            normalPotionLifeText.text = itensControl.itemNormalPotionLifeValue.ToString();
            minePotionStaminaText.text = itensControl.itemMinePotionStaminaValue.ToString();
            normalPotionStaminaText.text = itensControl.itemNormalPotionStaminaValue.ToString();
            potionLifeAndStaminaText.text = itensControl.itemPotionLifeAndStaminaValue.ToString();

        }
        if (inShop == true && Input.GetKeyDown(KeyCode.E) == true)
        {
            var playerRb = moveControl.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
            shop.SetActive(true);
            moveControl.canMoveValue = false;
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

    public void MinePotionLifeUp()
    {
        if (statusPlayer.coinsValue >= 10)
        {
            itensControl.itemMinePotionLifeValue += 1;
            statusPlayer.coinsValue -= 10;
        }
    }

    public void NormalPotionLifeUp()
    {
        if (statusPlayer.coinsValue >= 10)
        {
            itensControl.itemNormalPotionLifeValue += 1;
            statusPlayer.coinsValue -= 10;
        }
    }

    public void MinePotionStaminaUp()
    {
        if (statusPlayer.coinsValue >= 10)
        {
            itensControl.itemMinePotionStaminaValue += 1;
            statusPlayer.coinsValue -= 10;
        }
    }

    public void NormalPotionStaminaUp()
    {
        if (statusPlayer.coinsValue >= 10)
        {
            itensControl.itemNormalPotionStaminaValue += 1;
            statusPlayer.coinsValue -= 10;
        }
    }
    public void PotionLifeAndStaminaUp()
    {
        if (statusPlayer.coinsValue >= 20)
        {
            itensControl.itemPotionLifeAndStaminaValue += 1;
            statusPlayer.coinsValue -= 20;
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MoveControl>() == null)
            return;

        inShop = true;
        statusPlayer = collision.GetComponent<StatusPlayer>();
        moveControl = collision.GetComponent<MoveControl>();
        itensControl = collision.GetComponent<ItensControl>();

        /*if (Input.GetKeyDown(KeyCode.E) == true)
        {
            inShop = true;
            statusPlayer = collision.GetComponent<StatusPlayer>();
            moveControl = collision.GetComponent<MoveControl>();
        }*/
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        inShop = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensControl : MonoBehaviour
{
    private StatusPlayer statusPlayer;


    [SerializeField] private int itemMinePotionLife;
    [SerializeField] private int itemNormalPotionLife;
    [SerializeField] private int minePotionLife;
    [SerializeField] private int normalPotionLife;

    [SerializeField] private int itemMinePotionStamina;
    [SerializeField] private int itemNormalPotionStamina;
    [SerializeField] private int minePotionStamina;
    [SerializeField] private int normalPotionStamina;

    [SerializeField] private int itemPotionLifeAndStamina;
    [SerializeField] private int potionLife;
    [SerializeField] private int potionStamina;


    // Start is called before the first frame update
    void Start()
    {
        statusPlayer = GetComponent<StatusPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int itemMinePotionLifeValue
    {
        get { return itemMinePotionLife; }
        set { itemMinePotionLife = value; }
    }

    public int itemNormalPotionLifeValue
    {
        get { return itemNormalPotionLife; }
        set { itemNormalPotionLife = value; }
    }

    public int itemMinePotionStaminaValue
    {
        get { return itemMinePotionStamina; }
        set { itemMinePotionStamina = value; }
    }

    public int itemNormalPotionStaminaValue
    {
        get { return itemNormalPotionStamina; }
        set { itemNormalPotionStamina = value; }
    }

    public int itemPotionLifeAndStaminaValue
    {
        get { return itemPotionLifeAndStamina; }
        set { itemPotionLifeAndStamina = value; }
    }


    public void UseMinePotionLife()
    {
        if (itemMinePotionLife > 0)
        {
            itemMinePotionLife -= 1;

            statusPlayer.lifeValue += minePotionLife;
        }
    }

    public void UseNormalPotionLife()
    {
        if (itemNormalPotionLife > 0)
        {
            itemNormalPotionLife -= 1;

            statusPlayer.lifeValue += normalPotionLife;
        }
    }

    public void UseMinePotionStamina()
    {
        if (itemMinePotionStamina > 0)
        {
            itemMinePotionStamina -= 1;

            statusPlayer.staminaValue += minePotionStamina;
        }
    }

    public void UseNormalPotionStamina()
    {
        if (itemNormalPotionStamina > 0)
        {
            itemNormalPotionStamina -= 1;

            statusPlayer.staminaValue += normalPotionStamina;
        }
    }
    public void UsePotionLifeAndStamina()
    {
        if (itemPotionLifeAndStamina > 0)
        {
            itemPotionLifeAndStamina -= 1;

            statusPlayer.staminaValue += potionLife;
            statusPlayer.staminaValue += potionStamina;
        }
    }





}

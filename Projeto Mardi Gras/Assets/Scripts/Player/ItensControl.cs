using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI largeC;
    public TextMeshProUGUI smallC;
    public TextMeshProUGUI largeT;
    public TextMeshProUGUI smallT;
    public TextMeshProUGUI caldo;

    // Start is called before the first frame update
    void Start()
    {
        statusPlayer = GetComponent<StatusPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        largeC.text = $"{itemNormalPotionStamina}";
        smallC.text = $"{itemMinePotionStamina}";
        largeT.text = $"{itemNormalPotionLife}";
        smallT.text = $"{itemMinePotionLife}";
        caldo.text = $"{itemPotionLifeAndStamina}";
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
        if (itemMinePotionLife > 0 && statusPlayer.lifeValue < statusPlayer.maxLifeValue)
        {
            itemMinePotionLife -= 1;

            int amountToAdd = Mathf.Min(minePotionLife, statusPlayer.maxLifeValue - statusPlayer.lifeValue);
            statusPlayer.lifeValue += amountToAdd;
        }
    }

    public void UseNormalPotionLife()
    {
        if (itemNormalPotionLife > 0 && statusPlayer.lifeValue < statusPlayer.maxLifeValue)
        {
            itemNormalPotionLife -= 1;

            int amountToAdd = Mathf.Min(normalPotionLife, statusPlayer.maxLifeValue - statusPlayer.lifeValue);
            statusPlayer.lifeValue += amountToAdd;
        }
    }

    public void UseMinePotionStamina()
    {
        if (itemMinePotionStamina > 0 && statusPlayer.staminaValue < statusPlayer.maxStaminaValue)
        {
            itemMinePotionStamina -= 1;

            int amountToAdd = Mathf.Min(minePotionStamina, statusPlayer.maxStaminaValue - statusPlayer.staminaValue);
            statusPlayer.staminaValue += amountToAdd;
        }
    }

    public void UseNormalPotionStamina()
    {
        if (itemNormalPotionStamina > 0 && statusPlayer.staminaValue < statusPlayer.maxStaminaValue)
        {
            itemNormalPotionStamina -= 1;

            int amountToAdd = Mathf.Min(normalPotionStamina, statusPlayer.maxStaminaValue - statusPlayer.staminaValue);
            statusPlayer.staminaValue += amountToAdd;
        }
    }

    public void UsePotionLifeAndStamina()
    {
        if (itemPotionLifeAndStamina > 0)
        {
            itemPotionLifeAndStamina -= 1;

            int amountToAddLife = Mathf.Min(potionLife, statusPlayer.maxLifeValue - statusPlayer.lifeValue);
            int amountToAddStamina = Mathf.Min(potionStamina, statusPlayer.maxStaminaValue - statusPlayer.staminaValue);

            statusPlayer.lifeValue += amountToAddLife;
            statusPlayer.staminaValue += amountToAddStamina;
        }
    }
}
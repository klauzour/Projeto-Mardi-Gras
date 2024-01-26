using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusPlayer : MonoBehaviour
{

    [SerializeField] private int maxLife;
    [SerializeField] private int life;
    [SerializeField] private int maxStamina;
    [SerializeField] private int stamina;
    [SerializeField] private int damage;

    [SerializeField] private int coins;
    [SerializeField] private TextMeshProUGUI textCoin;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        textCoin.text = coins.ToString();
    }

    public int maxLifeValue
    {
        get { return maxLife; }
        set { maxLife = value; }
    }

    public int lifeValue
    {
        get { return life; }
        set { life = value; }
    }

    public int maxStaminaValue
    {
        get { return maxStamina; }
        set { maxStamina = value; }
    }

    public int staminaValue
    {
        get { return stamina; }
        set { stamina = value; }
    }

    public int damageValue
    {
        get { return damage; }
        set { damage = value; }
    }

    public int coinsValue
    {
        get { return coins; }
        set { coins = value; }
    }

}

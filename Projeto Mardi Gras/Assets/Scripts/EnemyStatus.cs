using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{

    public int maxLife;
    public int life;
    public int maxStamina;
    public int stamina;
    public int strength;

    public int attackProbability;
    public int dodgeProbability;
    public int waitProbability;

    public Combat combatScript;
    public Image enemyImage;

    // Start is called before the first frame update
    void Start()
    {
        combatScript = Object.FindFirstObjectByType<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Combat.CombatAction ChosenAction()
    {
        int randomValue = Random.Range(1, 101);

        if (randomValue <= attackProbability && stamina >= 1)
        {
            Debug.Log("inimigo atacou");
            return Combat.CombatAction.Attack;            
        }
        else if (randomValue <= attackProbability + dodgeProbability && stamina >= 2)
        {
            Debug.Log("inimigo deu dodge");
            return Combat.CombatAction.Dodge;            
        }
        else
        {
            Debug.Log("Inimigo esperou");
            return Combat.CombatAction.Wait;
        }
    }

}

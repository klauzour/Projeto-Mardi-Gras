using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static EnemyStatus;

public class Combat : MonoBehaviour
{
    public static Combat Instance;
    public StatusPlayer player;
    public EnemyStatus enemy;

    public TextMeshProUGUI playervida;
    public TextMeshProUGUI playerStrenght;
    public TextMeshProUGUI playerstamina;
    public TextMeshProUGUI playercoins;
    public TextMeshProUGUI enemyvida;
    public TextMeshProUGUI enemyStrenght;
    public TextMeshProUGUI enemyStamina;

    public Button attackButton;
    public Button dodgeButton;
    public Button waitButton;
    public Button itemsButton;

    private CombatAction playerChosenAction;
    private CombatAction enemyChosenAction;    

    private void Awake()
    {
        // Configura o Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Adicione os listeners aos botões de UI
        attackButton.onClick.AddListener(PlayerAttack);
        dodgeButton.onClick.AddListener(PlayerDodge);
        waitButton.onClick.AddListener(PlayerWait);

        // Inicie o combatea
        player = Object.FindFirstObjectByType<StatusPlayer>();
    }

    public void StartCombat(GameObject selectedEnemy)
    {
        enemy = Object.FindFirstObjectByType<EnemyStatus>();
        player = Object.FindFirstObjectByType<StatusPlayer>();
        StartCoroutine(StartCombatUI());
    }

    IEnumerator StartCombatUI()
    {
        // Aguarde um pequeno tempo para dar tempo de configurar o combate
        yield return new WaitForSeconds(0.1f);

        // Atualize a interface com os status do jogador e do inimigo
        UpdateUI();

        // Inicie o combate
        StartTurn();
    }

    void StartTurn()
    {                
        // Atualize a interface com os status do jogador e do inimigo
        UpdateUI();
    }

    public void PlayerAttack()
    {
        playerChosenAction = CombatAction.Attack;
        EnemyTurn();
        
    }

    public void PlayerDodge()
    {
        playerChosenAction = CombatAction.Dodge;
        EnemyTurn();        
    }

    public void PlayerWait()
    {
        playerChosenAction = CombatAction.Wait;
        EnemyTurn();        
    }

    void ExecuteActions()
    {
        if (playerChosenAction == CombatAction.Attack && player.staminaValue >= 1)
        {
            if (enemyChosenAction == CombatAction.Attack)
            {
                // Ambos atacam, aplicar dano a ambos
                TakeDamage(player, enemy.strength);
                TakeDamage(enemy, player.damageValue);
                enemy.stamina -= 1;
                player.staminaValue -= 1;
            }
            else if (enemyChosenAction == CombatAction.Dodge)
            {
                enemy.stamina -= 2;
                player.staminaValue -= 1;
                // Jogador ataca, inimigo desvia, nenhum dano é aplicado
            }
            else if (enemyChosenAction == CombatAction.Wait)
            {
                // Jogador ataca, inimigo espera, inimigo leva dano
                TakeDamage(enemy, player.damageValue * 2);
                enemy.stamina += 2;
                player.staminaValue -= 1;
            }
        }
        else if (playerChosenAction == CombatAction.Dodge && player.staminaValue >= 2)
        {         

            if (enemyChosenAction == CombatAction.Attack)
            {
                enemy.stamina -= 1;
                player.staminaValue -= 2;
                // Jogador esquiva, inimigo ataca, nenhum dano é aplicado
            }
            else if (enemyChosenAction == CombatAction.Dodge)
            {
                enemy.stamina -= 2;
                player.staminaValue -= 2;
                // Ambos esquivam, nenhum dano é aplicado
            }
            else if (enemyChosenAction == CombatAction.Wait)
            {
                // Jogador esquiva, inimigo espera, jogador gasta o dobro de stamina
                player.staminaValue -= 4;
                enemy.stamina += 2;
            }
        }
        else if (playerChosenAction == CombatAction.Wait)
        {

            if (enemyChosenAction == CombatAction.Attack)
            {
                enemy.stamina -= 1;
                player.staminaValue += 2;
                // Jogador espera, inimigo ataca, jogador leva dano
                TakeDamage(player, enemy.strength * 2);
            }
            else if (enemyChosenAction == CombatAction.Dodge)
            {
                // Jogador espera, inimigo esquiva, inimigo gasta o dobro de stamina
                enemy.stamina -= 4;
                player.staminaValue += 2;
            }
            else if (enemyChosenAction == CombatAction.Wait)
            {
                enemy.stamina += 2;
                player.staminaValue += 2;
                // Ambos esperam, nenhum dano é aplicado
            }
        }

        player.staminaValue = Mathf.Clamp(player.staminaValue, 0, player.maxStaminaValue);
        enemy.stamina = Mathf.Clamp(enemy.stamina, 0, enemy.maxStamina);

        // Atualize a interface com os status do jogador e do inimigo
        UpdateUI();
    }

    void EnemyTurn()
    {
        enemyChosenAction = enemy.ChosenAction();
        ExecuteActions();
        CheckCombatResult();
        UpdateUI();
    }    

    void TakeDamage(StatusPlayer target, int damage)
    {
        target.lifeValue -= damage;
        target.lifeValue = Mathf.Clamp(target.lifeValue, 0, target.maxLifeValue); // Garante que a vida não ultrapasse o máximo nem fique abaixo de 0

        if (target.lifeValue < 0)
        {
            target.lifeValue = 0;
        }
    }

    void TakeDamage(EnemyStatus target, int damage)
    {
        target.life -= damage;
        target.life = Mathf.Clamp(target.life, 0, target.maxLife); // Garante que a vida não ultrapasse o máximo nem fique abaixo de 0

        if (target.life < 0)
        {
            target.life = 0;
        }
    }

    void CheckCombatResult()
    {
        // Aqui você implementará a lógica para verificar se o combate deve continuar ou terminar
        // Pode ser com base em condições como vida restante, número de turnos, etc.

        // Exemplo (mude conforme necessário):
        if (player.lifeValue <= 0)
        {
            Debug.Log("O jogador perdeu o combate");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // Adicione a lógica para o jogador perder o combate
        }
        else if (enemy.life <= 0)
        {
            Debug.Log("O jogador venceu o combate");
            gameObject.SetActive(false);
            // Adicione a lógica para o jogador vencer o combate
        }
        else
        {
            // O combate continua, inicie o próximo turno
            StartTurn();
        }
    }

    void UpdateUI()
    {
        // Atualize a interface com os status do jogador e do inimigo
        playervida.text = $"{player.lifeValue}";
        playerStrenght.text = $"{player.damageValue}";
        playerstamina.text = $"{player.staminaValue}";
        playercoins.text = $"{player.coinsValue}";

        enemyvida.text = $"{enemy.life}";
        enemyStrenght.text = $"{enemy.strength}";
        enemyStamina.text = $"{enemy.stamina}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum CombatAction
    {
        Attack,
        Dodge,
        Wait
    }
}

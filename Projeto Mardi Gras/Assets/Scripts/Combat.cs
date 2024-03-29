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
    public MoveControl moveControl;
    public Image enemyImageUI;
    public combatanimations playerAnimationController;

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

    private CombatAction playerChosenAction;
    private CombatAction enemyChosenAction;
    public AudioSource dodgeSound;
    public AudioSource attackSound;

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
        // Inicie o combatea
        player = Object.FindFirstObjectByType<StatusPlayer>();
        moveControl = Object.FindFirstObjectByType<MoveControl>();
    }

    public void StartCombat(GameObject selectedEnemy)
    {

        // Obtenha o componente EnemyStatus do objeto selecionado
        enemy = selectedEnemy.GetComponent<EnemyStatus>();

        if (enemy == null)
        {
            Debug.LogError("EnemyStatus component not found on the selectedEnemy GameObject.");
            return;
        }

        if (moveControl != null)
        {
            var playerRb = moveControl.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);            
            moveControl.canMoveValue = false;
        }

        player = Object.FindFirstObjectByType<StatusPlayer>();
        StartCoroutine(StartCombatUI());
    }


    IEnumerator StartCombatUI()
    {
        yield return new WaitForSeconds(0.1f);

        // Atualize a interface com os status do jogador e do inimigo
        UpdateUI();

        // Substitua a imagem da UI pela imagem do inimigo
        if (enemyImageUI != null && enemy != null && enemy.enemyImage != null)
        {
            enemyImageUI.sprite = enemy.enemyImage.sprite; // Use a propriedade sprite para obter a sprite da Image
        }

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
        playerAnimationController.PlayAttackAnimation();
        attackSound.Play();
        EnemyTurn();
    }

    public void PlayerDodge()
    {
        playerChosenAction = CombatAction.Dodge;
        playerAnimationController.PlayDodgeAnimation();
        dodgeSound.Play();
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
                // Jogador ataca, inimigo desvia, nenhum dano � aplicado
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
                // Jogador esquiva, inimigo ataca, nenhum dano � aplicado
            }
            else if (enemyChosenAction == CombatAction.Dodge)
            {
                enemy.stamina -= 2;
                player.staminaValue -= 2;
                // Ambos esquivam, nenhum dano � aplicado
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
                // Ambos esperam, nenhum dano � aplicado
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
        target.lifeValue = Mathf.Clamp(target.lifeValue, 0, target.maxLifeValue); // Garante que a vida n�o ultrapasse o m�ximo nem fique abaixo de 0

        if (target.lifeValue < 0)
        {
            target.lifeValue = 0;
        }
    }

    void TakeDamage(EnemyStatus target, int damage)
    {
        target.life -= damage;
        target.life = Mathf.Clamp(target.life, 0, target.maxLife); // Garante que a vida n�o ultrapasse o m�ximo nem fique abaixo de 0

        if (target.life < 0)
        {
            target.life = 0;
        }
    }

    void CheckCombatResult()
    {
        // Aqui voc� implementar� a l�gica para verificar se o combate deve continuar ou terminar
        // Pode ser com base em condi��es como vida restante, n�mero de turnos, etc.

        // Exemplo (mude conforme necess�rio):
        if (player.lifeValue <= 0)
        {
            Debug.Log("O jogador perdeu o combate");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // Adicione a l�gica para o jogador perder o combate
        }
        else if (enemy.life <= 0)
        {
            Debug.Log("O jogador venceu o combate");
            gameObject.SetActive(false);
            player.lifeValue = player.maxLifeValue;
            player.staminaValue = player.maxStaminaValue;
            enemy.life = enemy.maxLife;
            enemy.stamina = enemy.maxStamina;
            // Adicione a l�gica para o jogador vencer o combate
            if (moveControl != null)
            {
                moveControl.canMoveValue = true;
            }
            player.coinsValue += 100;
        }
        else
        {
            // O combate continua, inicie o pr�ximo turno
            StartTurn();
        }
    }

    public void UpdateUI()
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
        UpdateUI();
    }

    public enum CombatAction
    {
        Attack,
        Dodge,
        Wait
    }
}
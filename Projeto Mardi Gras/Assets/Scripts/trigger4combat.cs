using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger4combat : MonoBehaviour
{
    public GameObject combatObject;
    public GameObject selectedEnemy; // Prefab do inimigo atual

    private void Start()
    {
        if (combatObject == null)
        {
            Debug.LogError("Objeto com script Combat não atribuído ao trigger4combat.");
            return;
        }

        // Garante que o objeto com o script Combat está ativado no início
        combatObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Trigger detectado pelo jogador.");
            combatObject.SetActive(true);
            // Quando o jogador entra no trigger, inicia o combate com o inimigo selecionado
            Combat.Instance.StartCombat(selectedEnemy);
            Destroy(gameObject);
        }
    }   
}

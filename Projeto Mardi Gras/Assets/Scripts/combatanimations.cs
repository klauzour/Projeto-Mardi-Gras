using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatanimations : MonoBehaviour
{    
    private Animator playerAnimator;

    private void Start()
    {
        // Obtenha o componente Animator no mesmo GameObject
        playerAnimator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        // Ative a animação de ataque
        playerAnimator.SetTrigger("Attack");
    }

    public void PlayDodgeAnimation()
    {
        // Ative a animação de esquiva
        playerAnimator.SetTrigger("Dodge");
    }

    // Adicione métodos adicionais conforme necessário
}


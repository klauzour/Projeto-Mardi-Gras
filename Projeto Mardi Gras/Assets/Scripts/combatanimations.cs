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
        // Ative a anima��o de ataque
        playerAnimator.SetTrigger("Attack");
    }

    public void PlayDodgeAnimation()
    {
        // Ative a anima��o de esquiva
        playerAnimator.SetTrigger("Dodge");
    }

    // Adicione m�todos adicionais conforme necess�rio
}


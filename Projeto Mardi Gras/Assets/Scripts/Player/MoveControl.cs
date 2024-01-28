using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MoveControl : MonoBehaviour
{

    private Rigidbody2D rb;
    private InputControl inputControl;

    [SerializeField] private float velocidade;
    private bool canMove = true;
    private bool inStair = false;

    private Animator anim;
    private Transform sprite;
    private int spriteLocal = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = GetComponent<InputControl>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<Transform>();
    }

    private void FixedUpdate()
    {
        MoveLogic();
        AnimLogic();
    }

    public float velocidadeValue
    {
        get { return velocidade; }
        set { velocidade = value; }
    }

    public bool canMoveValue
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public bool inStairValue
    {
        get { return inStair; }
        set { inStair = value; }
    }

    public void MoveLogic()
    {
        if (canMove == true && inStair == false)
        {
            rb.velocity = new Vector2(inputControl.inputXValue * velocidade, rb.velocity.y);
        }
    }

    public void FlipLogic()
    {
        if (rb.velocity.x > 0)
            spriteLocal = 1;
        else if (rb.velocity.x < 0)
            spriteLocal = -1;
        sprite.localScale = new Vector2(spriteLocal, sprite.localScale.y);
    }

    public void AnimLogic()
    {
        FlipLogic();
        anim.SetFloat("Horizontal", rb.velocity.x);
    }

}

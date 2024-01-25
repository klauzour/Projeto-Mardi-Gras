using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{

    private Rigidbody2D rb;
    private InputControl inputControl;

    [SerializeField] private float velocidade;
    private bool canMove = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = GetComponent<InputControl>();
    }

    private void FixedUpdate()
    {
        MoveLogic();
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

    public void MoveLogic()
    {
        if (canMove == true)
        {
            rb.velocity = new Vector2(inputControl.inputXValue * velocidade, rb.velocity.y);
        }
    }
}

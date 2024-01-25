using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMove : MonoBehaviour
{
    private InputControl inputControl;
    private MoveControl moveControl;
    private Rigidbody2D rb;
    private GroundCheck groundCheck;

    float climbSpeed = 5f;
    float climbJumpSpeed = 2f;
    bool onStair = false;

    void Update()
    {
        if (onStair)
        {
            moveControl.canMoveValue = false;
            rb.gravityScale = 0;
            var playerTransform = moveControl.GetComponent<Transform>();
            playerTransform.position = new Vector3 (transform.position.x, playerTransform.position.y, playerTransform.position.z);

            rb.velocity = new Vector2(0, climbSpeed * inputControl.inputYValue);

            if (inputControl.inputXValue != 0 && groundCheck.Grounded)
            {
                onStair = false;
            }
        }
        else if (rb)
        {
            rb.GetComponent<Rigidbody2D>().gravityScale = 1;
            moveControl.canMoveValue = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MoveControl>() == null)
            return;

        rb = collision.GetComponent<Rigidbody2D>();
        groundCheck = collision.GetComponentInChildren<GroundCheck>();
        moveControl = collision.GetComponent<MoveControl>();
        inputControl = collision.GetComponent<InputControl>();

        if (inputControl.inputYValue != 0)
        {
            onStair = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MoveControl>() == null)
            return;

        if (onStair)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, climbJumpSpeed), ForceMode2D.Impulse);
            onStair = false;
        }

    }
}

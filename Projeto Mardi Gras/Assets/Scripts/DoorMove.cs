using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{

    [SerializeField] private Transform moveObj;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MoveControl>() == null)
            return;

        if (collision.GetComponent<InputControl>().inputYValue > 0)
        {
            collision.transform.position = moveObj.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{

    [SerializeField] private Transform moveObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<InputControl>().inputYValue > 0)
        {
            collision.transform.position = moveObj.position;
        }
    }
}

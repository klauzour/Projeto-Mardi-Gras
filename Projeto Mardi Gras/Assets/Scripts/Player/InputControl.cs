using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{

    private float inputX, inputY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputLogic();
    }

    public float inputXValue
    { 
      get { return inputX; } 
      set { inputX = value; }
    }

    public float inputYValue
    {
        get { return inputY; }
        set { inputY = value; }
    }

    public void InputLogic()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }
}

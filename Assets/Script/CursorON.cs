using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ui ELEMENTS
public class CursorON : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;       //make cursor visible when win ot lose (UI)    
        Cursor.lockState = CursorLockMode.None; //CAN GOOUT
    }

}

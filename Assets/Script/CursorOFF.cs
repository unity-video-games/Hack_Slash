using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player plays
public class CursorOFF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;       //make cursor invisible inside game
        Cursor.lockState = CursorLockMode.Locked;     //make cursor cannot goout of th game
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

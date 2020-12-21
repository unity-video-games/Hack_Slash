using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtUI : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
       //To show enemy health bar 
		transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}

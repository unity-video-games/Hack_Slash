using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    float rot;
    [SerializeField]
    float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
		//Rotate Heart
        transform.rotation = Quaternion.Euler(0,rot * RotationSpeed, 0);
        rot++;
    }
}

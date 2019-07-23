using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private void Start()
    {
        if (transform.position.z > 0) 
        {
            transform.eulerAngles = 180 * Vector3.up;
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        transform.position = new Vector3(transform.position.x, 1.3f, transform.position.z);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotate = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.left * rotate);
    }
}

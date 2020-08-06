﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField]
    Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }
}

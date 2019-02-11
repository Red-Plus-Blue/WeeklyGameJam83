﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : Controller
{
    protected override void RunUpdate()
    {
        var direction = (gameObject.transform.forward * Input.GetAxis("Vertical")) + 
            (gameObject.transform.right * Input.GetAxis("Horizontal"));

        InputVertical = direction.z;
        InputHorizontal = direction.x;

        base.RunUpdate();
    }
}
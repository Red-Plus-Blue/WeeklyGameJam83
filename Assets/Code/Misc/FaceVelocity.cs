using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FaceVelocity : MonoBehaviour
{
    public Rigidbody Rigidbody;

    private void Update()
    {
        var direction = Rigidbody.velocity;
        if(direction.magnitude > 0.0f)
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, .1f);
        }
    }

}
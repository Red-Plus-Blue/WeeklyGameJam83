using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TrackingCamera : MonoBehaviour
{
    public Transform Target;

    protected Vector3 _offset;

    protected Quaternion _targetRotation;

    private void Awake()
    {
        _offset = gameObject.transform.position - Target.position;
        _targetRotation = gameObject.transform.rotation;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _targetRotation *= Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _targetRotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }

        gameObject.transform.position = Vector3.Lerp(
             gameObject.transform.position,
             Target.transform.position + _offset,
             .05f
        );

        gameObject.transform.rotation = Quaternion.Lerp(
            gameObject.transform.rotation,
            _targetRotation,
            .05f
        );
    }
}
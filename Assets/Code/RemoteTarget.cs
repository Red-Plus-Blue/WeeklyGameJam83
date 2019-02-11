using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteTarget : MonoBehaviour
{
    public GameObject RemoteCursor;

    public Transform LineOrigin;

    public LineRenderer LineRenderer;

    protected int _mode = 0;
    protected string[] _modes = { "red", "blue", "yellow" };

    public Color[] Colors;
    public Image[] ColorImages;

    protected Plane _plane;

    private void Awake()
    {
        _plane = new Plane(Vector3.up, gameObject.transform.position);
        UpdateColors();
    }

    protected void UpdateColors()
    {
        ColorImages[0].color = Colors[_mode];
        var nextMode = _mode > (_modes.Length - 2) ? 0 : _mode + 1;
        ColorImages[1].color = Colors[nextMode];
        nextMode = nextMode > (_modes.Length - 2) ? 0 : nextMode + 1;
        ColorImages[2].color = Colors[nextMode];
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            _mode = _mode > (_modes.Length - 2) ? 0 : _mode + 1;
            UpdateColors();
        }

        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var mouseTarget = Vector3.zero;

        if(_plane.Raycast(mouseRay, out float enter))
        {
            mouseTarget = mouseRay.GetPoint(enter);
        }

        var ray = new Ray(
            transform.position,
            mouseTarget - transform.position
        );

        var maxDistance = Vector3.Distance(transform.position, mouseTarget);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            RemoteCursor.transform.position = hit.point;

            var controlPanel = hit.collider.gameObject.GetComponent<ControlPanel>();
            if(controlPanel && Input.GetMouseButtonDown(0))
            {
                controlPanel.Activate(_mode);
            }
        }
        else
        {
            RemoteCursor.transform.position = mouseTarget;
        }

        var linePositions = new Vector3[]
        {
            transform.position,
            RemoteCursor.transform.position
        };
        LineRenderer.SetPositions(linePositions);
    }

}

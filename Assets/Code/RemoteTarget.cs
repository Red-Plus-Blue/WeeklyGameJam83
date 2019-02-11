using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteTarget : MonoBehaviour
{
    public GameObject RemoteCursor;

    public Transform LineOrigin;

    public LineRenderer LineRenderer;

    protected Plane _plane;

    private void Awake()
    {
        _plane = new Plane(Vector3.up, gameObject.transform.position);
    }

    private void Update()
    {
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
                controlPanel.Activate();
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

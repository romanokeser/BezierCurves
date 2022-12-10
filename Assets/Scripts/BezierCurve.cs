using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    [SerializeField] private GameObject _start;
    [SerializeField] private GameObject _middle;
    [SerializeField] private GameObject _end;
    [SerializeField] private Color _color;
    [SerializeField] private float _width;
    [SerializeField] private int _numberOfPoints;

    LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
    }

    private void Update()
    {
        //null check
        if (_lineRenderer is null || _start is null || _middle is null || _end is null)
            return;

        //update line renderer
        _lineRenderer.startColor = _color;
        _lineRenderer.endColor = _color;
        _lineRenderer.startWidth = _width;
        _lineRenderer.endWidth = _width;

        if(_numberOfPoints > 0)
            _lineRenderer.positionCount = _numberOfPoints;

        //set points of quadratic Bezier curve
        Vector3 p0 = _start.transform.position;
        Vector3 p1 = _middle.transform.position;
        Vector3 p2 = _end.transform.position;

        float t;
        Vector3 position;

        for (int i = 0; i < _numberOfPoints; i++)
        {
            t = i / (_numberOfPoints - 1f);
            position = (1f - t) * (1f - t) * p0
                    + 2f * (1f - t) * t * p1 + t * t * p2;
            _lineRenderer.SetPosition(i, position);
        }
    }
}

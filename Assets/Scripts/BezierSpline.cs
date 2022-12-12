using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class BezierSpline : MonoBehaviour
{
    [SerializeField] private List<GameObject> _controlPoints;
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private float _width = 0.2f;
    [SerializeField] private int _numberOfPoints = 20;
    LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
    }

    private void Update()
    {
        if (_lineRenderer is null || _controlPoints is null || _controlPoints.Count < 3)
            return;

        //update line renderer
        _lineRenderer.startColor = _color;
        _lineRenderer.endColor = _color;
        _lineRenderer.startWidth = _width;
        _lineRenderer.endWidth = _width;

        if (_numberOfPoints < 2)
            _numberOfPoints = 2;

        _lineRenderer.positionCount = _numberOfPoints * (_controlPoints.Count - 2);

        Vector3 p0, p1, p2;

        for (int j = 0; j < _controlPoints.Count - 2; j++)
        {
            //check control points
            if (_controlPoints[j] == null || _controlPoints[j + 1] == null
            || _controlPoints[j + 2] == null)
            {
                return;
            }
            //determine control points of segment
            p0 = 0.5f * (_controlPoints[j].transform.position
            + _controlPoints[j + 1].transform.position);
            p1 = _controlPoints[j + 1].transform.position;
            p2 = 0.5f * (_controlPoints[j + 1].transform.position
            + _controlPoints[j + 2].transform.position);

            //set points of quadratic Bezier curve
            Vector3 position;
            float t;
            float pointStep = 1.0f / _numberOfPoints;

            //last point of last segment should reach p2
            if (j == _controlPoints.Count - 3)
                pointStep = 1.0f / (_numberOfPoints - 1.0f);

            for (int i = 0; i < _numberOfPoints; i++)
            {
                t = i * pointStep;
                position = (1.0f - t) * (1.0f - t) * p0
                + 2.0f * (1.0f - t) * t * p1 + t * t * p2;
                _lineRenderer.SetPosition(i + j * _numberOfPoints, position);
            }
        }
    }
}

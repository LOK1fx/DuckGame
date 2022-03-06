using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[ExecuteInEditMode]
public class Ruler : MonoBehaviour
{
    [SerializeField] private float _totalDistance;

    [Space]
    [Header("Visual")]
    [SerializeField] private Color _lineColor = Color.yellow;
    [SerializeField] private float _spheresRadius = 0.15f;

    [Space]
    [SerializeField] private float[] _distances;
    [SerializeField] private List<Transform> _points = new List<Transform>();

    private void Update()
    {
        if(_points.Count < 1) { return; }

        _distances = new float[_points.Count - 1];

        _totalDistance = 0f;

        for (int i = 0; i < _points.Count; i++)
        {
            if(i + 1 < _points.Count)
            {
                if (_points[i] == null || _points[i + 1] == null) { return; }

                _distances[i] = Vector3.Distance(_points[i].position, _points[i + 1].position);

                _totalDistance += _distances[i];
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_points.Count < 1) { return; }

        for (int i = 0; i < _points.Count; i++)
        {
            if(i + 1 < _points.Count)
            {
                Gizmos.color = _lineColor;
                Gizmos.DrawLine(_points[i].position, _points[i + 1].position);
            }

            Gizmos.DrawSphere(_points[i].position, _spheresRadius);
        }
    }
}
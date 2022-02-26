using System.Collections.Generic;
using UnityEngine;

public class ProjectileDebugTrace : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] private int _maxTracePoses = 50;

    private List<Vector3> _tracePoses = new List<Vector3>();

    private Vector3 _startPos;
    private Vector3 _endPos;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (_tracePoses.Count > _maxTracePoses) { return; }

        _tracePoses.Add(transform.position);
    }

    private void OnDrawGizmos()
    {
        if(_tracePoses.Count > 0)
        {
            for (int i = 0; i < _tracePoses.Count; i++)
            {
                if(i + 1 < _tracePoses.Count)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(_tracePoses[i], _tracePoses[i + 1]);
                }
            }
        }
    }

    private void OnDestroy()
    {
        _endPos = transform.position;

        var dirDistance = Vector3.Distance(_startPos, _endPos);

        var distance = 0f;
        var distances = new float[_tracePoses.Count - 1];

        for (int i = 0; i < _tracePoses.Count; i++)
        {
            for (int j = 0; j < _tracePoses.Count; j++)
            {
                if (j + 1 < _tracePoses.Count)
                {
                    distances[j] = Vector3.Distance(_tracePoses[j], _tracePoses[j + 1]);

                    if(j - 1 > 0)
                    {
                        distance += (distances[j - 1] - distances[j]);
                    }
                }
            }
        }

        Debug.DrawLine(_startPos, _endPos, Color.green, 8f);

        Debug.Log($"Projectile traveles {dirDistance}m directly" +
            $"\n and {distance}m with curve.");
    }

#endif
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    private List<CheckPoint> _checkpoints;

    private void Awake()
    {
        _checkpoints = GetComponentsInChildren<CheckPoint>().ToList();
    }

    public CheckPoint GetNextCheckPoint(CheckPoint currentPoint)
    {
        if (currentPoint == null || currentPoint == _checkpoints.Last())
        {
            return _checkpoints.First();
        } 
        else
        {
            int index = _checkpoints.IndexOf(currentPoint);

            return _checkpoints[++index];
        }
    }
}

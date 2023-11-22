using GameEvent;
using System;
using UnityEngine;

public class MoverByPoints : Mover
{   
    [SerializeField] private Path _path;
    [SerializeField] private IntEvent _changedDirection;

    private CheckPoint _currentPoint;

    private void Start()
    {
        SetDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CheckPoint checkPoint) && checkPoint == _currentPoint)
            SetDirection(); 
    }

    private void SetDirection()
    {
        int direction;

        _currentPoint = _path.GetNextCheckPoint(_currentPoint);
        direction = Convert.ToInt16((_currentPoint.transform.position - transform.position).normalized.x);
        OnChangedDirection(direction);
        _changedDirection.Invoke(direction);
    }    
}

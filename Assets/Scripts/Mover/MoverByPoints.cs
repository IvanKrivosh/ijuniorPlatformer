using GameEvent;
using System;
using UnityEngine;

public class MoverByPoints : Mover
{   
    [SerializeField] private Path _path;
    [SerializeField] private IntEvent _changedDirection;

    private CheckPoint _currentPoint;
    private Transform _player;

    private void Start()
    {
        SetDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_player == null && collision.gameObject.TryGetComponent(out CheckPoint checkPoint) && checkPoint == _currentPoint)
            SetDirection(); 
    }
    
    public void OnFoundPlayer(Transform player)
    {
        _player = player;

        if (_player == null)
            SetDirection();
    }

    public void OnKilledEnemy()
    {
        OnFoundPlayer(null);
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

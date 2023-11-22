using UnityEngine;
using UnityEngine.Events;
using GameEvent;
using System;

public class KeyPressHandler : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const KeyCode SpaceCode = KeyCode.Space;
    private const int RightDirection = 1;
    private const int LeftDirection = -1;
    private const int MiddleDirection = 0;

    [SerializeField] private IntEvent _pressedHorizontalAxis;
    [SerializeField] private UnityEvent _pressedSpace;

    private int _direction = 0;
    private float _currentHorizontalValue = 0f;    

    private void Update()
    {
        float newHorizontalValue = Input.GetAxis(Horizontal);
        float direction = Math.Abs(newHorizontalValue) - Math.Abs(_currentHorizontalValue);
        int newDirection = _direction;

        if (direction > 0)        
            newDirection = newHorizontalValue > 0 ? RightDirection : LeftDirection;        
        else if (direction < 0)        
            newDirection = MiddleDirection;
        
        _currentHorizontalValue = newHorizontalValue;

        if (newDirection != _direction)
        {
            _direction = newDirection;
            _pressedHorizontalAxis.Invoke(_direction);
        }

        if (Input.GetKeyDown(SpaceCode))        
            _pressedSpace.Invoke();        
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MainMenuButtonsContainer : MonoBehaviour
{    
    [SerializeField] private float _distanceMove;
    [SerializeField] private float _stepMove;

    private Vector2 _currentPosition;
    private int _direction = 1;

    private void Awake()
    {
        _currentPosition = transform.position;
        StartCoroutine(MoveImage());
    }

    private IEnumerator MoveImage()
    {
        while (enabled)
        {
            if (Mathf.Abs(transform.position.y - _currentPosition.y) >= _distanceMove)            
                _direction = _direction * -1;

            transform.position = new Vector2(transform.position.x, transform.position.y + _stepMove * _direction);

            yield return null;
        }
    }

}

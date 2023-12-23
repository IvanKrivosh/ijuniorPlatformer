using GameEvent;
using System;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private Character _characterEnemy;

    private CharacterEvent _collidedCharacter = new CharacterEvent();

    public CharacterEvent CollidedEnemy => _collidedCharacter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Character>(out Character character) 
            && character.IsAlive && character.GetType() == _characterEnemy.GetType())
           CollidedEnemy.Invoke(character);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Character>(out Character character) 
            && character.GetType() == _characterEnemy.GetType())
            CollidedEnemy.Invoke(null);
    }

    public void ChangeDirection(int direction)
    {
        if (direction != 0)            
            transform.localPosition = new Vector2(Math.Abs(transform.localPosition.x) * direction, transform.localPosition.y);
    }
}

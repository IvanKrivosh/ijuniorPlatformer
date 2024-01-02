using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Attacker : MonoBehaviour
{
    [SerializeField] private AttackArea _area;
    [Range(1, 10)]
    [SerializeField] private int _damage = 5;
    [Range(0.1f, 5f)]
    [SerializeField] private float _attackDelay = 0.5f;
    [SerializeField] private bool _autoAttack = false;
    [SerializeField] UnityEvent _attaked;
    [SerializeField] UnityEvent _killedEnemy;    

    private WaitForSeconds _delayTime;
    private IEnumerator _attackCoroutine;
    private Character _focusedCharacter;

    public int Damage => _damage;

    private void Awake()
    {
        _area.CollidedEnemy.AddListener(OnCollidedEnemy);
        _delayTime = new WaitForSeconds(_attackDelay);
    }

    private void OnDestroy()
    {
        _area.CollidedEnemy.RemoveListener(OnCollidedEnemy);
    }

    public void OnAttacked()
    {
        if (_focusedCharacter != null)
            _focusedCharacter.TakeDamage(this.Damage);
    }

    public void OnChanchedDirection(int direction)
    {
        _area.ChangeDirection(direction);
    }

    public void Disabled()
    {
        this.enabled = false;
    }

    private void OnCollidedEnemy(Character character)
    {
        _focusedCharacter = character;

        if (_autoAttack)
        {
            if (_focusedCharacter != null && _attackCoroutine == null)
            {
                _focusedCharacter.Died.AddListener(OnKilledEnemy);
                _attackCoroutine = AttackEnemy();
                StartCoroutine(_attackCoroutine);
            }
            else if(_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }            
    }

    private IEnumerator AttackEnemy()
    {
        while (enabled && _focusedCharacter.IsAlive)
        {
            OnAttacked();
            _attaked.Invoke();
            yield return _delayTime;
        }

        _attackCoroutine = null;
    }

    private void OnKilledEnemy()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
            _focusedCharacter = null;
            _killedEnemy.Invoke();
        }
    }
}

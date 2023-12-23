using GameEvent;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [Range(10, 100)]
    [SerializeField] private int _health = 10;    
    [Range(1f, 10f)]
    [SerializeField] private float _lookDistance = 5f;
    [SerializeField] private ContactFilter2D _filter;
    [SerializeField] private TransformEvent _foundEnemy;

    private int _direction = 0;
    private Rigidbody2D _rigidbody;
    private RaycastHit2D[] _collisionResult = new RaycastHit2D[1];
    private Character _enemy;

    public UnityEvent Died;

    public bool IsAlive => _health > 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 lookDirection;
        int collisionCound;

        if (_direction == 0) return;

        lookDirection = transform.right * _direction;
        collisionCound = _rigidbody.Cast(lookDirection, _filter, _collisionResult, _lookDistance);

        if (collisionCound > 0 && _enemy == null 
            && _collisionResult[0].transform.TryGetComponent<Character>(out Character character) && character.IsAlive)
        {
            _enemy = character;
            _foundEnemy.Invoke(_enemy.transform);
        }
        else if (collisionCound ==  0 && _enemy != null)
        {
            _enemy = null;
            _foundEnemy.Invoke(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Elexir>(out Elexir elexir))
        {
            _health += elexir.Health;
            Destroy(elexir.gameObject);
        }
    }

    public void TakeDamage(Attacker enemy)
    {
        if (_health <= 0) return;

        _health -= enemy.Damage;

        if (_health <= 0)        
            Died.Invoke();                  
    }

    public void OnChangedDirection(int direction)
    {
        _direction = direction;
    }
}

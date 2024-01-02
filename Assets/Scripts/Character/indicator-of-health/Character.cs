using GameEvent;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [Range(10, 100)]
    [SerializeField] private int _healthValue = 10;    
    [Range(1f, 10f)]
    [SerializeField] private float _lookDistance = 5f;
    [SerializeField] private ContactFilter2D _filter;
    [SerializeField] private TransformEvent _foundEnemy;

    private int _direction = 0;
    private Rigidbody2D _rigidbody;
    private RaycastHit2D[] _collisionResult = new RaycastHit2D[1];
    private Character _enemy;
    private Health _healht;

    public UnityEvent Died;
    public TwoParamIntEvent ChangedHealth;

    public bool IsAlive => _healht.IsAlive;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _healht = new Health(_healthValue);

        _healht.Died += OnDied;
        _healht.ChandedHealth += OnChangedHealth;        
    }

    private void OnDestroy()
    {
        _healht.Died -= OnDied;
        _healht.ChandedHealth -= OnChangedHealth;
    }

    private void Start()
    {
        OnChangedHealth();
    }

    private void FixedUpdate()
    {
        Vector2 lookDirection;
        int collisionCount;

        if (_direction == 0) return;

        lookDirection = transform.right * _direction;
        collisionCount = _rigidbody.Cast(lookDirection, _filter, _collisionResult, _lookDistance);

        if (collisionCount > 0 && _enemy == null 
            && _collisionResult[0].transform.TryGetComponent<Character>(out Character character) && character.IsAlive)
        {
            _enemy = character;
            _foundEnemy.Invoke(_enemy.transform);
        }
        else if (collisionCount ==  0 && _enemy != null)
        {
            _enemy = null;
            _foundEnemy.Invoke(null);
        }
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Elexir>(out Elexir elexir))
        {
            _healht.TakeHealing(elexir.Health);
            Destroy(elexir.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        _healht.TakeDamage(damage);
    }

    public void TakeHealing(int healing)
    {
        _healht.TakeHealing(healing);
    }

    public void OnChangedDirection(int direction)
    {
        _direction = direction;
    }

    private void OnChangedHealth()
    {
        ChangedHealth.Invoke(_healht.MaxValue, _healht.Value);
    }

    private void OnDied()
    {
        Died.Invoke();
    }    
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const int DirectionRight = 1;    
    private const int DirectionLeft = -1;
    private const int NullSpeed = 0;

    [SerializeField] private float _maxSpeed;

    private Rigidbody2D _rigidbody;
    private float _currentSpeed;            
    private bool isLanded;    

    public bool IsLanded => isLanded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();        
    }

    private void FixedUpdate()
    {
        if (isLanded)
            _rigidbody.velocity = new Vector2(_currentSpeed, _rigidbody.velocity.y);
    }

    public void OnChangedDirection(int direction)
    {
        switch (Mathf.Clamp(direction, DirectionLeft, DirectionRight))
        {            
            case DirectionRight:
                _currentSpeed = _maxSpeed * DirectionRight;
                break;
            case DirectionLeft:
                _currentSpeed = _maxSpeed * DirectionLeft;
                break;
            default:
                _currentSpeed = NullSpeed;
                break;
        }
    }

    public void OnFlew()
    {
        isLanded = false;
    }

    public void OnLanded()
    {
        isLanded = true;
    }

    public void Disabled()
    {
        this.enabled = false;
    }

    protected void AddForceDirection(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force);
    }
}

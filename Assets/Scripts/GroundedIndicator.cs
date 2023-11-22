using UnityEngine;
using UnityEngine.Events;

public class GroundedIndicator : MonoBehaviour
{
    [SerializeField] UnityEvent _flew;
    [SerializeField] UnityEvent _landed; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Platform ground) && collision.GetContact(0).normal.y > 0)            
              _landed.Invoke();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {        
        if (collision.collider.TryGetComponent(out Platform ground))
            _flew.Invoke();
    }
}

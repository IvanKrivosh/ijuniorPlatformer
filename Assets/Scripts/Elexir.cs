
using UnityEngine;
using UnityEngine.Events;

class Elexir : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private int _health = 5;

    [HideInInspector] public UnityEvent Destroyed;

    public int Health => _health;

    private void OnDestroy()
    {
        Destroyed.Invoke();
    }
}

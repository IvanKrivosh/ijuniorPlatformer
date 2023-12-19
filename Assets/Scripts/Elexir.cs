
using UnityEngine;

class Elexir : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private int _health = 5;
    
    public int Health => _health;
}

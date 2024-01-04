using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class AbilitiesContainer : MonoBehaviour
{
    [SerializeField] private List<Ability> _abilities;

    private Player _player;
    private Ability _selectedAbility;

    private void Awake()
    {        
        _selectedAbility = _abilities[0];
        _player = GetComponent<Player>();

        foreach (var ability in _abilities)
            ability.Init(_player);
    }

    public void ActivateAbility()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _selectedAbility.Distance);

        foreach (Collider2D collider in hitColliders)
            if (collider.TryGetComponent<Enemy>(out Enemy enemy) && enemy.IsAlive)
            {
                _selectedAbility.Activate(enemy);
                break;
            }
    }

}

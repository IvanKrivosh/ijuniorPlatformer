using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SpawenPoint : MonoBehaviour
{
    [SerializeField] private Elexir _spawnObject;
    [SerializeField] private float _spawnTime = 10.0f;
    [SerializeField] private float _spawnDelay = 5f;

    private Elexir _elexir;

    private void Start()
    {
        SpawnObject();
    }

    private void SpawnObject()
    {
        _elexir = Instantiate(_spawnObject, transform);
        _elexir.Destroyed.AddListener(OnDestroyedElexir);
    }

    private void OnDestroyedElexir()
    {
        _elexir.Destroyed.RemoveListener(OnDestroyedElexir);

        if (enabled)
            Invoke(nameof(SpawnObject), _spawnDelay);
    }
}

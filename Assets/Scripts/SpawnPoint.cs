using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SpawenPoint : MonoBehaviour
{
    [SerializeField] private Elexir _spawnObject;
    [SerializeField] private float _spawnTime = 10.0f;
    [SerializeField] private float _spawnDelay = 5f;

    private Elexir _currntObject;

    private void Start()
    {
        SpawnObject();
    }

    private void SpawnObject()
    {
        _currntObject = Instantiate(_spawnObject, transform);
        _currntObject.Destroyed.AddListener(OnDestroyedElexir);
    }

    private void OnDestroyedElexir()
    {
        _currntObject.Destroyed.RemoveListener(OnDestroyedElexir);

        if (enabled)
            Invoke(nameof(SpawnObject), _spawnDelay);
    }
}

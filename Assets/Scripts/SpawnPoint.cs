using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SpawenPoint : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private float _spawnTime = 10.0f;

    private float _passedTime;
    private GameObject _currntObject;

    private void Start()
    {
        SpawnObject();
    }

    private void Update()
    {
        if (_currntObject == null)
        {
            if (_passedTime >= _spawnTime)
            {
                SpawnObject();
                _passedTime = 0;
            }

            _passedTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMover>(out PlayerMover player))
            Destroy(_currntObject);
    }

    private void SpawnObject()
    {
        _currntObject = Instantiate(_spawnObject, transform);
        _passedTime = 0;
    }
}

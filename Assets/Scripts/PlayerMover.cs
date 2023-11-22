using UnityEngine;

public class PlayerMover : Mover
{
    [SerializeField] private float _jumpForce = 1.0f;

    public void OnJumped()
    {
        if (IsLanded)
            AddForceDirection(Vector3.up, _jumpForce);
    }
}

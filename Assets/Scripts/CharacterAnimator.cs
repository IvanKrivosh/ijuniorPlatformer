using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private const string IsWalking = "IsWalking";
    private const string Direction = "Direction";
    private const string Landed = "IsLanded";
    private const float MiddleDirection = 0f;

    private Animator _animator;
    private float _direction = 0f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnMoved(int direction)
    {
        Vector2 vectorDirection = new Vector2(direction, 0).normalized;

        if (!Mathf.Approximately(vectorDirection.x, MiddleDirection))
            _direction = vectorDirection.x;

        _animator.SetBool(IsWalking, vectorDirection.magnitude != MiddleDirection);
        _animator.SetFloat(Direction, _direction);
    }

    public void OnFlew()
    {
        SetLanded(false);
    }

    public void OnLanded()
    {
        SetLanded();
    }

    private void SetLanded(bool isLanded = true)
    {
        _animator.SetBool(Landed, isLanded);
    }
}

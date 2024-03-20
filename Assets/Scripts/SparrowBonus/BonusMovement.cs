using System;
using DG.Tweening;
using UnityEngine;

public class BonusMovement : MonoBehaviour
{
  [SerializeField] private Animator _animator;
  [SerializeField] private float _duration;

    public void MoveToTarget(Vector3 target, Action onMovementComplete)
    {
      _animator.Play("Fly");
      transform.DOMove(target, _duration).OnComplete(() => OnMovementCompleted(onMovementComplete));
    }

    private void OnMovementCompleted(Action onMovementComplete)
    {
        _animator.Play("Eat");
        onMovementComplete?.Invoke();
    }
}
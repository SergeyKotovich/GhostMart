using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BonusMovement : MonoBehaviour
{
  [SerializeField] private Animator _animator;
  [SerializeField] private float _duration=20f;

    public void MoveToTarget(Vector3 target)
    {
      _animator.Play("Fly");
        transform.DOMove(target,_duration).OnComplete(() => _animator.Play("Eat"));
    }
}
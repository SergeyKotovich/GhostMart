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
    //new Vector3(-4.26000023f, 22.3799992f, -110.699997f), 15f улет
    //Vector3(-4.34656954,0,-48.4787254) прилет
}
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] private float _moveRadius = 5f; // Радиус перемещения
    [SerializeField] private float _moveInterval = 5f; // Интервал времени между перемещениями
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _centralPoint; // Центральная точка для определения радиуса перемещения

    private bool isMoving;

    private void Start()
    {
        RandomMove();
    }

    private async UniTask RandomMove()
    {
        while (true)
        {
            if (!isMoving)
            {
                // Генерируем случайную позицию в пределах радиуса
                Vector2 randomOffset = Random.insideUnitCircle * _moveRadius;
                Vector3 randomPosition = new Vector3(randomOffset.x, 0f, randomOffset.y) + _centralPoint.transform.position;
                
                transform.DOLookAt(randomPosition, 0.5f);

                MoveToPosition(randomPosition);

                PlayIdleAnimation();
            }

            await UniTask.Delay((int)(_moveInterval * 4000)); // Ожидаем перед началом следующего перемещения
        }
    }

    private async UniTask MoveToPosition(Vector3 targetPosition)
    {
        isMoving = true;

        PlayMovementAnimation(true);

        transform.DOMove(targetPosition, _moveInterval).OnComplete(() => isMoving = false);

        await UniTask.Delay(2000);
        PlayMovementAnimation(false);
    }

    private void PlayMovementAnimation(bool isWalking)
    {
        if (_animator != null)
        {
            _animator.SetBool("IsWalking", isWalking);
        }
    }

    private void PlayIdleAnimation()
    {
        if (_animator != null)
        {
            _animator.Play("Idle1");
        }
    }
}
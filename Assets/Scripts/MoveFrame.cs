using UnityEngine;
using DG.Tweening;

public class MoveFrame : MonoBehaviour
{

    [SerializeField] private float moveDuration = 2f; // Длительность одного цикла перемещения
    [SerializeField] private float whaitDuration = 0.5f; // Ожидание

    [SerializeField] private GameObject _gameObject1;
    [SerializeField] private GameObject _gameObject2;
    [SerializeField] private GameObject _gameObject3;
    [SerializeField] private GameObject _gameObject4;

    void Start()
    {
        StartMoveSequence();
    }

    void StartMoveSequence()
    {
        
        DOTween.Sequence()
            .Append(_gameObject1.transform.DOLocalMove(new Vector3(0.5f, -0.5f, 0), moveDuration))
            .AppendInterval(whaitDuration)
            .Append(_gameObject1.transform.DOLocalMove(Vector3.zero, moveDuration));

        DOTween.Sequence()
            .Append(_gameObject2.transform.DOLocalMove(new Vector3(0.5f, 0.5f, 0), moveDuration))
            .AppendInterval(whaitDuration)
            .Append(_gameObject2.transform.DOLocalMove(Vector3.zero, moveDuration));

        DOTween.Sequence()
            .Append(_gameObject3.transform.DOLocalMove(new Vector3(-0.5f, -0.5f, 0), moveDuration))
            .AppendInterval(whaitDuration)
            .Append(_gameObject3.transform.DOLocalMove(Vector3.zero, moveDuration));

        DOTween.Sequence()
            .Append(_gameObject4.transform.DOLocalMove(new Vector3(-0.5f, 0.5f, 0), moveDuration))
            .AppendInterval(whaitDuration)
            .Append(_gameObject4.transform.DOLocalMove(Vector3.zero, moveDuration))
            .OnComplete(StartMoveSequence); // новый цикл
    }
}
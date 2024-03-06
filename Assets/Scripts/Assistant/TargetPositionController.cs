using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetPositionController : MonoBehaviour
{
    [SerializeField] private List<Transform> _pointPath;
    private int _currentIndex;

    public Vector3 SetPosition()
    {
        var currentPosition = _pointPath.First().position;
        SetNextPoint();
        return currentPosition;

    }
    private void SetNextPoint()
    {
        _currentIndex = (_currentIndex + 1) % _pointPath.Count;
    }
}
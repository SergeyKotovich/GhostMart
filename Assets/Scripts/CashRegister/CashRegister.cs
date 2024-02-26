using System;
using Interfaces;
using UnityEngine;

public class CashRegister : MonoBehaviour, IInteractable
{
    public event Action LineChanged;
    [field:SerializeField] public Sprite StandIcon {get; private set; }
    [field:SerializeField] public Transform PointForCustomers { get; private set; }
    [field:SerializeField] public TypeProduct Type { get; private set; }
    [field:SerializeField] public Vector3 ShiftForNextPosition { get; private set; }
    public bool IsAvailable { get; private set; }
    private bool _isBusy;
    public Vector3 LastBusyPositionInLine { get; private set; }

    private void Awake()
    {
        LastBusyPositionInLine = PointForCustomers.position;
    }
    public Vector3 GetFreePosition()
    {
        if (!_isBusy)
        {
            _isBusy = true;
            return PointForCustomers.position;
        }
        return LastBusyPositionInLine += ShiftForNextPosition;
    }

    public void OnCustomerLeft()
    {
        if (LastBusyPositionInLine == PointForCustomers.position)
        {
            _isBusy = false;
            LineChanged?.Invoke();
            Debug.Log("customerLeft");
            return;
        }
        
        LineChanged?.Invoke();
        LastBusyPositionInLine -= ShiftForNextPosition;
    }
}
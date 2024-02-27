using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using NUnit.Framework;
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

    private List<ICustomer> _customersInLine = new();

    private void Awake()
    {
        LastBusyPositionInLine = PointForCustomers.position;
        IsAvailable = false;
    }
    public Vector3 GetFreePosition(ICustomer customer)
    {
        _customersInLine.Add(customer);
        if (!_isBusy)
        {
            _isBusy = true;
            return PointForCustomers.position;
        }
        return LastBusyPositionInLine += ShiftForNextPosition;
    }

    public void OnCustomerLeft(ICustomer customer)
    {
        _customersInLine.Remove(customer);
        _isBusy = false;
        Debug.Log("customerLeft");
        Debug.Log("LastBusyPositionInLine before " + LastBusyPositionInLine);
        LastBusyPositionInLine -= ShiftForNextPosition;
        Debug.Log("LastBusyPositionInLine after " + LastBusyPositionInLine);
        MoveCustomersForward();
    }

    private void MoveCustomersForward()
    {
        foreach (var customer in _customersInLine)
        {
            var destination = customer.PositionInLine - ShiftForNextPosition;
            customer.SetDestination(destination);
        }
    }

    public void Open()
    {
        IsAvailable = true;
        Debug.Log("CashRegister IsAvailable" + IsAvailable);
    }

    public void CLose()
    {
        IsAvailable = false;
        Debug.Log("CashRegister IsAvailable" + IsAvailable);
    }
}